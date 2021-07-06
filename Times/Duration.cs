using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celezt.Time
{
    /// <summary>
    /// Count down a duration.
    /// </summary>
    [Serializable]
    public struct Duration : IEquatable<Duration>
    {
        /// <summary>
        /// Unique integer value.
        /// </summary>
        public readonly int ID;
        /// <summary>
        /// If the duration still is active or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (!_paused)
                    Update();

                return _timeLeft > 0;
            }
        }
        /// <summary>
        /// How much time is left in seconds.
        /// </summary>
        public float TimeLeft
        {
            get
            {
                if (!_paused)
                    Update();

                return _timeLeft;
            }
        }
        /// <summary>
        /// How much time is left in unit interval [1,0].
        /// </summary>
        public float UnitIntervalTimeLeft
        {
            get
            {
                if (!_paused)
                    Update();

                return _timeLeft / _initTime;
            }
        }
        /// <summary>
        /// If the duration is currently paused or not.
        /// </summary>
        public bool IsPaused { get => _paused; }
        /// <summary>
        /// Initialized time length.
        /// </summary>
        public float InitTime { get => _initTime; }
        /// <summary>
        /// Delta time of the duration. 1 / duration.
        /// </summary>
        public float Delta { get => 1 / _initTime; }

        private static int _counter;

        private float _oldGameTime;
        private float _pauseGameTime;
        [SerializeField] private float _timeLeft;
        [SerializeField] private float _initTime;
        private bool _paused;
        private bool _isScaledTime;

        public bool Equals(Duration other) => ID == other.ID;
        public override bool Equals(object obj) => (obj != null) ? obj.GetHashCode() == GetHashCode() : false;
        public override int GetHashCode() => ID;
        public override string ToString() => TimeLeft.ToString();

        public bool Pause()
        {
            if (!_paused)
                _pauseGameTime = _isScaledTime ? UnityEngine.Time.time : UnityEngine.Time.unscaledTime;

            return _paused = true;
        }
        public bool Resume()
        {
            if (_paused)
            {
                float currentTime = _isScaledTime ? UnityEngine.Time.time : UnityEngine.Time.unscaledTime;
                float deltaTime = currentTime - _pauseGameTime;
                _pauseGameTime = currentTime;
                _oldGameTime += deltaTime;
            }

            return _paused = false;
        }

        public void Reset()
        {
            _timeLeft = _initTime;
            _oldGameTime = UnityEngine.Time.time;
            Resume();
        }

        public void Done()
        {
            _timeLeft = 0;
            Pause();
        }

        public void Set(float duration)
        {
            _oldGameTime = _isScaledTime ? UnityEngine.Time.time : UnityEngine.Time.unscaledTime;
            _pauseGameTime = _oldGameTime;
            _paused = false;
            _timeLeft = duration;
            _initTime = duration;
        }

        /// <summary>
        /// Manual Update.
        /// </summary>
        public void Update()
        {
            if (_paused || float.IsPositiveInfinity(_timeLeft))
                return;

            float currentTime = _isScaledTime ? UnityEngine.Time.time : UnityEngine.Time.unscaledTime;
            float deltaTime = currentTime - _oldGameTime;
            _oldGameTime = currentTime;

            if (_timeLeft - deltaTime >= 0)
                _timeLeft -= deltaTime;
            else
                _timeLeft = 0;
        }

        public Duration(float duration, bool isScaledTime = true)
        {
            ID = ++_counter;
            _isScaledTime = isScaledTime;
            _oldGameTime = _isScaledTime ? UnityEngine.Time.time : UnityEngine.Time.unscaledTime;
            _pauseGameTime = _oldGameTime;
            _paused = false;
            _timeLeft = duration;
            _initTime = duration;
        }

        public static Duration Infinity => new Duration(float.PositiveInfinity);
        public static Duration Empty => new Duration(0);

        public static bool operator ==(Duration lhs, Duration rhs) => lhs.ID == rhs.ID;
        public static bool operator !=(Duration lhs, Duration rhs) => lhs.ID != rhs.ID;
        public static bool operator >(Duration lhs, Duration rhs) => lhs.TimeLeft > rhs.TimeLeft;
        public static bool operator <(Duration lhs, Duration rhs) => lhs.TimeLeft < rhs.TimeLeft;
        public static bool operator <=(Duration lhs, Duration rhs) => lhs.TimeLeft <= rhs.TimeLeft;
        public static bool operator >=(Duration lhs, Duration rhs) => lhs.TimeLeft >= rhs.TimeLeft;
        public static float operator +(Duration lhs, Duration rhs) => lhs._timeLeft + rhs.TimeLeft;
        public static float operator -(Duration lhs, Duration rhs) => lhs._timeLeft - rhs.TimeLeft;
        public static float operator +(Duration lhs, float rhs) => lhs._timeLeft + rhs;
        public static float operator -(Duration lhs, float rhs) => lhs._timeLeft - rhs;
        public static float operator +(float lhs, Duration rhs) => lhs + rhs.TimeLeft;
        public static float operator -(float lhs, Duration rhs) => lhs - rhs.TimeLeft;
    }
}
