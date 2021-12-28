using Silk.NET.OpenGL;

namespace Silk_OpenGL
{
    public static class RenderPipeline
    {
        public static Shader shader;
        private static int shaderIndex = 2;
        public static void ShaderLoading(GL Gl)
        {
            shader = new Shader(Gl,Assets.Shaders[shaderIndex].path); //加载shader内容
            
        }

        public static void UpdateShader(GL Gl,Camera OnCanera,uint Texture01,uint Texture02)
        {
            for (int i = 0; i < Assets.Shaders[shaderIndex].Textures.Length; i++)
            {
                shader.UniformTexture2D(Gl, Texture01, Assets.Shaders[shaderIndex].Textures[i], i);
            }
            shader.UpdataGlobalValue(Gl,OnCanera);
        }

        public static void DrawShader(GL Gl)
        {
            shader.Run(Gl);
        }
    }
}