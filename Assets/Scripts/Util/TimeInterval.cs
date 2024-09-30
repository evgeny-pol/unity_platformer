using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class TimeInterval
    {
        [SerializeField, Min(0f)] private float _duration = 1f;

        private float _timeEnd;

        public float Remaining => Mathf.Max(_timeEnd - Time.time, 0);

        public float Duration => _duration;

        public bool IsEnded => Remaining == 0;

        public void Start()
        {
            _timeEnd = Time.time + _duration;
        }

        public void End()
        {
            _timeEnd = Time.time;
        }
    }
}
