using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celezt.BehaviourTree.GameObject
{
    public interface INodeBehaviour
    {
        public IReadOnlyList<NodeBehaviour> Nodes { get; }
        public NodeBehaviour Root { get; }
        public NodeBehaviour Parent { get; }
        public NodeStatus Status { get; }

        /// <summary>
        /// Called each time the node is rebuild. Does it on start.
        /// </summary>
        public abstract void CreateNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent);
        /// <summary>
        /// Called each time the node is processed.
        /// </summary>
        public abstract INodeAsset ProcessNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent);
    }
}