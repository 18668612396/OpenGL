using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


namespace OpenGL_CSharp.Texture
{
    public class Texture
    {
        public static Texture LoadFromFile(string path)
        {
            int texBuffer;
            
            
            
            
            
   
            return null;
        }
        
    }
}








// Image<Rgba32> image = Image.Load<Rgba32>(path);
// image.Mutate(x=>x.Flip(FlipMode.Vertical));
//
// var pixels = new List<byte>(4 * image.Width * image.Height);
//
// for (int y = 0; y < image.Height; y++)
// {
//     var row = image.GetPixelRowSpan(y);
//
//     for (int x = 0; x < image.Width; x++)
//     {
//         pixels.Add(row[x].R);
//         pixels.Add(row[x].G);
//         pixels.Add(row[x].B);
//         pixels.Add(row[x].A);
//     }
//
// }