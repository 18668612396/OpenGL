using System;
using Silk.NET.OpenGL;
using System.IO;
using System.Numerics;
using System.Runtime;

namespace Silk_OpenGL
{
    public class Transform
    {
        public unsafe static void UpdataTransform(GL Gl)
        {
            var modelLocation = Gl.GetUniformLocation(Shader.program, "Matrix_ObjectToWorld");
            //这里一定要把三个变换都乘上 哪怕没有数值
            Matrix4x4 model =  Matrix4x4.CreateTranslation(new Vector3(0.0f,0.0f,0.0f)) * Matrix4x4.Identity * Matrix4x4.CreateScale(1.0f) ;
            Gl.UniformMatrix4(modelLocation,1,false,(float*) &model);
            
            var viewLocation = Gl.GetUniformLocation(Shader.program, "Matrix_WorldToView");
            Matrix4x4 view = Camera.GetViewMatrix;
            Gl.UniformMatrix4(viewLocation,1,false,(float*) &view);
            
            var projectionLocation = Gl.GetUniformLocation(Shader.program, "Matrix_ViewToHClip");
            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(Radians(Camera.FieldOfView), 400 / 300,Camera.ClippingPlanes.X,Camera.ClippingPlanes.Y);
            Gl.UniformMatrix4(projectionLocation,1,false,(float*) &projection);
        }
        
        private static float Radians(float value)
        {
            return MathF.PI / 180 * value;
        }
    }
}