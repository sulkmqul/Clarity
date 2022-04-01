using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    public static class NumericsExtension
    {
        public static float GetIndex(this System.Numerics.Vector2 v, int i)
        {
            switch (i)
            {
                case 0:
                    return v.X;
                case 1:
                    return v.Y;
                default:
                    throw new ArgumentOutOfRangeException("Vector2 range 0 .. 1");
            }
        }

        public static void SetIndex(ref this System.Numerics.Vector2 v, int i, float val)
        {
            switch (i)
            {
                case 0:
                    v.X = val;
                    return;
                case 1:
                    v.Y = val;
                    return;
                default:
                    throw new ArgumentOutOfRangeException("Vector2 range 0 .. 1");
            }
        }



        public static float GetIndex(this System.Numerics.Vector3 v, int i)
        {
            switch (i)
            {
                case 0:
                    return v.X;
                case 1:
                    return v.Y;
                case 2:
                    return v.Z;
                default:
                    throw new ArgumentOutOfRangeException("Vector3 range 0 .. 2");
            }
        }

        public static void SetIndex(ref this System.Numerics.Vector3 v, int i, float val)
        {
            switch (i)
            {
                case 0:
                    v.X = val;
                    return;
                case 1:
                    v.Y = val;
                    return;
                case 2:
                    v.Z = val;
                    return;
                default:
                    throw new ArgumentOutOfRangeException("Vector3 range 0 .. 2");
            }
        }



        public static float GetIndex(this System.Numerics.Vector4 v, int i)
        {
            switch (i)
            {
                case 0:
                    return v.X;
                case 1:
                    return v.Y;
                case 2:
                    return v.Z;
                case 3:
                    return v.W;
                default:
                    throw new ArgumentOutOfRangeException("Vector4 range 0 .. 3");
            }
        }

        public static void SetIndex(ref this System.Numerics.Vector4 v, int i, float val)
        {
            switch (i)
            {
                case 0:
                    v.X = val;
                    return;
                case 1:
                    v.Y = val;
                    return;
                case 2:
                    v.Z = val;
                    return;
                case 3:
                    v.W = val;
                    return;
                default:
                    throw new ArgumentOutOfRangeException("Vector4 range 0 .. 3");
            }
        }
    }
}
