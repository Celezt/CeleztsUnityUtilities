using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celezt.BehaviourTree.GameObject
{
    /// <summary>
    /// Implement to add a new node type to the behaviour tree.
    /// </summary>
    public interface INodeAsset
    {
        public NodeStatus Status { get; set; }

        /// <summary>
        /// Called each time the result from a node is returned.
        /// </summary>
        /// <param name="node">Processed node.</param>
        /// <returns>Next node.</returns>
        public NodeBehaviour Process(NodeBehaviour node);
    }
}
