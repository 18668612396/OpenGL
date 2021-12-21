using Silk.NET.GLFW;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.SDL;
using Silk.NET.Windowing;
using Cursor = Silk.NET.SDL.Cursor;
using Window = Silk.NET.Windowing.Window;


namespace Silk_OpenGL
{
    class Program
    {
        private static IWindow window;
        private static GL Gl;
        private static InputContext input;


        private static void Main(string[] args)
        {
            var options = WindowOptions.Default; //创建窗口实例
            options.Size = new Vector2D<int>(800, 600); //设置窗口大小
            options.Title = "LearnOpenGL with Silk.NET"; //设置窗口名

            window = Window.Create(options); //创建窗口

            window.Load += OnLoad; //设置Load
            window.Update += OnUpdate; //设置Upadate
            window.Render += OnRender; //设置渲染入口
            window.Run(); //运行窗口
        }

        private static void OnLoad()
        {
            Gl = GL.GetApi(window); //调用API 在window内
            input = new InputContext();//赋予外部设备变量
            input.Main(window);//调用input.Main 
            
            Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            
        }


        private static void OnUpdate(double obj)
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            //Here all updates to the program should be done.
        }

        private static void OnRender(double obj)
        {
            //Here all rendering should be done.
        }
    }
}