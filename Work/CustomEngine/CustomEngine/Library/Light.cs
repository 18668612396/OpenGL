using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Silk_OpenGL
{
    public class Light
    {
        public  Vector3 Position;
        public  Vector3 Direction;
        public  Vector3 Color;
        public  float Attenuation;

        public static void UpdateLight()
        {
        }

        public  Light Directional()
        {
            Position = new Vector3(10.0f, 10.0f, 10.0f);

            return null;
        }

        public  Light Point()
        {
            return null;
        }
    }
}