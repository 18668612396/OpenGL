using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenGL_CSharp
{
    public class Game : GameWindow
    {
        public Game() :
            base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(400, 300)); //重定义Window大小
        }

        //调整窗口大小时调用
        protected override void OnResize(ResizeEventArgs e)
        {
            //GL.Viewport  1:中心坐标偏移x 2:中心坐标偏移y 3:屏幕宽度 4:屏幕高度
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }


        private OpenTK.Graphics.BufferHandle vao; //vertex Array Object
        private OpenTK.Graphics.ProgramHandle shaderProgramHandle;

        private OpenTK.Graphics.VertexArrayHandle vertexArrayHandle;

        //窗口开启时调用
        protected override void OnLoad()
        {
            //清除后的背景颜色，我只想让他调用一次，所以放在OnLoad里使用
            GL.ClearColor(new Color4<Rgba>(0.3f, 0.4f, 0.5f, 0.6f));
            float[] vertices = new float[]
            {
                -0.5f, -0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                0.0f, 0.5f, 0.0f
            };
            this.vao = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, vao);
            GL.BufferData(BufferTargetARB.ArrayBuffer, vertices.Length * sizeof(float), vertices[0],
                BufferUsageARB.StaticDraw);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, (OpenTK.Graphics.BufferHandle) 0);

            this.vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayHandle);
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, this.vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray((OpenTK.Graphics.VertexArrayHandle) 0);
            string vertexShaderrSource =
                @"
                #version 330 core
               layout(location = 0) in vec3 aPos;
                void main()
                {
                 gl_Position  = vec4(aPos,1.0f);
                }";
            
            string fragmentShaderSource =
                @"
                 #version 330
                    out vec4 outputColor;
                void main()
                    {
                     outputColor = vec4(1.0, 1.0, 0.0, 1.0);
                }";
            OpenTK.Graphics.ShaderHandle vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle,vertexShaderrSource);
            GL.CompileShader(vertexShaderHandle);
            
            OpenTK.Graphics.ShaderHandle fragmentShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(fragmentShaderHandle,fragmentShaderSource);
            GL.CompileShader(fragmentShaderHandle);

            
            GL.AttachShader(this.shaderProgramHandle,vertexShaderHandle);
            GL.AttachShader(this.shaderProgramHandle,fragmentShaderHandle);
            GL.LinkProgram(shaderProgramHandle);
            
            GL.DetachShader(this.shaderProgramHandle,vertexShaderHandle);
            GL.DetachShader(this.shaderProgramHandle,fragmentShaderHandle);
            
            //删除shader
            // GL.DetachShader(vertexShaderHandle);
            // GL.DetachShader(fragmentShaderHandle);
            
            
            base.OnLoad();
        }

        //窗口关闭时调用
        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTargetARB.ParameterBuffer,(OpenTK.Graphics.BufferHandle)0);//绑定VAO为0！！！！
            GL.DeleteBuffer(this.vao);//释放VAO
            
            GL.UseProgram((OpenTK.Graphics.ProgramHandle)0);//使用Program 0！！！！
            GL.DeleteProgram(this.shaderProgramHandle);//释放ShaderProgram
            
            base.OnUnload();
        }

//每一帧进行调用
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); //使用 OnLoad 中设置的颜色清除屏幕
            GL.UseProgram(this.shaderProgramHandle);
            GL.BindVertexArray(this.vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Triangles,0,3);
            Console.WriteLine(shaderProgramHandle.Handle);
            //双缓冲
            this.Context.SwapBuffers();
            base.OnUpdateFrame(args);
        }
    }
}