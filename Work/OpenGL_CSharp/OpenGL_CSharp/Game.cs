using System;
using System.IO;
using System.Reflection.Metadata;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
namespace OpenGL_CSharp
{
    public partial class Game : GameWindow
    {
        #region MeshVertices

        private float[] vertices =
        {
            // positions          // colors           // texture coords
             0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f  // top left 
        };

        private int[] indices =
        {
            0, 1, 3, // 第一个三角形
            2, 3, 1 // 第二个三角形
        };

        #endregion

        #region VertexBuffer

        // VBO`EBO --> VAO  一个vao可以绑定两个buffer，其分别是 vbo和ebo

        //Vertex Buffer Object  用来缓存模型传进来的数据 拿到的数据将是从模型文件内拿到的完整数据，当我们打开它时 其中的数据可能看起来是杂乱无章的，但其是正确的
        //vbo 只是为了传输拆分好的顶点数据给到vao  例如 顶点 法线 UV 切线等 
        //注：：vbo 是将模型数据从CPU拉取到GPU内
        private OpenTK.Graphics.BufferHandle vbo;

        //Vertex Array Buffeer   拉取vbo内的顶点数据并且排序，将 顶点、法线、UV、切线等按照设定的规则横向排放，然后挑取其中对应的数据传输到顶点着色器内//
        //vao一般有16个栏位，存放了 顶点，法线，切线，UV等
        private OpenTK.Graphics.VertexArrayHandle vao;

        //Element Buffer Object 他是一个独立的管线，且独立与vbo 主要是为了传输顶点的indices ,也就是说 使用他来指定 某个面所需的顶点是哪几个
        private OpenTK.Graphics.BufferHandle ebo;

        #endregion

        private Shader OnShader;

        //窗口开启时调用
        protected override void OnLoad()
        {

            
            //这里用的不是GL.GenVertexArrays 所以一次只是生成一个
            vao = GL.GenVertexArray(); //生成一个vao
            GL.BindVertexArray(vao); //绑定vao

            vbo = GL.GenBuffer(); //生成一个vbo
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, vbo); //把vbo绑定到ArrayBuffer

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo); //把ebo绑定到ElementArrayBuffer

            //BufferData是一个专门用来把用户定义的数据复制到当前绑定缓冲的函数。
            //它的第一个参数是目标缓冲的类型：顶点缓冲对象当前绑定到BufferTargetARB.ArrayBuffer目标上。
            //第二个参数指定传输数据的大小(以字节为单位)；用一个简单的sizeof计算出顶点数据大小就行。
            //第三个参数是我们希望发送的实际数据。
            GL.BufferData(BufferTargetARB.ElementArrayBuffer, indices.Length * sizeof(int), indices[0],
                BufferUsageARB.StaticDraw);
            GL.BufferData(BufferTargetARB.ArrayBuffer, vertices.Length * sizeof(float), vertices[0],
                BufferUsageARB.StaticDraw);

            var vertexShaderPath = "E:/GitHub/OpenGL/Work/OpenGL_CSharp/OpenGL_CSharp/Shader/vertexShader.vert";
            var fragmentShaderPath = "E:/GitHub/OpenGL/Work/OpenGL_CSharp/OpenGL_CSharp/Shader/fragmentShader.frag";
            OnShader = new Shader(vertexShaderPath,fragmentShaderPath);
            
     

            
            
            //清除后的背景颜色，我只想让他调用一次，所以放在OnLoad里使用
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            base.OnLoad();
        }

        //窗口关闭时调用
        protected override void OnUnload()
        {
            OnShader.Dispose();//释放ShaderProgram
            Console.WriteLine("esc");
            base.OnUnload();
        }

        //每一帧进行调用 可用于存放一些跟着渲染一起更新的数据
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            GL.BindVertexArray(vao); //绑定vao
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo); //绑定ebo
            base.OnUpdateFrame(args);
        }

        //渲染的主要入口 每一帧调用他来实现渲染循环
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit); //clear缓冲区

            OnShader.Use();
            //绘制
            //第一个参数为绘制的模式，一般我们设置为三角形
            //第二个 从第几个顶点开始绘制
            //第三个是绘制多少个顶点数量
            // GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
            //OpenGL双缓冲
            SwapBuffers(); //https://docs.microsoft.com/zh-cn/windows/win32/opengl/drawing-with-double-buffers
            base.OnRenderFrame(args);
        }
    }
}