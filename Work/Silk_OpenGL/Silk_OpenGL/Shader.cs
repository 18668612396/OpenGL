using System;
using Silk.NET.OpenGL;
using System.IO;
using System.Numerics;


namespace Silk_OpenGL
{
    public class Shader
    {
        public uint program;
        public unsafe void LoadShader(GL Gl)
        {
            string vertexSource = File.ReadAllText("D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/shader.vert");
            string fragmentSource = File.ReadAllText("D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/shader.frag");
            
            uint vertexShader = Gl.CreateShader(ShaderType.VertexShader); //创建Shadeer
            Gl.ShaderSource(vertexShader, vertexSource); //将Shader源代码放进shader内
            Gl.CompileShader(vertexShader); //编译shader

            uint fragmentShader = Gl.CreateShader(ShaderType.FragmentShader); //创建Shadeer
            Gl.ShaderSource(fragmentShader, fragmentSource); //将Shader源代码放进shader内
            Gl.CompileShader(fragmentShader); //编译shader

            program = Gl.CreateProgram(); //创建ShaderProgram
            Gl.AttachShader(program, vertexShader); //AttachShader
            Gl.AttachShader(program, fragmentShader); //AttachShader
            Gl.LinkProgram(program); //将自定义Shader绑定到渲染管线内

            //因为已经把Shader绑定到Program了，那么在当前这一帧需要把他们卸载掉
            Gl.DetachShader(program, vertexShader); //释放Shader
            Gl.DetachShader(program, fragmentShader); //释放Shader
            Gl.DeleteShader(vertexShader); //释放顶点着色器
            Gl.DeleteShader(fragmentShader); //释放片元着色器

            //绑定相关的
            Gl.VertexAttribPointer(0, 3, GLEnum.Float, false, 8 * sizeof(float), (void*) 0);
            Gl.EnableVertexAttribArray(0);
            Gl.VertexAttribPointer(1, 3, GLEnum.Float, false, 8 * sizeof(float), (void*)(3 * sizeof(float)));
            Gl.EnableVertexAttribArray(1);
            Gl.VertexAttribPointer(2, 2, GLEnum.Float, false, 8 * sizeof(float), (void*)(6 * sizeof(float)));
            Gl.EnableVertexAttribArray(2);
        }

        public void Run(GL Gl)
        {
            Gl.UseProgram(program);
        }

        public void Dispose(GL Gl)
        {
            Gl.DeleteProgram(program);
        }

        public void SetUniform(GL Gl,string name, float value)
        {
            int location = Gl.GetUniformLocation(program, name);
            Gl.Uniform1(location,value);
        }
        
        public void UniformTexture2D(GL Gl,uint texture, string name,int textureUnit)
        {
            int location = Gl.GetUniformLocation(program, name);
            Gl.ActiveTexture(TextureUnit.Texture0 + textureUnit);
            Gl.BindTexture(TextureTarget.Texture2D,texture);
            Gl.Uniform1(location,textureUnit);
        }
        
       public unsafe void Transform(GL Gl)
        {
            int modelLocation = Gl.GetUniformLocation(program, "ModelMatrix");
            
            Matrix4x4 Model = Matrix4x4.CreateTranslation(-0.5f  ,0,0);
            Gl.UniformMatrix4(modelLocation,1,false,(float*) &Model);
            
            int viewLocation = Gl.GetUniformLocation(program, "RotationMatrix");
            Matrix4x4 rotation = Matrix4x4.CreateRotationZ(45f);
            Gl.UniformMatrix4(viewLocation,1,false,(float*) &rotation);

            
        }
    }
}