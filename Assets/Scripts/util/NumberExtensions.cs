using UnityEngine;
using static System.Math;
using Random = System.Random;

namespace extensions
{
    public static class NumberExtensions
    {

        public static Random r = new Random();

        /**
         * Convert float to 1 or negative 1
         */
        public static float Normalize(this float f, bool pickRandomDirWhenZero = false)
        {
            if (f.EqZero() && pickRandomDirWhenZero) return r.NextDouble() > 0.5 ? 1 : -1;
            return f >= 0 ? 1 : 0;
        }

        public static bool EqZero(this float f) => Abs(f) < float.Epsilon;
        public static bool SoftEquals(this float f, float that) => (f - that).EqZero();

        public static float Clamp(this float f, float min = -1, float max = -1) => Min(Max(f, min), max);

        /**
         * Given a float between 0 and 1, scale it to be between the new min and max values
         */
        public static float Interpolate(this float f, float min = 0, float max = 1) => f.ReScale(newMin: min, newMax: max);
        
        public static float ReScale(this float f, float min = 0, float max = 1, float newMin = 0, float newMax = 1) => newMin + (newMax - newMin) * ((f - min) / (max - min));

        public static float Squared(this float f) => f * f;
    }
}