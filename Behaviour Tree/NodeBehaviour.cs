using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;


namespace Celezt.BehaviourTree.GameObject
{
    /// <summary>
    /// NodeBehaviour is the base class from which every BehaviourTree node derives.
    /// </summary>
    public abstract class NodeBehaviour : MonoBehaviour, INodeBehaviour
    {
        public IReadOnlyList<NodeBehaviour> Nodes { get => _nodes.AsReadOnly(); }
        public NodeBehaviour Root => _root;
        public NodeBehaviour Parent => _parent;
        public NodeStatus Status => _status;
        public bool IsRoot => _isRoot;

        [SerializeField, ConditionalField(nameof(_isRoot))] private UpdateType _updateType = UpdateType.Update;
        [SerializeField, ConditionalField(nameof(_updateType), false, UpdateType.Manual)] private float _seconds = 0.5f;

        private List<NodeBehaviour> _nodes;
        private List<NodeBehaviour> _children;
        private NodeBehaviour _parent;
        private NodeBehaviour _root;
        private NodeBehaviour _pendingNode;

        private Coroutine _updateBT;

        private NodeStatus _status;

        private bool _isStarted;
        private bool _isCreated;

        [SerializeField, HideInInspector] private bool _isRoot;

        private enum UpdateType
        {
            Update,
            FixedUpdate,
            Manual,
        }

        public abstract void CreateNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent);
        public abstract INodeAsset ProcessNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent);

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetupParent();
        }
#endif

        private void Awake()
        {
            SetupParent();
            SetupChildren();
        }

        private void Start()
        {
            _isStarted = true;

            if (_parent == null)
            {
                SetupRecursiveCreate();

                if (_updateBT == null)
                    _updateBT = StartCoroutine(UpdateBT());
            }
        }

        private void OnEnable()
        {
            if (_isStarted)
            {
                if (_parent != null)
                    _parent.SetupChildren();

                if (!_isCreated)
                    SetupCreate();
            }

            if (_parent == null && _root != null)
            {
                if (_updateBT == null)
                    _updateBT = StartCoroutine(UpdateBT());
            }
        }

        private void OnDisable()
        {
            if (_parent != null)
                _parent._children.Remove(this);

            StopCoroutine(UpdateBT());
            _updateBT = null;
        }

        private void SetupParent()
        {
            Transform parent = transform.parent;
            if (parent != null)
                _isRoot = !parent.TryGetComponent(out _parent);
            else
                _isRoot = true;
        }

        private void SetupChildren()
        {
            _children = new List<NodeBehaviour>();
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out NodeBehaviour node))
                    if (node.enabled)
                        _children.Add(node);
            }
        }

        private void SetupCreate()
        {
            if (_parent == null && _root == null)
                SetupRoot();
            if (_parent != null)
            {
                _root = _parent._root;
                _nodes = _parent._nodes;
                _nodes.AddRange(_children);
            }

            _isCreated = true;
            CreateNode(_children, _parent);
        }

        private void SetupRecursiveCreate()
        {
            SetupCreate();

            for (int i = 0; i < _children.Count; i++)
            {
                if (_children[i].enabled)
                    _children[i].SetupRecursiveCreate();
            }
        }

        private void SetupRoot()
        {
            _nodes = new List<NodeBehaviour>();
            _root = this;
            _pendingNode = this;
        }

        private IEnumerator UpdateBT()
        {
            while (true)
            {
                while (true)
                {
                    NodeBehaviour tempNode = Next(_pendingNode);

                    if (tempNode == _pendingNode)
                        break;
                    else
                        _pendingNode = tempNode;
                }

                switch (_updateType)
                {
                    case UpdateType.Update:
                        yield return null;
                        break;
                    case UpdateType.FixedUpdate:
                        yield return new WaitForFixedUpdate();
                        break;
                    case UpdateType.Manual:
                        yield return new WaitForSeconds(_seconds);
                        break;
                }
            }
        }

        private NodeBehaviour Next(NodeBehaviour nextNode)
        {
            INodeAsset data = nextNode.ProcessNode(nextNode._children, nextNode._parent);
            nextNode._status = data.Status;

            return data.Process(nextNode);
        }
    }
}
