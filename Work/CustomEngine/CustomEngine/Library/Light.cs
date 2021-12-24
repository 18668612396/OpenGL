using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Silk.NET.Maths;
using Silk.NET.OpenGL;

namespace Silk_OpenGL
{
    public class Light
    {
        public static Vector3 Position;
        public static Vector3 Rotation;
        public static Vector3 Direction;
        public static Vector3 Color;
        public static float Attenuation;

        public static void UpdateLight()
        {
            Direction = new Vector3(0.0f, 0.0f, 1.0f);
            Direction = Vector3.Transform(new Vector3(0.0f, 0.0f, 0.0f), Transform.Rotation);
            Direction *= -1.0f;
        }

        public static void UpdateUniformValue(GL Gl)
        {
            Gl.Uniform3(Gl.GetUniformLocation(Shader.program,"LightPos"),Position);
            Gl.Uniform3(Gl.GetUniformLocation(Shader.program,"LightDirection"),Direction);
            Gl.Uniform3(Gl.GetUniformLocation(Shader.program,"LightColor"),Color);
        }
        public Light(Vector3 position, Vector3 rotation, Vector3 color)
        {
            Position = position;
            Rotation = rotation;
            Color = color;
            UpdateLight();
        }


        public Light Point()
        {
            return null;
        }
    }
}