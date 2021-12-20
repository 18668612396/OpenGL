using System;
using System.Reflection.Metadata;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGL_CSharp
{
    public class Game : GameWindow
    {
        #region ProcessInput

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Keys.Escape)
            {
                Console.WriteLine("Down");
                base.Close();
            }

            base.OnKeyDown(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                CursorVisible = false;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                CursorVisible = true;
            }

            base.OnMouseUp(e);
        }

        #endregion

        #region CreateRenderWindow

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

        #endregion

        #region MeshVertices

        #endregion

        #region VertexBuffer

        // VBO`EBO --> VAO  一个vao可以绑定两个buffer，其分别是 vbo和ebo

        //Vertex Buffer Object  用来缓存模型传进来的数据 拿到的数据将是从模型文件内拿到的完整数据，当我们打开它时 其中的数据可能看起来是杂乱无章的，但其是正确的
        private OpenTK.Graphics.BufferHandle vbo;

        //Vertex Array Buffeer   拉取vbo内的顶点数据并且排序，将 顶点、法线、UV、切线等按照设定的规则横向排放，然后挑取其中对应的数据传输到顶点着色器内//
        private OpenTK.Graphics.VertexArrayHandle vao;

        //Element Buffer Object  
        private int ebo;

        #endregion

        #region ShaderSource

        private OpenTK.Graphics.ProgramHandle shaderProgram;

        //定义vertexShader变量
        private OpenTK.Graphics.ShaderHandle vertexShader;

        private string vertexShaderSource =
            @"
                   #version 330 core
                    layout (location = 0) in vec3 aPosition;
                    void main()
                    {
                     gl_Position = vec4(aPosition, 1.0);
                    } ";

        private OpenTK.Graphics.ShaderHandle fragmentShader;

        private string fragmentShaderSource =
            @"
                #version 330 core
                out vec4 FragColor;
                void main()
                {
                FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
                }";

        #endregion

        //窗口开启时调用
        protected override void OnLoad()
        {
            float[] vertices =
            {
                -0.5f, -0.5f, 0.0f, //Bottom-left vertex
                0.5f, -0.5f, 0.0f, //Bottom-right vertex
                0.0f, 0.5f, 0.0f //Top vertex
            };
            //这里用的不是GL.GenVertexArrays 所以一次只是生成一个
            vao = GL.GenVertexArray(); //生成一个vao
            GL.BindVertexArray(vao); //绑定vao

            vbo = GL.GenBuffer(); //生成一个vbo
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, vbo); //把vbo绑定到ArrayBuffer

            //BufferData是一个专门用来把用户定义的数据复制到当前绑定缓冲的函数。
            //它的第一个参数是目标缓冲的类型：顶点缓冲对象当前绑定到BufferTargetARB.ArrayBuffer目标上。
            //第二个参数指定传输数据的大小(以字节为单位)；用一个简单的sizeof计算出顶点数据大小就行。
            //第三个参数是我们希望发送的实际数据。
            GL.BufferData(BufferTargetARB.ArrayBuffer, vertices.Length * sizeof(float), vertices[0],
                BufferUsageARB.StaticDraw);

            //创建一个顶点着色器返还给我们定义好的Shader变量
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource); //将shader代码给定到vertexShader变量内
            GL.CompileShader(vertexShader); //编译顶点着色器

            //创建一个顶点着色器返还给我们定义好的Shader变量
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource); //将shader代码给定到FragmentShader变量内
            GL.CompileShader(fragmentShader); //编译顶点着色器


            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);


            //清除后的背景颜色，我只想让他调用一次，所以放在OnLoad里使用
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            base.OnLoad();
        }

        //窗口关闭时调用
        protected override void OnUnload()
        {
            base.OnUnload();
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        //每一帧进行调用
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            // Console.WriteLine("test");
            base.OnUpdateFrame(args);
        }


        //渲染的主要入口 每一帧调用他来实现渲染循环
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); //clear缓冲区

            GL.BindVertexArray(vao);
            GL.UseProgram(shaderProgram);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            //OpenGL双缓冲
            SwapBuffers(); //https://docs.microsoft.com/zh-cn/windows/win32/opengl/drawing-with-double-buffers
            base.OnRenderFrame(args);
        }
    }
}