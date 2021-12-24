using Silk.NET.OpenGL;

namespace Silk_OpenGL
{
    public static class OnShader
    {
        public static Shader shader;
        public static void Loading(GL Gl)
        {
            shader = new Shader(Gl,"D:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets/NewShader.glsl"); //加载shader内容
        }

        public static void UpdateShader(GL Gl,uint Texture01,uint Texture02)
        {
            shader.UniformTexture2D(Gl, Texture01, "diffuse", 0);
            shader.UniformTexture2D(Gl, Texture02, "specular", 1);
            shader.UpdataGlobalValue(Gl);
        }

        public static void DrawShader(GL Gl)
        {
            shader.Run(Gl);
        }
    }
}