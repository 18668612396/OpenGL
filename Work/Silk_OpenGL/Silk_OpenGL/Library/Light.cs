using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Silk_OpenGL
{
    public class Light
    {
        public static Vector3 Position;
        public static Vector3 Direction;

        public struct Lighting
        {
            public static Vector3 Position;
            public static Vector3 Direction;
            public static Vector3 Color;
            public static float Attenuation;
        };

        public static void UpdateLight()
        {
        }

        public static Light Directional()
        {
            Lighting lighting;


            return null;
        }

        public static Light Point()
        {
            return null;
        }
    }
}