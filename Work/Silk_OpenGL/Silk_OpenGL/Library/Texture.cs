using Silk.NET.OpenGL;
using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

namespace Silk_OpenGL
{
    public class Texture
    {
        
        public static uint texture;
        public static unsafe uint LoadTexture(GL Gl, string path)
        {
            var image = (Image<Rgba32>) Image.Load(path);

            image.Mutate(x =>x.Flip(FlipMode.Vertical));
            fixed (void* data = &MemoryMarshal.GetReference(image.GetPixelRowSpan(0)))
            {
                LoadImage(Gl, data, (uint) image.Width, (uint) image.Height);
            }

            image.Dispose();

            return texture;
        }
        private static unsafe void LoadImage(GL Gl, void* data, uint width, uint height)
        {
            //Saving the gl instance.


            //Generating the opengl handle;
            texture = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2D, texture);
       
            //Setting the data of a texture.
            Gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba,
                PixelType.UnsignedByte, data);
            //Setting some texture perameters so the texture behaves as expected.
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) GLEnum.Repeat);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) GLEnum.Repeat);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) GLEnum.Linear);
            Gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) GLEnum.Linear);
            Gl.GenerateMipmap(TextureTarget.Texture2D);
            //Generating mipmaps.

        }

        public static void Dispose(GL Gl)
        {
            Gl.DeleteTexture(texture);
        }
    }
}