using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGLES1.PixelFormat;
namespace OpenGL_CSharp
{
    public class Texture
    {
       public readonly OpenTK.Graphics.TextureHandle texture; //定义纹理变量 生成一个空纹理
        public static Texture LoadFromFile(string path)
        {
            OpenTK.Graphics.TextureHandle texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2d, texture); //绑定纹理类型
            GL.ActiveTexture(TextureUnit.Texture0); //绑定一个纹理槽位
            
            //LoadTexture
            using (var image = new Bitmap(path)) //声明一个返回类型为var的变量，将引入进来的纹理给到他
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY); //垂直翻转纹理
       
                //将位图锁定到系统中
                var data = image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Console.WriteLine(data);
            }
            
            GL.GenerateTextureMipmap(texture);//生成mipmap
            Console.WriteLine("image");
            return new Texture();
        }

        public void Use(TextureUnit unit)
        {
           GL.ActiveTexture((unit));
           GL.BindTexture(TextureTarget.Texture2d,texture);
        }
    }
}

