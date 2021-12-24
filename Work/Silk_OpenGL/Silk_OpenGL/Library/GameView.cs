using System;
using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Window = Silk.NET.Windowing.Window;

namespace Silk_OpenGL
{
    class GameView  
    {
        private static IWindow window;
        private static InputContext Oninput = new InputContext();
        private static GL Gl;
        private static Camera OnCamera = new Camera();
        private static Transform OnTransform = new Transform();
        public static void Run(string[] args)
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
            RenderPipeline.ShaderLoading(Gl);
            Texture01 = Texture.LoadTexture(Gl, "D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/container2.png");
            Texture02 = Texture.LoadTexture(Gl, "D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/container2_specular.png");
            Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        private static void OnUpdate(double obj)
        {

            RenderPipeline.UpdateShader(Gl,Texture01,Texture02);
            OnCamera.UpdataCamera(Gl,window,new Shader(Gl,"D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/NewShader.glsl").program);
            OnTransform.UpdataTransform(Gl,new Shader(Gl,"D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/NewShader.glsl").program,OnCamera);
            //Here all updates to the program should be done.
            var light = new Light().Directional();
        }


        private static void OnRender(double obj)
        {
            //使用Shader
            RenderPipeline.DrawShader(Gl);
            //绘制内容
            Buffer.Draw(Gl);

        }

        private static void OnClose()
        {
            // OnShader.Dispose(Gl);
            Buffer.Disepose(Gl);
        }
    }
}