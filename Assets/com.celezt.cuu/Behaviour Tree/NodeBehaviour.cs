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

            _children = new List<NodeBehaviour>();
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out NodeBehaviour node))
                    _children.Add(node);
            }
        }

        private void Start()
        {
            if (_parent == null)
            {
                SetupCreate();

                if (_updateBT == null)
                    _updateBT = StartCoroutine(UpdateBT());
            }
        }

        private void OnEnable()
        {
            if (_parent == null && _root != null)
                if (_updateBT == null)
                    _updateBT = StartCoroutine(UpdateBT());
        }

        private void OnDisable()
        {
            StopCoroutine(UpdateBT());
            _updateBT = null;
        }

        private void SetupParent()
        {
            if (_parent == null)
            {
                Transform parent = transform.parent;
                if (parent != null)
                    _isRoot = !parent.TryGetComponent(out _parent);
                else
                    _isRoot = true;
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

            CreateNode(_children, _parent);

            for (int i = 0; i < _children.Count; i++)
            {
                _children[i].SetupCreate();
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
            _status = data.Status;

            return data.Process(nextNode);
        }
    }
}
