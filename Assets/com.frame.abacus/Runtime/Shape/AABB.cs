using System;
using System.Numerics;

namespace MortiseFrame.Abacus {

    public class AABB {

        Vector2 min;
        public Vector2 Min => min;
        public void SetMin(Vector2 value) => min = value;
        Vector2 max;
        public Vector2 Max => max;
        public void SetMax(Vector2 value) => max = value;
        Vector2 center;
        public Vector2 Center => center;
        Vector2 size;
        public Vector2 Size => size;

        public AABB(Vector2 min, Vector2 max) {
            this.min = min;
            this.max = max;
            this.center = (min + max) / 2;
            this.size = max - min;
        }

        public bool Contains(Vector2 point) {
            return point.X >= min.X && point.X <= max.X && point.Y >= min.Y && point.Y <= max.Y;
        }

        public bool Intersects(AABB other) {
            return other.min.X <= max.X && other.max.X >= min.X && other.min.Y <= max.Y && other.max.Y >= min.Y;
        }

        public bool Intersects(Circle other) {
            float x = MathF.Max(min.X, MathF.Min(other.center.X, max.X));
            float y = MathF.Max(min.Y, MathF.Min(other.center.Y, max.Y));
            float distance = MathF.Sqrt((x - other.center.X) * (x - other.center.X) + (y - other.center.Y) * (y - other.center.Y));
            return distance < other.radius;
        }



    }

}

