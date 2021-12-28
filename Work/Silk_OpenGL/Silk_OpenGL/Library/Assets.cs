
using System;
using System.Collections.Generic;
using System.IO;
using Silk.NET.OpenGL;

namespace Silk_OpenGL
{
    public struct TextureMate
    {
        private string name;
        private string path;
        private uint id;
    }

    public struct MaterialMate
    {
        private string name;
        private string path;
        private uint id;
        private string[] Textures;
        private string[] Floats;
        private string[] Ints;
    }
    
    public struct PrefabMate
    {
        private string name;
        private string path;
        private uint id;
    }
    public struct ShaderMate
    {
        public string name;
        public string path;
        public Shader id;
        public string[] Textures;
        public string[] Ints;
        public string[] Floats;

    }
    
    
    public static class Assets
    {
        public static List<TextureMate> Textures  = new List<TextureMate>();
        public static List<MaterialMate> Materials = new List<MaterialMate>();
        public static List<PrefabMate> Prefabs = new List<PrefabMate>();
        
        public static List<ShaderMate> Shaders = new List<ShaderMate>();
        public static void Loading(GL Gl)
        {
            
            //获取Assets目录
            DirectoryInfo TheAssets = new DirectoryInfo("E:/GitHub/OpenGL/Work/Silk_OpenGL/Silk_OpenGL/Assets");
            //遍历Assets下的文件夹
            foreach (var file in TheAssets.GetFiles())
            {
                //获取文件后缀名
                var extension = Path.GetExtension(file.Name);

                var fileName = file.Name;//获取文件名
                var filePath = file.FullName.Replace("\\","/");//获取文件路径并修改文件路径的斜杠
                var fileContent = File.ReadAllText(filePath);
               
                if (extension == ".obj")
                {
                    // Console.WriteLine(file.Name);
                }

                if (extension ==".png")
                {
                    // Console.WriteLine(file.Name);
                }

                if (extension == ".shader")
                {
                    var shaderMate = new ShaderMate();
                    // Console.WriteLine(file.Name);
                    // Console.WriteLine(filePath);
                    shaderMate.name = fileName;
                    shaderMate.path = filePath;
                    shaderMate.id = new Shader(Gl,filePath);
                    shaderMate.Textures = new[] {"diffuse", "specular"};//读取当前Shader内Properties内的Texture
                    shaderMate.Ints = new string[] { };//读取当前Shader中Properties内的Int
                    shaderMate.Floats = new string[] { };//读取当前Shader中Properties内的Float
                    Shaders.Add(shaderMate);
                }

                if (extension == ".mat")
                {
                    Console.WriteLine(file.Name);
                    //读取纹理坐标
                    
                    Console.WriteLine(fileContent);
                    
                }
                


            }
            
        }
        
        
        
    }
}