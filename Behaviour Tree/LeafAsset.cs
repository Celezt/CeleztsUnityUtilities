using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celezt.BehaviourTree.GameObject
{
    public struct LeafAsset : INodeAsset
    {
        public NodeStatus Status { get; set; }

        public NodeBehaviour Process(NodeBehaviour node)
        {
            // If the leaf is also the root, reset and return itself.
            if (node == node.Root)
                return node;

            return node.Parent;
        }
    }
}

