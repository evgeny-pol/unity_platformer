using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField, Min(0f)] private float _value = 1f;

        private float _timeUp;

        public bool IsReady => _timeUp <= Time.time;

        public void Reset()
        {
            _timeUp = Time.time + _value;
        }
    }
}
