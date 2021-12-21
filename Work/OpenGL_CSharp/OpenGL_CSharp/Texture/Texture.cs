using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace OpenGL_CSharp
{
    class Texture
    {
        public static OpenTK.Graphics.TextureHandle LoadTexture(string path, TextureUnit unit)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("error");
            }

            OpenTK.Graphics.TextureHandle id = GL.GenTexture();
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2d, id);

            Bitmap bitmap = new Bitmap(path);
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            
            GL.TexImage2D(TextureTarget.Texture2d, 0, (int)InternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.Byte, data.Scan0);
            
            //设置纹理的循环模式
            GL.TexParameterf(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
            GL.TexParameterf(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);
            
            //设置纹理的滤波模式
            GL.TexParameterf(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter,
                (int) TextureMinFilter.Linear);
            GL.TexParameterf(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter,
                (int) TextureMinFilter.Linear);
            GL.GenerateMipmap(TextureTarget.Texture2d);//生成mipmap
            
            bitmap.UnlockBits(data); //释放
            return id;
        }

        public static void OnUniformTexture(Shader shader,OpenTK.Graphics.TextureHandle texture,string uniformName,int TextureUnit)
        {
            GL.ActiveTexture((OpenTK.Graphics.OpenGL.TextureUnit)TextureUnit);
            GL.BindTexture(TextureTarget.Texture2d,texture);
            GL.Uniform1i(GL.GetUniformLocation(shader.shaderProgram,uniformName),TextureUnit);
        }
    }
}