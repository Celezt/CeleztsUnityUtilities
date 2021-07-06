using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celezt.BehaviourTree.GameObject
{
    [AddComponentMenu("Celezt/BehaviourTree/GameObject/Composites/Selector")]
    public class SelectorBehaviour : NodeBehaviour
    {
        private NodeBehaviour _currentChild;

        private int _traversed;

        public override void CreateNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent)
        {

        }

        public override INodeAsset ProcessNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent)
        {
            CompositeAsset data = default;

            // If the current child returns success, break this composite and return success.
            if (_currentChild != null && _currentChild.Status == NodeStatus.Success)
            {
                data = new CompositeAsset { Status = NodeStatus.Success };
                Reset();
            }
            // If it has no children, set status as success (nothing happens).
            else if (children.Count == 0)
            {
                data = new CompositeAsset { Status = NodeStatus.Success };
                Reset();
            }
            else
            {
                if (_traversed < children.Count)
                {
                    NodeBehaviour next = children[_traversed++];
                    data = new CompositeAsset { Status = NodeStatus.Running, Next = next };
                    _currentChild = next;
                }
                // If it has passed all the nodes unsuccessfully.
                else
                {
                    data = new CompositeAsset { Status = NodeStatus.Failure };
                    Reset();
                }
            }

            return data;
        }

        private void Reset()
        {
            _traversed = 0;
            _currentChild = null;
        }
    }
}
