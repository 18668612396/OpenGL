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
        private static GL Gl;
        private static InputContext input;

        private static uint Vbo;
        private static uint Ebo;
        private static uint Vao;
        private static uint Shader;

        private static float[] vertices =
        {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f,  0.5f, 0.0f
        };

        private static int[] indices =
        {
            0, 1, 3, // 第一个三角形
            2, 3, 1 // 第二个三角形
        };
        
        private static readonly string VertexShaderSource = @"
        #version 330 core //Using version GLSL version 3.3
        layout (location = 0) in vec4 vPos;
        
        void main()
        {
            gl_Position = vec4(vPos.x, vPos.y, vPos.z, 1.0);
        }
        ";

        //Fragment shaders are run on each fragment/pixel of the geometry.
        private static readonly string FragmentShaderSource = @"
        #version 330 core
        out vec4 FragColor;

        void main()
        {
            FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
        }
        ";

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

        private unsafe static void OnLoad()
        {
            Gl = GL.GetApi(window); //调用API 在window内
            input = new InputContext(); //赋予外部设备变量
            input.Main(window); //调用input.Main 
        //-------------------------------------------------------
        
            Vao = Gl.GenVertexArray(); //生成一个vao
            Gl.BindVertexArray(Vao); //绑定vao

            Vbo = Gl.GenBuffer(); //生成一个vbo
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, Vbo); //绑定vbo

            Ebo = Gl.GenBuffer(); //生成一个Ebo
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, Ebo); //绑定Ebo
            
            Gl.BufferData((GLEnum) BufferTargetARB.ArrayBuffer,(uint)vertices.Length * sizeof(float),vertices[0],BufferUsageARB.StaticDraw);
           
            uint vertexShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vertexShader,VertexShaderSource);
            Gl.CompileShader(vertexShader);
            
            uint fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fragmentShader,FragmentShaderSource);
            Gl.CompileShader(fragmentShader);

            Shader = Gl.CreateProgram();
            Gl.AttachShader(Shader,vertexShader);
            Gl.AttachShader(Shader,fragmentShader);
            Gl.LinkProgram(Shader);
            
            Gl.DetachShader(Shader, vertexShader);
            Gl.DetachShader(Shader, fragmentShader);
            Gl.DeleteShader(vertexShader);
            Gl.DeleteShader(fragmentShader);

         
            Gl.VertexAttribPointer(0, 3, GLEnum.Float, false, 3 * sizeof(float), (void*)0 );
            Gl.EnableVertexAttribArray(0);
            
            Gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        }


        private static void OnUpdate(double obj)
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            //Here all updates to the program should be done.
        }

        private static void OnRender(double obj)
        {
            Gl.BindVertexArray(Vao);
            Gl.UseProgram(Shader);
            
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
            
            
            //Here all rendering should be done.
        }
    }
}