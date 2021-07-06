using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celezt.BehaviourTree.GameObject
{
    public struct CompositeAsset : INodeAsset
    {
        public NodeStatus Status { get; set; }
        public NodeBehaviour Next { get; set; }

        public NodeBehaviour Process(NodeBehaviour node)
        {
            if (Status == NodeStatus.Running)
                return Next;

            // If the composite is also the root, reset and return itself.
            if (node == node.Root)
                return node;

            // if it succeed or fails, return the status to its parent.
            return node.Parent;
        }
    }
}
