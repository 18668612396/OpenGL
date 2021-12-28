﻿using System;
using Silk.NET.OpenGL;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Silk_OpenGL
{
    public class Shader
    {
        public uint program;

        public unsafe Shader(GL Gl, string path)
        {
            //获取顶点着色器的代码段
            var shaderSource = File.ReadAllText(path);
            Regex vertexRegex = new Regex("(?<=(" + "#VERTEX" + "))[.\\s\\S]*?(?=(" + "#VERTEND" + "))",
                RegexOptions.Multiline | RegexOptions.Singleline);
            var vertexSource = vertexRegex.Match(shaderSource).Value;

            //获取片元着色器的代码段
            Regex fragmentRegex = new Regex("(?<=(" + "#FRAGMENT" + "))[.\\s\\S]*?(?=(" + "#FRAGEND" + "))",
                RegexOptions.Multiline | RegexOptions.Singleline);
            var fragmentSource = fragmentRegex.Match(shaderSource).Value;
            ;


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


        public unsafe void UpdataGlobalValue(GL Gl, Camera OnCanera)
        {
            //MVP变换
            Matrix4x4 model = Matrix4x4.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f)) * Matrix4x4.Identity *
                              Matrix4x4.CreateScale(1.0f); //这里一定要把三个变换都乘上 哪怕没有数值
            Gl.UniformMatrix4(Gl.GetUniformLocation(program, "Matrix_ObjectToWorld"), 1, false, (float*) &model);
            Matrix4x4 view = OnCanera.GetViewMatrix;
            Gl.UniformMatrix4(Gl.GetUniformLocation(program, "Matrix_WorldToView"), 1, false, (float*) &view);
            Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(OnCanera.Radians(OnCanera.FieldOfView),
                400 / 300, OnCanera.ClippingPlanes.X, OnCanera.ClippingPlanes.Y);
            Gl.UniformMatrix4(Gl.GetUniformLocation(program, "Matrix_ViewToHClip"), 1, false, (float*) &projection);

            Gl.Uniform3(Gl.GetUniformLocation(program, "LightPos"), new Vector3(10.0f, 10.0f, 10.0f));
            Gl.Uniform3(Gl.GetUniformLocation(program, "_WorldSpaceCameraPos"), OnCanera.Camera_Position);
        }

        public void Run(GL Gl)
        {
            Gl.UseProgram(program);
        }

        public void Dispose(GL Gl)
        {
            Gl.DeleteProgram(program);
        }

        public void UniformTexture2D(GL Gl, uint texture, string name, int textureUnit)
        {
            var location = Gl.GetUniformLocation(program, name);
            Gl.ActiveTexture(TextureUnit.Texture0 + textureUnit);
            Gl.BindTexture(TextureTarget.Texture2D, texture);
            Gl.Uniform1(location, textureUnit);
        }
    }
}