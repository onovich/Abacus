namespace MortiseFrame.Abacus {

    public struct FVector3 {

        public float x, y, z;

        public FVector3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static FVector3 operator +(FVector3 a, FVector3 b) {
            return new FVector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static FVector3 operator -(FVector3 a, FVector3 b) {
            return new FVector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static FVector3 operator *(FVector3 a, float b) {
            return new FVector3(a.x * b, a.y * b, a.z * b);
        }

        public static FVector3 operator *(float a, FVector3 b) {
            return new FVector3(a * b.x, a * b.y, a * b.z);
        }

        public static FVector3 operator /(FVector3 a, float b) {
            return new FVector3(a.x / b, a.y / b, a.z / b);
        }

        public static FVector3 operator -(FVector3 a) {
            return new FVector3(-a.x, -a.y, -a.z);
        }

        public static bool operator ==(FVector3 a, FVector3 b) {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(FVector3 a, FVector3 b) {
            return !(a == b);
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType()) return false;
            FVector3 v = (FVector3)obj;
            return x == v.x && y == v.y && z == v.z;
        }

        public override int GetHashCode() {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        public override string ToString() {
            return $"({x}, {y}, {z})";
        }

        public static FVector3 zero = new FVector3(0, 0, 0);
        public static FVector3 one = new FVector3(1, 1, 1);
        public static FVector3 up = new FVector3(0, 1, 0);
        public static FVector3 down = new FVector3(0, -1, 0);
        public static FVector3 left = new FVector3(-1, 0, 0);
        public static FVector3 right = new FVector3(1, 0, 0);
        public static FVector3 forward = new FVector3(0, 0, 1);
        public static FVector3 back = new FVector3(0, 0, -1);

        public static float Distance(FVector3 a, FVector3 b) {
            return (a - b).Magnitude();
        }

        public float Magnitude() {
            return (float)System.Math.Sqrt(x * x + y * y + z * z);
        }
        public float magnitude => Magnitude();

        public FVector3 Normalize() {
            float m = Magnitude();
            if (m > 1E-05f) {
                this = this / m;
            } else {
                this = zero;
            }
            return this;
        }

        public FVector3 normalized => Normalize();

        public float SqrMagnitude() {
            return x * x + y * y + z * z;
        }

        public float sqrMagnitude => SqrMagnitude();

        public static float Dot(FVector3 a, FVector3 b) {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static FVector3 Lerp(FVector3 a, FVector3 b, float t) {
            return a + (b - a) * t;
        }

        public static FVector3 Max(FVector3 a, FVector3 b) {
            return new FVector3(System.Math.Max(a.x, b.x), System.Math.Max(a.y, b.y), System.Math.Max(a.z, b.z));
        }

        public static FVector3 Min(FVector3 a, FVector3 b) {
            return new FVector3(System.Math.Min(a.x, b.x), System.Math.Min(a.y, b.y), System.Math.Min(a.z, b.z));
        }

        public static FVector3 Scale(FVector3 a, FVector3 b) {
            return new FVector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static FVector3 Reflect(FVector3 inDirection, FVector3 inNormal) {
            return inDirection - 2 * Dot(inDirection, inNormal) * inNormal;
        }

        public static FVector3 Cross(FVector3 a, FVector3 b) {
            return new FVector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

    }

}