using System;
using System.Collections.Generic;
using System.Numerics;
using Assimp.Unmanaged;
using Silk.NET.OpenGL;

namespace Silk_OpenGL
{
   public  struct Vertex
    {
        public  IntPtr Position;
        public  IntPtr Normal;
        public  AiMeshTextureCoordinateArray Texcoord;
    };

    public class Mesh
    {
        public List<Vertex> Vretices;
        public List<IntPtr> Indices;

        private uint Vao, Vbo, Ebo;

        public Mesh(GL Gl, List<Vertex> vertices, List<IntPtr> indices)
        {
            this.Vretices = vertices;
            this.Indices = indices;
            SetupMesh(Gl);
            
        }

        private unsafe void SetupMesh(GL Gl)
        {
            List<Vertex> list = new List<Vertex>();


            
            
            Vao = Gl.GenVertexArray();
            Gl.BindVertexArray(Vao);

            Vbo = Gl.GenBuffer();
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, Vbo);
            Gl.BufferData((GLEnum) BufferTargetARB.ArrayBuffer, (nuint) (Vretices.Count * sizeof(Vertex)), Vretices[0],
                BufferUsageARB.StaticDraw); //顶点的数据

            Ebo = Gl.GenBuffer();
            Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, Ebo);
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint) (Indices.Count * sizeof(int)), Indices[0],
                BufferUsageARB.StaticDraw);

            Gl.VertexAttribPointer(0, 3, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) 0);
            Gl.EnableVertexAttribArray(0);

            Gl.VertexAttribPointer(1, 3, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) (3 * sizeof(float)));
            Gl.EnableVertexAttribArray(1);

            Gl.VertexAttribPointer(2, 2, GLEnum.Float, false, (uint) sizeof(Vertex), (void*) (6 * sizeof(float)));
            Gl.EnableVertexAttribArray(2);

            Gl.BindVertexArray(0); //解绑
        }

        public void Draw(Shader shader)
        {
        }
    }


}