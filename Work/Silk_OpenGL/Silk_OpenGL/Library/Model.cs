// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Numerics;
// using Assimp;
// using Assimp.Unmanaged;
// using Silk.NET.OpenGL;
// using Matrix4x4 = Assimp.Matrix4x4;
//
// namespace Silk_OpenGL
// {
//     public class Model
//     {
//         private List<Mesh> meshes = new List<Mesh>();
//
//         public Model(GL Gl, string path)
//         {
//             LoadFromFile(Gl, path);
//         }
//
//         private static void LoadFromFile(GL Gl, string path)
//         {
//             //设置处理方式
//             const PostProcessSteps postProcessSteps = PostProcessSteps.GenerateSmoothNormals |
//                                                       PostProcessSteps.CalculateTangentSpace |
//                                                       PostProcessSteps.Triangulate |
//                                                       PostProcessSteps.FlipUVs;
//             var import = new AssimpContext(); //实例化ImportContext
//
//             var scene = import.ImportFile(path, postProcessSteps); //导入模型
//
//             processNode(Gl, scene.RootNode, scene);
//         }
//
//         //遍历模型
//         private static void processNode(GL Gl, Node node, Scene scene)
//         {
//             //遍历RootNode中的ChildNode
//             foreach (var nodeChild in scene.RootNode.Children)
//             {
//                 Console.WriteLine(nodeChild.Name);
//             }
//             
//
//         }
//
//         private static Mesh processMesh(GL Gl, AiMesh mesh, Scene scene)
//         {
//             var vertices = new List<Vertex>();
//             var indices = new List<IntPtr>();
//             var tempVertices = new Vertex();
//             //遍历RootNode中的ChildNode
//             foreach (var nodeChild in scene.RootNode.Children)
//             {
//                 Console.WriteLine(nodeChild.Name);
//                 if (!nodeChild.HasMeshes) continue; //如果node里不存在mesh 就跳过下一行代码
//                 //遍历ChildNode中的Mesh
//                 foreach (var index in nodeChild.MeshIndices)
//                 {
//                     var curMesh = scene.Meshes[index]; //获得当前的mesh
//                     //遍历Mesh中面的数据
//                     foreach (var face in curMesh.Faces)
//                     {
//                         for (int i = 0; i < face.IndexCount; i++)
//                         {
//                             var indice = face.Indices[i];
//
//                             //读取顶点色
//                             if (curMesh.HasVertexColors(0))
//                             {
//                                 var vertexColor = curMesh.VertexColorChannels[0][indice];
//                                 tempVertices.Color = vertexColor;
//                             }
//
//                             //读取顶点法线
//                             if (curMesh.HasNormals)
//                             {
//                                 var normal = curMesh.Normals[indice];
//                                 tempVertices.Normal = normal;
//                             }
//
//                             //读取顶点UV坐标
//                             if (curMesh.HasTextureCoords(0))
//                             {
//                                 var uvw = curMesh.TextureCoordinateChannels[0][indice];
//                                 tempVertices.Texcoord = uvw;
//                             }
//
//                             //读取顶点坐标
//                             var pos = curMesh.Vertices[indice];
//                             tempVertices.Position = pos;
//                             vertices.Add(tempVertices);
//                         }
//                     }
//                 }
//             }
//
//             return new Mesh(Gl, vertices, null);
//         }
//         
//         
//     }
// }