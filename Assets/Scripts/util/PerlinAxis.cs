using UnityEngine;
using Random = UnityEngine.Random;

namespace util
{
    public class PerlinAxis
    {
        private readonly Vector2 _startPos;
        private readonly Vector2 _moveVector;
        private readonly float _startFixedTime;
        public PerlinAxis(float speed)
        {
            _startFixedTime = Time.fixedTime;
            _startPos = new Vector2(Random.value, Random.value) * 100;
            _moveVector = new Vector2(Random.value, Random.value).normalized * speed;
        }

        public float GetValue()
        {
            var newPos = _startPos + (Time.fixedTime - _startFixedTime) * _moveVector;
            return Mathf.PerlinNoise(newPos.x, newPos.y);
        }
    }
}