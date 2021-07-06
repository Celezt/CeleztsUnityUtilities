using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Celezt.Timeline
{
    [AddComponentMenu("Celezt/Playable/Playable Handler")]
    public class PlayableHandler : MonoBehaviour
    {
        public PlayableDirector CurrentDirector => _currentDirector;

        private PlayableDirector _currentDirector;

        public void GetDirector(PlayableDirector director)
        {
            _currentDirector = director;
        }

        public void OnSkip()
        {
            _currentDirector.time = _currentDirector.playableAsset.duration;
        }

        public void OnSkipTo(float time)
        {
            _currentDirector.time = time;
        }

        public void ResetToStart()
        {
            _currentDirector.time = 0;
            _currentDirector.Evaluate();
        }

        private void OnEnable()
        {
            TryGetComponent<PlayableDirector>(out _currentDirector);    // Try to get component if it exist on the same game object.
        }
    }
}
