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
        public List<Vertex> vertices = new List<Vertex>();
        public List<int> indices = new List<int>();

        private uint Vao, Vbo, Ebo;

        public Mesh(GL Gl, float[] vertices)
        {
    
            for (int m = 0; m < vertices.Length / 3; m++)
            {
                Vertex temp = new Vertex();
                for (int i = 0; i < vertices.Length; i++)
                {
                    temp.Position.X = vertices[i];
                    temp.Position.Y = vertices[i + 1];
                    temp.Position.Z = vertices[i + 2];
                    i = i + 2;
                }
                this.vertices.Add(temp);
                Console.WriteLine(this.vertices[m].Position);
            }
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
            Gl.BufferData((GLEnum) BufferTargetARB.ArrayBuffer, (uint) vertices.Count * sizeof(float), vertices[0],
                BufferUsageARB.StaticDraw); //顶点的数据

            Ebo = Gl.GenBuffer();
            Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, Ebo);
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (uint) (sizeof(int) * indices.Count), indices[0],
                BufferUsageARB.StaticDraw);

            Gl.VertexAttribPointer(0, 3, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) 0);
            Gl.EnableVertexAttribArray(0);

            // Gl.VertexAttribPointer(1, 3, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) (3 * sizeof(float)));
            // Gl.EnableVertexAttribArray(1);
            //
            // Gl.VertexAttribPointer(2, 2, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) (6 * sizeof(float)));
            // Gl.EnableVertexAttribArray(2);
            //
            // Gl.VertexAttribPointer(3, 4, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) (6 * sizeof(float)));
            // Gl.EnableVertexAttribArray(3);

            Gl.BindVertexArray(0); //解绑
        }

        public void Draw(GL Gl)
        {
            unsafe
            {
                Gl.Clear(ClearBufferMask.ColorBufferBit); //绘制开始前 先清理上一帧的屏幕颜色buffer
                Gl.Clear(ClearBufferMask.DepthBufferBit); //绘制开始前 先清理上一帧的屏幕深度buffer
                Gl.BindVertexArray(Vao); //绑定Vao
                Gl.DrawElements(PrimitiveType.Triangles, (uint) indices.Count, DrawElementsType.UnsignedInt,
                    null); //以索引方式绘制
                Gl.BindVertexArray(0); //释放
                Gl.ActiveTexture(TextureUnit.Texture0); //释放
                // Gl.DrawArrays((PrimitiveType) PrimitiveType.Triangles, 0, 36);
            }
        }
    }
}