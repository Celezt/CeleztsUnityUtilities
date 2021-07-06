using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Celezt.BehaviourTree.GameObject
{
    [AddComponentMenu("Celezt/BehaviourTree/GameObject/Leaves/Unity Event")]
    public class UnityEventBehaviour : NodeBehaviour
    {
        [SerializeField, Tooltip("How many updates it takes to invoke.")] private int _frequency = 1;
        [SerializeField] private UnityEvent _processEvent;

        private int _counter;

        public override void CreateNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent)
        {

        }

        public override INodeAsset ProcessNode(IReadOnlyList<NodeBehaviour> children, NodeBehaviour parent)
        {
            if (_counter == 0)
                _processEvent.Invoke();

            _counter = (1 + _counter) % _frequency;

            return new LeafAsset { Status = NodeStatus.Success };
        }
    }
}
