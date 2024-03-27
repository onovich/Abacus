using System;
using System.Globalization;

namespace MortiseFrame.Abacus {

    public struct FColor32 : IEquatable<FColor32>, IFormattable {

        public byte r, g, b, a;

        public FColor32(byte r, byte g, byte b, byte a = 255) {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public byte this[int index] {
            get {
                switch (index) {
                    case 0: return r;
                    case 1: return g;
                    case 2: return b;
                    case 3: return a;
                    default: throw new IndexOutOfRangeException("Invalid FColor32 index!");
                }
            }
            set {
                switch (index) {
                    case 0: r = value; break;
                    case 1: g = value; break;
                    case 2: b = value; break;
                    case 3: a = value; break;
                    default: throw new IndexOutOfRangeException("Invalid FColor32 index!");
                }
            }
        }

        public override string ToString() => ToString(null, CultureInfo.InvariantCulture);

        public string ToString(string format, IFormatProvider formatProvider = null) {
            formatProvider ??= CultureInfo.InvariantCulture;
            return $"R:{r.ToString(format, formatProvider)} G:{g.ToString(format, formatProvider)} B:{b.ToString(format, formatProvider)} A:{a.ToString(format, formatProvider)}";
        }

        public bool Equals(FColor32 other) => r == other.r && g == other.g && b == other.b && a == other.a;

        public override bool Equals(object obj) => obj is FColor32 other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(r, g, b, a);

        public static bool operator ==(FColor32 left, FColor32 right) => left.Equals(right);

        public static bool operator !=(FColor32 left, FColor32 right) => !(left == right);

        public static explicit operator FColor(FColor32 c) => new FColor(c.r / 255f, c.g / 255f, c.b / 255f, c.a / 255f);

        public static implicit operator FColor32(FColor c) => new FColor32((byte)Math.Round(c.r * 255), (byte)Math.Round(c.g * 255), (byte)Math.Round(c.b * 255), (byte)Math.Round(c.a * 255));

    }

}