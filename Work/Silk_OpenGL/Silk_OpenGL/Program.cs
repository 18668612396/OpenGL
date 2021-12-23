using System;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Window = Silk.NET.Windowing.Window;

namespace Silk_OpenGL
{
    class Program
    {
        private static IWindow window;
        private static InputContext Oninput = new InputContext();
        private static GL Gl;

        private static void Main(string[] args)
        {
            var options = WindowOptions.Default; //创建窗口实例
            options.Size = new Vector2D<int>(600, 600); //设置窗口大小
            options.Title = "LearnOpenGL with Silk.NET"; //设置窗口名
            window = Window.Create(options); //创建窗口

            window.Load += OnLoad; //设置Load
            window.Update += OnUpdate; //设置Upadate
            window.Render += OnRender; //设置渲染入口
            window.Closing += OnClose; //设置Close
            window.Run(); //运行窗口
        }

        private static uint Texture01;
        private static uint Texture02;

        private static void OnLoad()
        {
            Gl = GL.GetApi(window); //调用API 在window内
            //-------------------------------------------------------
            Buffer.LoadVertex(Gl); //加载VertexBuffer:Vbo Ebo Vao等顶点输入GPU相关内容
            Shader.LoadShader(Gl); //加载shader内容
            Texture01 = Texture.LoadTexture(Gl, "D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/container2.png");
            Texture02 = Texture.LoadTexture(Gl, "D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/container2_specular.png");
            Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        private static void OnUpdate(double obj)
        {

            Shader.UniformTexture2D(Gl, Texture01, "diffuse", 0);
            Shader.UniformTexture2D(Gl, Texture02, "specular", 1);
            Shader.UpdataGlobalValue(Gl);
            Camera.UpdataCamera(Gl,window);
            Transform.UpdataTransform(Gl);
            // Buffer.Disepose(Gl);
            //Here all updates to the program should be done.
            
        }


        private static void OnRender(double obj)
        {
            //使用Shader
            Shader.Run(Gl);
            //绘制内容
            Buffer.Draw(Gl);

        }

        private static void OnClose()
        {
            Shader.Dispose(Gl);
            Buffer.Disepose(Gl);
        }
    }
}