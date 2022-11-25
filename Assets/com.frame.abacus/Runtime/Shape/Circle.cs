using System;
using System.Numerics;

namespace MortiseFrame.Abacus {

    public class Circle {

        public Vector2 center;
        public float radius;

        public Circle(Vector2 center, float radius) {
            this.center = center;
            this.radius = radius;
        }

        public bool Contains(Vector2 point) {
            return (point - center).Length() < radius;
        }

        public bool Intersects(Circle other) {
            return (other.center - center).Length() < radius + other.radius;
        }

        public bool Intersects(AABB other) {
            float x = MathF.Max(other.Min.X, MathF.Min(center.X, other.Max.X));
            float y = MathF.Max(other.Min.Y, MathF.Min(center.Y, other.Max.Y));
            float distance = MathF.Sqrt((x - center.X) * (x - center.X) + (y - center.Y) * (y - center.Y));
            return distance < radius;
        }


    }

}

