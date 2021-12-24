using System;
using Silk.NET.OpenGL;
using System.IO;
using System.Numerics;

namespace Silk_OpenGL
{
    public class Shader
    {
        public  uint program;
        public  unsafe Shader(GL Gl,string path)
        {
            //获取顶点着色器的代码段
            var shaderSource = File.ReadAllText(path);
            var vertexBegin = shaderSource.IndexOf("#VERTEX");
            var vertexEnd = shaderSource.IndexOf("#VERTEND");
            var vertexSource = shaderSource.Substring(vertexBegin + 7, vertexEnd - 7);

            //获取片元着色器的代码段
            var fragmentBegin = shaderSource.IndexOf("#FRAGMENT");
            var fragmentEnd = shaderSource.IndexOf("#FRAGEND");
            var fragmentSource = shaderSource.Substring(fragmentBegin + 9, fragmentEnd - fragmentBegin - 9);
            //这个方法表明了 我从 （#FRAGMENT + 9）个字符串开始往后数(#FRAGEND - #FRAGMENT - 9)个字符

            var vertexShader = Gl.CreateShader(ShaderType.VertexShader); //创建Shadeer
            Gl.ShaderSource(vertexShader, vertexSource); //将Shader源代码放进shader内
            Gl.CompileShader(vertexShader); //编译shader

            var fragmentShader = Gl.CreateShader(ShaderType.FragmentShader); //创建Shadeer
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
            Gl.VertexAttribPointer(1, 3, GLEnum.Float, false, 8 * sizeof(float), (void*) (3 * sizeof(float)));
            Gl.EnableVertexAttribArray(1);
            Gl.VertexAttribPointer(2, 2, GLEnum.Float, false, 8 * sizeof(float), (void*) (6 * sizeof(float)));
            Gl.EnableVertexAttribArray(2);
        }

        
        public  void UpdataGlobalValue(GL Gl)
        {
            Gl.Uniform3(Gl.GetUniformLocation(program,"LightPos"),new Vector3(10.0f,10.0f,10.0f));
        }
        public  void Run(GL Gl)
        {
            Gl.UseProgram(program);
        }

        public  void Dispose(GL Gl)
        {
            Gl.DeleteProgram(program);
        }

        public  void SetUniform(GL Gl, string name, float value)
        {
            var location = Gl.GetUniformLocation(program, name);
            Gl.Uniform1(location, value);
        }

        public  void UniformTexture2D(GL Gl, uint texture, string name, int textureUnit)
        {
            var location = Gl.GetUniformLocation(program, name);
            Gl.ActiveTexture(TextureUnit.Texture0 + textureUnit);
            Gl.BindTexture(TextureTarget.Texture2D, texture);
            Gl.Uniform1(location, textureUnit);
        }
    }
}