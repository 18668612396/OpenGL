﻿using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Numerics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Vector3 = OpenTK.Mathematics.Vector3;


namespace OpenGL_CSharp
{
    public class Shader
    {
        #region ShaderSource

        public OpenTK.Graphics.ProgramHandle shaderProgram;

        //定义vertexShader变量
        private OpenTK.Graphics.ShaderHandle vertexShader;
        private OpenTK.Graphics.ShaderHandle fragmentShader;

        #endregion


        public Shader(string vertexPath, string fragmentPath)
        {
            if (!File.Exists(vertexPath))
            {
                if (!File.Exists(fragmentPath))
                {
                    Console.WriteLine("shader加载失败啦！！");
                }
            }

            //读取顶点着色器完整内容
            var vertexSource = File.ReadAllText(vertexPath);
            //读取片元着色器完整内容
            var fragmentSource = File.ReadAllText(fragmentPath);

            //创建一个顶点着色器返还给我们定义好的Shader变量
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexSource); //将shader代码给定到vertexShader变量内
            GL.CompileShader(vertexShader); //编译顶点着色器

            //创建一个顶点着色器返还给我们定义好的Shader变量
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentSource); //将shader代码给定到FragmentShader变量内
            GL.CompileShader(fragmentShader); //编译顶点着色器

            shaderProgram = GL.CreateProgram(); //创建Program //这个相当于渲染管线的片段或者说是插槽，里面装载了一些我们需要的shader，然后填充进渲染管线里
            GL.AttachShader(shaderProgram, vertexShader); //添加顶点着色器到shaderProgram
            GL.AttachShader(shaderProgram, fragmentShader); //添加片元着色器到shaderProgram
            GL.LinkProgram(shaderProgram); //将Program填充进渲染管线内

            //获取顶点数据
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            //获取顶点色数据
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            //获取UV数据
            GL.VertexAttribPointer(15, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(15);

            
            

            
        }

        public void OnMatrix(Shader shader,Matrix4d matrix)
        {

            GL.UniformMatrix4d(GL.GetUniformLocation(shader.shaderProgram,"trans"),true,matrix);
            
         
        }

        protected virtual void Dispose(bool disposing)
        {
            bool disposeValue = false;

            if (!disposeValue)
            {
                GL.DeleteProgram(shaderProgram);
                disposeValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(shaderProgram);
        }

        public void Dispose()
        {
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Use()
        {

            GL.UseProgram(shaderProgram); //使用shaderProgram插入渲染管线内
        }
    }
}