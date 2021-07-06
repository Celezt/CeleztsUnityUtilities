using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Celezt.BehaviourTree.GameObject
{
    [AddComponentMenu("Celezt/BehaviourTree/GameObject/Leaves/Link")]
    public class LinkBehaviour : NodeBehaviour
    {
        private NodeStatus _status;

        public void OnSuccess() => _status = NodeStatus.Success;
        public void OnFailure() => _status = NodeStatus.Failure;
        public void OnRunning() => _status = NodeStatus.Running;

        public override void CreateNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent)
        {

        }

        public override INodeAsset ProcessNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent)
        {
            return new LeafAsset { Status = _status };
        }
    }
}