using System;

namespace MortiseFrame.Abacus {

    public struct FMatrix4x4 {

        public float m00, m01, m02, m03;
        public float m10, m11, m12, m13;
        public float m20, m21, m22, m23;
        public float m30, m31, m32, m33;

        public FMatrix4x4(FVector4 row1, FVector4 row2, FVector4 row3, FVector4 row4) {
            m00 = row1.x; m01 = row1.y; m02 = row1.z; m03 = row1.w;
            m10 = row2.x; m11 = row2.y; m12 = row2.z; m13 = row2.w;
            m20 = row3.x; m21 = row3.y; m22 = row3.z; m23 = row3.w;
            m30 = row4.x; m31 = row4.y; m32 = row4.z; m33 = row4.w;
        }

        public static FVector4 MultiplyVector(FMatrix4x4 m, FVector4 v) {
            return new FVector4(
                m.m00 * v.x + m.m01 * v.y + m.m02 * v.z + m.m03 * v.w,
                m.m10 * v.x + m.m11 * v.y + m.m12 * v.z + m.m13 * v.w,
                m.m20 * v.x + m.m21 * v.y + m.m22 * v.z + m.m23 * v.w,
                m.m30 * v.x + m.m31 * v.y + m.m32 * v.z + m.m33 * v.w
            );
        }

        public static FMatrix4x4 Multiply(FMatrix4x4 a, FMatrix4x4 b) {
            FMatrix4x4 result = new FMatrix4x4();
            for (int row = 0; row < 4; row++) {
                for (int col = 0; col < 4; col++) {
                    result[row, col] = a[row, 0] * b[0, col] + a[row, 1] * b[1, col] +
                                       a[row, 2] * b[2, col] + a[row, 3] * b[3, col];
                }
            }
            return result;
        }

        public float this[int row, int column] {
            get {
                return row switch {
                    0 => column switch { 0 => m00, 1 => m01, 2 => m02, 3 => m03, _ => throw new IndexOutOfRangeException() },
                    1 => column switch { 0 => m10, 1 => m11, 2 => m12, 3 => m13, _ => throw new IndexOutOfRangeException() },
                    2 => column switch { 0 => m20, 1 => m21, 2 => m22, 3 => m23, _ => throw new IndexOutOfRangeException() },
                    3 => column switch { 0 => m30, 1 => m31, 2 => m32, 3 => m33, _ => throw new IndexOutOfRangeException() },
                    _ => throw new IndexOutOfRangeException(),
                };
            }
            set {
                switch (row) {
                    case 0: switch (column) { case 0: m00 = value; break; case 1: m01 = value; break; case 2: m02 = value; break; case 3: m03 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    case 1: switch (column) { case 0: m10 = value; break; case 1: m11 = value; break; case 2: m12 = value; break; case 3: m13 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    case 2: switch (column) { case 0: m20 = value; break; case 1: m21 = value; break; case 2: m22 = value; break; case 3: m23 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    case 3: switch (column) { case 0: m30 = value; break; case 1: m31 = value; break; case 2: m32 = value; break; case 3: m33 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

    }

}