using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.SDL;
using Cursor = Silk.NET.SDL.Cursor;
using Window = Silk.NET.Windowing.Window;


namespace Silk_OpenGL
{
    class Program
    {
        private static IWindow window;
        private static InputContext Oninput = new InputContext();
        private static GL Gl;

        private static VertexBuffer OnVertexBuffer = new VertexBuffer();
        private static Texture OnTexture = new Texture();
        private static Shader OnShader = new Shader();


        private static void Main(string[] args)
        {
            var options = WindowOptions.Default; //创建窗口实例
            options.Size = new Vector2D<int>(400, 300); //设置窗口大小
            options.Title = "LearnOpenGL with Silk.NET"; //设置窗口名
            window = Window.Create(options); //创建窗口
            
            window.Load += OnLoad; //设置Load
            window.Update += OnUpdate; //设置Upadate
            window.Render += OnRender; //设置渲染入口
            window.Closing += OnClose;//设置Close
            window.Run(); //运行窗口
        }
        private static uint Texture01;
        private static uint Texture02;
        private static void OnLoad()
        {
            Gl = GL.GetApi(window); //调用API 在window内
            Oninput.LoadInput(window); //调用input.Main 
        //-------------------------------------------------------
            
            OnVertexBuffer.LoadVertex(Gl);//加载VertexBuffer:Vbo Ebo Vao等顶点输入GPU相关内容

            OnShader.LoadShader(Gl);//加载shader内容
            Texture01 =  OnTexture.LoadTexture(Gl,"D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/container.bmp");
            Texture02 =  OnTexture.LoadTexture(Gl,"D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/awesomeface.png");
            Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }

        private static void OnUpdate(double obj)
        {
            OnShader.UniformTexture2D(Gl, Texture01, "diffuse", 0);
            OnShader.UniformTexture2D(Gl, Texture02,"specular", 1);
            OnShader.SetUniform(Gl,"test",0.5f);
            OnShader.Transform(Gl);
            //Here all updates to the program should be done.
        }
        private static void OnRender(double obj)
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            
            OnVertexBuffer.Draw(Gl);
            //使用Shader
            OnShader.Run(Gl);
            //绘制内容

        }
        
        private static void OnClose()
        {
            OnShader.Dispose(Gl);
            OnTexture.Dispose(Gl);
            // OnVertexBuffer.Disepose(Gl);
        }


    }
}