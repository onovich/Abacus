namespace MortiseFrame.Abacus {

    public struct FVector4 {

        public float x, y, z, w;

        public FVector4(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static FVector4 operator +(FVector4 a, FVector4 b) => new FVector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static FVector4 operator -(FVector4 a, FVector4 b) => new FVector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static FVector4 operator *(FVector4 a, float b) => new FVector4(a.x * b, a.y * b, a.z * b, a.w * b);
        public static FVector4 operator *(float a, FVector4 b) => new FVector4(a * b.x, a * b.y, a * b.z, a * b.w);
        public static FVector4 operator /(FVector4 a, float b) => new FVector4(a.x / b, a.y / b, a.z / b, a.w / b);
        public static FVector4 operator -(FVector4 a) => new FVector4(-a.x, -a.y, -a.z, -a.w);

        public static bool operator ==(FVector4 a, FVector4 b) => a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        public static bool operator !=(FVector4 a, FVector4 b) => !(a == b);
        public override bool Equals(object obj) => obj is FVector4 v && this == v;
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();

        public override string ToString() => $"({x}, {y}, {z}, {w})";
        public float Magnitude() => (float)System.Math.Sqrt(x * x + y * y + z * z + w * w);
        public float magnitude => Magnitude();
        public FVector4 Normalize() {
            float m = Magnitude();
            if (m > 1E-05f) {
                return this / m;
            } else {
                return zero;
            }
        }
        public FVector4 normalized => Normalize();
        public float SqrMagnitude() => x * x + y * y + z * z + w * w;
        public float sqrMagnitude => SqrMagnitude();
        public static float Dot(FVector4 a, FVector4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static FVector4 Lerp(FVector4 a, FVector4 b, float t) => a + (b - a) * t;

        public static FVector4 zero => new FVector4(0, 0, 0, 0);
        public static FVector4 one => new FVector4(1, 1, 1, 1);

    }

}