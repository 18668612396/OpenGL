using System;
using Silk.NET.OpenGL;
using System.IO;
using System.Numerics;
using System.Runtime;

namespace Silk_OpenGL
{
    public class Transform
    {
        public unsafe void UpdataTransform(GL Gl,uint ShaderProgram,Camera camera)
        {
            var modelLocation = Gl.GetUniformLocation(ShaderProgram, "Matrix_ObjectToWorld");
            //这里一定要把三个变换都乘上 哪怕没有数值
            Matrix4x4 model =  Matrix4x4.CreateTranslation(new Vector3(0.0f,0.0f,0.0f)) * Matrix4x4.Identity * Matrix4x4.CreateScale(1.0f) ;
            Gl.UniformMatrix4(modelLocation,1,false,(float*) &model);
            
            var viewLocation = Gl.GetUniformLocation(ShaderProgram, "Matrix_WorldToView");
            Matrix4x4 view = camera.GetViewMatrix;
            Gl.UniformMatrix4(viewLocation,1,false,(float*) &view);
            
            var projectionLocation = Gl.GetUniformLocation(ShaderProgram, "Matrix_ViewToHClip");
            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(Radians(camera.FieldOfView), 400 / 300,camera.ClippingPlanes.X,camera.ClippingPlanes.Y);
            Gl.UniformMatrix4(projectionLocation,1,false,(float*) &projection);
        }
        
        private static float Radians(float value)
        {
            return MathF.PI / 180 * value;
        }
    }
}