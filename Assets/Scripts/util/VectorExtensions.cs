using System;
using UnityEngine;

namespace extensions
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3Vertical(this Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0);
        }
        
        public static Vector3 ToVector3Horizontal(this Vector2 v2)
        {
            return new Vector3(v2.x, 0, v2.y);
        }

        public static Vector3 ZeroX(this Vector3 v)
        {
            return v.WithX(0);
        }

        public static Vector3 ZeroY(this Vector3 v)
        {
            return v.WithY(0);
        }

        public static Vector3 ZeroZ(this Vector3 v)
        {
            return v.WithZ(0);
        }

        public static Vector3 WithX(this Vector3 v, float newX)
        {
            return new Vector3(newX, v.y, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float newY)
        {
            return new Vector3(v.x, newY, v.z);
        }
        
        public static Vector3 With(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x.GetValueOrDefault(v.x), y.GetValueOrDefault(v.y), z.GetValueOrDefault(v.z));
        }

        public static Vector3 WithZ(this Vector3 v, float newZ)
        {
            return new Vector3(v.x, v.y, newZ);
        }

        public static Vector3 AddX(this Vector3 v1, Vector3 v2)
        {
            return v1 + v2.KeepX();
        }

        public static Vector3 AddY(this Vector3 v1, Vector3 v2)
        {
            return v1 + v2.KeepY();
        }

        public static Vector3 AddZ(this Vector3 v1, Vector3 v2)
        {
            return v1 + v2.KeepZ();
        }

        public static Vector3 KeepX(this Vector3 v)
        {
            return Vector3.Scale(v, new Vector3(1, 0, 0));
        }

        public static Vector3 KeepY(this Vector3 v)
        {
            return Vector3.Scale(v, new Vector3(0, 1, 0));
        }

        public static Vector3 KeepZ(this Vector3 v)
        {
            return Vector3.Scale(v, new Vector3(0, 0, 1));
        }

        public static Vector3 WithMaxX(this Vector3 v, float max)
        {
            return v.WithX(Math.Min(max, v.x));
        }

        public static Vector3 WithMaxY(this Vector3 v, float max)
        {
            return v.WithY(Math.Min(max, v.y));
        }

        public static Vector3 WithMinY(this Vector3 v, float min)
        {
            return v.WithY(Math.Max(min, v.y));
        }

        public static Vector3 WithMaxZ(this Vector3 v, float max)
        {
            return v.WithZ(Math.Min(max, v.z));
        }

        public static Vector3 WithClampY(this Vector3 v, float min, float max)
        {
            return v.WithY(Math.Max(min, Math.Min(max, v.y)));
        }

        public static Quaternion ToQuaternion(this Vector3 v)
        {
            return Quaternion.Euler(v);
        }

        public static Vector3 ClampMagnitude(this Vector3 v, float maxMagnitude, bool clampX=true, bool clampY=true, bool clampZ=true)
        {
            var newV = v.WithZeroedDimensions(!clampX, !clampY, !clampZ);
            var inverseNewV = v.WithZeroedDimensions(clampX, clampY, clampZ);
            return newV.magnitude > maxMagnitude ? (newV.normalized * maxMagnitude) + inverseNewV : v;
        }

        public static Vector3 Add(this Vector3 v, Vector3 that)
        {
            return v + that;
        }

        public static Vector3 Subtract(this Vector3 v, Vector3 that)
        {
            return v - that;
        }

        public static Vector3 ClampSpeed(this Vector3 v, float horizontalMaxSpeed=Mathf.Infinity, float verticalMaxSpeed=Mathf.Infinity)
        {
            return v.ClampMagnitude(horizontalMaxSpeed, clampY: false)
                .ClampMagnitude(verticalMaxSpeed, false, clampZ: false);
        }

        public static Vector3 WithZeroedDimensions(this Vector3 v, bool zeroX, bool zeroY, bool zeroZ)
        {
            return new Vector3(zeroX ? 0 : v.x, zeroY ? 0 : v.y, zeroZ ? 0 : v.z);
        }

        public static Vector3 WithXZ(this Vector3 v, Vector3 other)
        {
            return new Vector3(other.x, v.y, other.z);
        }

        public static Vector3 Max(Vector3 v1, Vector3 v2)
        {
            return new Vector3(Mathf.Max(v1.x, v2.x), Mathf.Max(v1.y, v2.y), Mathf.Max(v1.z, v2.z));
        }

        public static Vector3 Min(Vector3 v1, Vector3 v2)
        {
            return new Vector3(Mathf.Min(v1.x, v2.x), Mathf.Min(v1.y, v2.y), Mathf.Min(v1.z, v2.z));
        }

        
        /**
         * Say your implementing a double jump, you want the second jump impulse to be a little stronger if the player is already on the way down from the first jump. That's all this does.
         */
        public static Vector3 IncreaseJumpForceIfFalling(this Vector3 jumpVector, Vector3 rigidbodyVelocity)
        {
            return jumpVector.Subtract(rigidbodyVelocity.y < 0
                ? rigidbodyVelocity.KeepY()
                : Vector3.zero);
        }

        /* Saves on calling a sqrt */
        public static float DistanceSquared(this Vector3 pos1, Vector3 pos2)
        {
            return (pos1.x - pos2.x) * (pos1.x - pos2.x) +
                (pos1.y - pos2.y) * (pos1.y - pos2.y) +
                (pos1.z - pos2.z) * (pos1.z - pos2.z);
        }
    }
}