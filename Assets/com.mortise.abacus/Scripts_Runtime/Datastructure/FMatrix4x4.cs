using System;
using System.Runtime.InteropServices;

namespace MortiseFrame.Abacus {

    [StructLayout(LayoutKind.Sequential)]
    public struct FMatrix4x4 {

        public float m00, m10, m20, m30;
        public float m01, m11, m21, m31;
        public float m02, m12, m22, m32;
        public float m03, m13, m23, m33;

        public FMatrix4x4(FVector4 col1, FVector4 col2, FVector4 col3, FVector4 col4) {
            m00 = col1.x; m01 = col2.x; m02 = col3.x; m03 = col4.x;
            m10 = col1.y; m11 = col2.y; m12 = col3.y; m13 = col4.y;
            m20 = col1.z; m21 = col2.z; m22 = col3.z; m23 = col4.z;
            m30 = col1.w; m31 = col2.w; m32 = col3.w; m33 = col4.w;
        }

        public static FVector4 operator *(FMatrix4x4 m, FVector4 v) {
            return new FVector4(
                m.m00 * v.x + m.m01 * v.y + m.m02 * v.z + m.m03 * v.w,
                m.m10 * v.x + m.m11 * v.y + m.m12 * v.z + m.m13 * v.w,
                m.m20 * v.x + m.m21 * v.y + m.m22 * v.z + m.m23 * v.w,
                m.m30 * v.x + m.m31 * v.y + m.m32 * v.z + m.m33 * v.w
            );
        }

        public static FMatrix4x4 operator *(FMatrix4x4 a, FMatrix4x4 b) {
            FMatrix4x4 result = new FMatrix4x4();
            for (int col = 0; col < 4; col++) {
                for (int row = 0; row < 4; row++) {
                    result[row, col] = a[row, 0] * b[0, col] + a[row, 1] * b[1, col] +
                                       a[row, 2] * b[2, col] + a[row, 3] * b[3, col];
                }
            }
            return result;
        }

        public float this[int row, int column] {
            get {
                switch (column) {
                    case 0: return row switch { 0 => m00, 1 => m10, 2 => m20, 3 => m30, _ => throw new IndexOutOfRangeException() };
                    case 1: return row switch { 0 => m01, 1 => m11, 2 => m21, 3 => m31, _ => throw new IndexOutOfRangeException() };
                    case 2: return row switch { 0 => m02, 1 => m12, 2 => m22, 3 => m32, _ => throw new IndexOutOfRangeException() };
                    case 3: return row switch { 0 => m03, 1 => m13, 2 => m23, 3 => m33, _ => throw new IndexOutOfRangeException() };
                    default: throw new IndexOutOfRangeException();
                }
            }
            set {
                switch (column) {
                    case 0: switch (row) { case 0: m00 = value; break; case 1: m10 = value; break; case 2: m20 = value; break; case 3: m30 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    case 1: switch (row) { case 0: m01 = value; break; case 1: m11 = value; break; case 2: m21 = value; break; case 3: m31 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    case 2: switch (row) { case 0: m02 = value; break; case 1: m12 = value; break; case 2: m22 = value; break; case 3: m32 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    case 3: switch (row) { case 0: m03 = value; break; case 1: m13 = value; break; case 2: m23 = value; break; case 3: m33 = value; break; default: throw new IndexOutOfRangeException(); } break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
    }

}