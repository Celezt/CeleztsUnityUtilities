using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using MyBox;
using Celezt.Mathematics;

namespace Celezt.Timeline
{
    [AddComponentMenu("Celezt/Playable/Playable Range Remap")]
    public class PlayableRangeRemap : MonoBehaviour
    {
        public PlayableDirector CurrentDirector => _currentDirector;

        public MinMaxFloat Range;
        public MinMaxFloat ToRange;

        [SerializeField] private UnityEvent _outsideRangeEvent;

        private PlayableDirector _currentDirector;

        public void GetDirector(PlayableDirector director)
        {
            _currentDirector = director;
        }

        public void MirrorCurrentTime()
        {
            float time = (float)_currentDirector.time;
            if (Range.IsInRange(time))
                _currentDirector.time = ToRange.Max - (_currentDirector.time.Map(Range, ToRange) - ToRange.Min);
            else
                _outsideRangeEvent.Invoke();
        }

        public void RemapCurrentTime()
        {
            float time = (float)_currentDirector.time;
            if (Range.IsInRange(time))
                _currentDirector.time = _currentDirector.time.Map(Range, ToRange);
            else
                _outsideRangeEvent.Invoke();
        }

        private void OnEnable()
        {
            if (!TryGetComponent<PlayableDirector>(out _currentDirector))   // Try to get component if it exist on the same game object.
                if (TryGetComponent<PlayableHandler>(out PlayableHandler handler))  // Else try get it from PlayableHandler.
                    _currentDirector = handler.CurrentDirector;
        }
    }
}
