using System;
using System.Collections.Generic;
using System.Numerics;
using Assimp;
using Assimp.Unmanaged;
using Silk.NET.OpenGL;
using PrimitiveType = Silk.NET.OpenGL.PrimitiveType;

namespace Silk_OpenGL
{
    public struct Vertex
    {
        public Vector3D Position;
        // public Vector3D Normal;
        // public Vector3D Texcoord;
        // public Color4D Color;
    };

    public class Mesh
    {
        public Vertex vertices;
        public List<int> indices = new List<int>();

        private uint Vao, Vbo, Ebo;

         private static float[] test =
        {
        // positions          // normals           // texture coords
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f,  0.0f,
      
        };
        public Mesh(GL Gl, float[] vertices)
        {
            Vertex temp = new Vertex();
            for (int i = 0; i < vertices.Length / 3; i++)
            {
                // Console.WriteLine(i);
                temp.Position.X = vertices[i];
                temp.Position.Y = vertices[i + 1];
                temp.Position.Z = vertices[i + 2];
                this.vertices.Add(temp);
                // Console.WriteLine(temp.Position);
            }

            test = vertices;
            SetupMesh(Gl);
        }

        public Mesh(GL Gl, List<Vertex> vertices, List<int> indices)
        {
            this.vertices = vertices;
            this.indices = indices;
        
            SetupMesh(Gl);
        }

        private unsafe void SetupMesh(GL Gl)
        {
            Gl.Enable(EnableCap.DepthTest); //开启深度测试

            Vao = Gl.GenVertexArray();
            Gl.BindVertexArray(Vao);

            Vbo = Gl.GenBuffer();
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, Vbo);
            
            GL.BufferData(BufferTargetARB.ArrayBuffer, (IntPtr)total, IntPtr.Zero
                , BufferUsageARB.StaticDraw);
            
            GL.BufferSubData(BufferTargetARB.ArrayBuffer, (IntPtr)(0), (IntPtr)vertSize, ModelInfo.Verts.ToArray());
            
            
            
            Gl.BufferData((GLEnum) BufferTargetARB.ArrayBuffer,(uint) (sizeof(Vertex) * vertices.Count), vertices[0],
                BufferUsageARB.StaticDraw); //顶点的数据
            // Ebo = Gl.GenBuffer();
            // Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, Ebo);
            // Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (uint) (sizeof(int) * indices.Count), indices[0],
            //     BufferUsageARB.StaticDraw);

            // Gl.VertexAttribPointer(0, 3, GLEnum.Float, false,0, (void*) 0);
            // Gl.EnableVertexAttribArray(0);
            // Console.WriteLine((uint) sizeof(Vertex));
            // Gl.VertexAttribPointer(1, 3, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) (3 * sizeof(float)));
            // Gl.EnableVertexAttribArray(1);
            // //
            // Gl.VertexAttribPointer(2, 2, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) (6 * sizeof(float)));
            // Gl.EnableVertexAttribArray(2);

            // Gl.BufferData((GLEnum) BufferTargetARB.ArrayBuffer, (uint) test.Length * sizeof(float), test[0],
            //     BufferUsageARB.StaticDraw); //顶点的数据
            
            Console.WriteLine(vertices[0].Position);

            Gl.BindVertexArray(0); //解绑
        }

        public void Draw(GL Gl)
        {
            unsafe
            {
                Gl.Clear(ClearBufferMask.ColorBufferBit); //绘制开始前 先清理上一帧的屏幕颜色buffer
                Gl.Clear(ClearBufferMask.DepthBufferBit); //绘制开始前 先清理上一帧的屏幕深度buffer
                Gl.BindVertexArray(Vao); //绑定Vao
                // Gl.DrawElements(PrimitiveType.Triangles, (uint) indices.Count, DrawElementsType.UnsignedInt,
                // null); //以索引方式绘制
                Gl.DrawArrays((PrimitiveType) PrimitiveType.Triangles, 0, 36);
                // Gl.BindVertexArray(0); //释放
                // Gl.ActiveTexture(TextureUnit.Texture0); //释放
            }
        }
    }
}