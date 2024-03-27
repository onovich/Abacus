using System;

namespace MortiseFrame.Abacus {

    public struct FColor : IEquatable<FColor> {

        public float r, g, b, a;

        public FColor(float r, float g, float b, float a = 1.0f) {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public float this[int index] {
            get {
                switch (index) {
                    case 0: return r;
                    case 1: return g;
                    case 2: return b;
                    case 3: return a;
                    default: throw new IndexOutOfRangeException("Invalid FColor index!");
                }
            }
            set {
                switch (index) {
                    case 0: r = value; break;
                    case 1: g = value; break;
                    case 2: b = value; break;
                    case 3: a = value; break;
                    default: throw new IndexOutOfRangeException("Invalid FColor index!");
                }
            }
        }

        public static readonly FColor yellow = new FColor(1f, 0.92f, 0.016f, 1f);
        public static readonly FColor clear = new FColor(0f, 0f, 0f, 0f);
        public static readonly FColor grey = new FColor(0.5f, 0.5f, 0.5f, 1f);
        public static readonly FColor gray = grey;
        public static readonly FColor magenta = new FColor(1f, 0f, 1f, 1f);
        public static readonly FColor cyan = new FColor(0f, 1f, 1f, 1f);
        public static readonly FColor red = new FColor(1f, 0f, 0f, 1f);
        public static readonly FColor black = new FColor(0f, 0f, 0f, 1f);
        public static readonly FColor white = new FColor(1f, 1f, 1f, 1f);
        public static readonly FColor blue = new FColor(0f, 0f, 1f, 1f);
        public static readonly FColor green = new FColor(0f, 1f, 0f, 1f);

        public static FColor Lerp(FColor a, FColor b, float t) {
            t = Math.Clamp(t, 0f, 1f);
            return new FColor(
                a.r + (b.r - a.r) * t,
                a.g + (b.g - a.g) * t,
                a.b + (b.b - a.b) * t,
                a.a + (b.a - a.a) * t
            );
        }

        public static FColor HSVToRGB(float H, float S, float V, bool hdr = false) {
            if (S == 0f) {
                return new FColor(V, V, V, 1f);
            }

            if (V == 0f) {
                return new FColor(0f, 0f, 0f, 1f);
            }

            H = Math.Clamp(H, 0f, 1f) * 6f;
            S = Math.Clamp(S, 0f, 1f);
            V = Math.Clamp(V, 0f, hdr ? float.MaxValue : 1f);

            int i = (int)Math.Floor(H);
            float f = H - i;
            float p = V * (1f - S);
            float q = V * (1f - S * f);
            float t = V * (1f - S * (1f - f));

            switch (i) {
                case 0: return new FColor(V, t, p, 1f);
                case 1: return new FColor(q, V, p, 1f);
                case 2: return new FColor(p, V, t, 1f);
                case 3: return new FColor(p, q, V, 1f);
                case 4: return new FColor(t, p, V, 1f);
                case 5: return new FColor(V, p, q, 1f);
                default: return new FColor(0f, 0f, 0f, 1f); // Should never happen
            }
        }

        public bool Equals(FColor other) {
            return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
        }

        public override bool Equals(object obj) {
            return obj is FColor other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = r.GetHashCode();
                hashCode = (hashCode * 397) ^ g.GetHashCode();
                hashCode = (hashCode * 397) ^ b.GetHashCode();
                hashCode = (hashCode * 397) ^ a.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(FColor left, FColor right) {
            return left.Equals(right);
        }

        public static bool operator !=(FColor left, FColor right) {
            return !(left == right);
        }

        public override string ToString() {
            return string.Format("R:{0} G:{1} B:{2} A:{3}", r, g, b, a);
        }
    }
}

