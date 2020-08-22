using System;
using Random = UnityEngine.Random;

namespace util
{
    public static class RandomExtensions
    {
        public static bool RandomBool => Random.value > 0.5;
        public static float RandomSign => Random.value > 0.5 ? 1 : -1;
    }
}