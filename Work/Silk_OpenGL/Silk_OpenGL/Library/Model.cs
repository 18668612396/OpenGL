using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Assimp;
using Assimp.Unmanaged;
using Silk.NET.OpenGL;
using Matrix4x4 = Assimp.Matrix4x4;

namespace Silk_OpenGL
{
    public class Model
    {
        public Model(GL Gl, string path)
        {
            List<Mesh> meshes;

            LoadFromFile(Gl, path);
        }

        private Scene m_model;
        public string directory;

        private static void LoadFromFile(GL Gl, string path)
        {
            //设置处理方式
            var postProcessSteps = PostProcessSteps.GenerateSmoothNormals |
                                   PostProcessSteps.CalculateTangentSpace |
                                   PostProcessSteps.Triangulate |
                                   PostProcessSteps.FlipUVs;
            var import = new AssimpContext(); //实例化ImportContext
            Scene scene = import.ImportFile(path, postProcessSteps); //导入模型

            processNode(Gl, scene.RootNode, scene);
        }

        //遍历模型
        private static void processNode(GL Gl, Node node, Scene scene)
        {
            //遍历RootNode中的ChildNode
            foreach (var nodeChild in node.Children)
            {
                Console.WriteLine(nodeChild.Name);

                if (nodeChild.HasMeshes) //如果ChildNode中存在Mesh 那么我就遍历他
                {
                    //遍历ChildNode中的Mesh
                    foreach (var index in nodeChild.MeshIndices)
                    {
                        Assimp.Mesh mesh = scene.Meshes[index]; //获得当前的mesh
                        // Console.WriteLine(index.ToString());

                        //遍历Mesh中的面
                        foreach (var face in mesh.Faces)
                        {
                            foreach (var faceIndex in face.Indices)
                            {
                                
                                if (mesh.HasVertexColors(0))
                                {
                                    Color4D vertexColor = mesh.VertexColorChannels[0][faceIndex];
                                 
                                }

                                if (mesh.HasNormals)
                                {
                                    Vector3D normal = mesh.Normals[faceIndex];
                                    Console.WriteLine(normal);
                                }

                                if (mesh.HasTextureCoords(0))
                                {
                                    Vector3D uvw = mesh.TextureCoordinateChannels[0][faceIndex];
                                    
                                }
                                Vector3D pos = mesh.Vertices[faceIndex];
                               
                            }
                        }
                    }
                }
            }
        }
    }
}