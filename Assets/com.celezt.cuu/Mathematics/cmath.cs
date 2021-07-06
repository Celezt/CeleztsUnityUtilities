using System;
using UnityEngine;
using Unity.Mathematics;
using MyBox;

namespace Celezt.Mathematics
{
    public static class cmath
    {
        /// <summary>
        /// Calculate the Manhattan distance between two points in 3D-space.
        /// </summary>
        /// <param name="value1">int3.</param>
        /// <param name="value2">int3.</param>
        /// <returns>Manhattan distance.</returns>
        public static int ManhattanDistance(this int3 value1, int3 value2)
        {
            int x = math.abs(value2.x - value1.x);
            int y = math.abs(value2.y - value1.y);
            int z = math.abs(value2.z - value1.z);

            return x + y + z;
        }

        /// <summary>
        /// Clamp a value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <returns>Clamped value.</returns>
        public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max) => new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );

        /// <summary>
        /// Clamp a value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <returns>Clamped value.</returns>
        public static Vector3 Clamp(this Vector3 value, uint3 min, uint3 max) => new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );

        /// <summary>
        /// Clamp a value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <returns>Clamped value.</returns>
        public static Vector3 Clamp(this Vector3 value, float3 min, float3 max) => new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );

        /// <summary>
        /// Clamp a value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <returns>Clamped value.</returns>
        public static Vector3 Clamp(this float3 value, uint3 min, uint3 max) => new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );

        /// <summary>
        /// Clamp a value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <returns>Clamped value.</returns>
        public static Vector3 Clamp(this float3 value, int3 min, int3 max) => new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );

        /// <summary>
        /// Clamp a value.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <returns>Clamped value.</returns>
        public static Vector3 Clamp(this float3 value, float3 min, float3 max) => new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z)
            );

        /// <summary>
        /// Map value from a range to another.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="range">From range.</param>
        /// <param name="toRange">To range.</param>
        /// <returns>Mapped value.</returns>
        public static float Map(this float value, MinMaxFloat range, MinMaxFloat toRange) =>
            (value - range.Min) / (range.Max - range.Min) * (toRange.Max - toRange.Min) + toRange.Min;

        /// <summary>
        /// Map value from a range to another.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="range">From range.</param>
        /// <param name="toRange">To range.</param>
        /// <returns>Mapped value.</returns>
        public static double Map(this double value, MinMaxFloat range, MinMaxFloat toRange) =>
            (value - range.Min) / (range.Max - range.Min) * (toRange.Max - toRange.Min) + toRange.Min;

        /// <summary>
        /// Flatten a int3 into int.
        /// </summary>
        /// <param name="value">To flatten.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int.</returns>
        public static int flat(this int3 value, int3 size) => value.x + value.y * size.x + value.z * size.x * size.y;
        /// <summary>
        /// Flatten a int3 into int.
        /// </summary>
        /// <param name="value">To flatten.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int.</returns>
        public static int flat(this int3 value, uint3 size) => (int)(value.x + value.y * size.x + value.z * size.x * size.y);
        /// <summary>
        /// Flatten a uint3 into int.
        /// </summary>
        /// <param name="value">To flatten.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int.</returns>
        public static int flat(this uint3 value, int3 size) => (int)(value.x + value.y * size.x + value.z * size.x * size.y);
        /// <summary>
        /// Flatten a uint3 into int.
        /// </summary>
        /// <param name="value">To flatten.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int.</returns>
        public static int flat(this uint3 value, uint3 size) => (int)(value.x + value.y * size.x + value.z * size.x * size.y);
        /// <summary>
        /// Flatten a float3 into int.
        /// </summary>
        /// <param name="value">To flatten.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int.</returns>
        public static int flat(this float3 value, int3 size) => (int)(value.x + value.y * size.x + value.z * size.x * size.y);
        /// <summary>
        /// Flatten a float3 into int.
        /// </summary>
        /// <param name="value">To flatten.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int.</returns>
        public static int flat(this float3 value, float3 size) => (int)(value.x + value.y * size.x + value.z * size.x * size.y);

        /// <summary>
        /// Volumify a int into int3.
        /// </summary>
        /// <param name="value">To volumify.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int3.</returns>
        public static int3 volume(this int value, int3 size)
        {
            int x = value % size.x;
            int y = (value / size.x) % size.y;
            int z = value / (size.x * size.y);

            return new int3(x, y, z);
        }

        /// <summary>
        /// Volumify a int into int3.
        /// </summary>
        /// <param name="value">To volumify.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int3.</returns>
        public static int3 volume(this int value, uint3 size)
        {
            int3 newSize = (int3)size;
            int x = value % newSize.x;
            int y = (value / newSize.x) % newSize.y;
            int z = value / (newSize.x * newSize.y);

            return new int3(x, y, z);
        }

        /// <summary>
        /// Volumify a short into int3.
        /// </summary>
        /// <param name="value">To volumify.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int3.</returns>
        public static int3 volume(this short value, uint3 size)
        {
            int3 newSize = (int3)size;
            int x = value % newSize.x;
            int y = (value / newSize.x) % newSize.y;
            int z = value / (newSize.x * newSize.y);

            return new int3(x, y, z);
        }

        /// <summary>
        /// Volumify a short into int3.
        /// </summary>
        /// <param name="value">To volumify.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int3.</returns>
        public static int3 volume(this ushort value, uint3 size)
        {
            int3 newSize = (int3)size;
            int x = value % newSize.x;
            int y = (value / newSize.x) % newSize.y;
            int z = value / (newSize.x * newSize.y);

            return new int3(x, y, z);
        }

        /// <summary>
        /// Volumify a short into vector3.
        /// </summary>
        /// <param name="value">To volumify.</param>
        /// <param name="size">Volume of the 3D-space.</param>
        /// <returns>int3.</returns>
        public static Vector3 volumef(this short value, uint3 size)
        {
            int3 newSize = (int3)size;
            int x = value % newSize.x;
            int y = (value / newSize.x) % newSize.y;
            int z = value / (newSize.x * newSize.y);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Round float value to the closest integer.
        /// </summary>
        /// <param name="x">To convert.</param>
        /// <returns>Rounded value.</returns>
        public static int roundint(float x) => Convert.ToInt32(x);

        /// <summary>
        /// Round float3's values to the closest integer.
        /// </summary>
        /// <param name="x">To convert.</param>
        /// <returns>Rounded value.</returns>
        public static int3 roundfloat3(float3 x) => new int3(
            Convert.ToInt32(x.x),
            Convert.ToInt32(x.y),
            Convert.ToInt32(x.z));
    }
}
