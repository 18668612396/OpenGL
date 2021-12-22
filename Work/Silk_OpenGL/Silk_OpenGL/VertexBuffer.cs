using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Silk_OpenGL
{
    public class VertexBuffer
    {
        private static uint Vbo;
        private static uint Ebo;
        private static uint Vao;

        private float[] vertices =
        {
            // positions          // colors           // texture coords
            0.5f,  0.5f, 0.0f,   1.0f, 0.0f, 0.0f,   1.0f, 1.0f, // top right
            0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f,   1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f,   0.0f, 0.0f, 1.0f,   0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f,   1.0f, 1.0f, 0.0f,   0.0f, 1.0f  // top left 
        };

        private int[] indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3  // second triangle
        };

        public void LoadVertex(GL Gl)
        {
            Vao = Gl.GenVertexArray(); //生成vao
            Gl.BindVertexArray(Vao); //绑定vao

            Vbo = Gl.GenBuffer(); //生成vbo
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, Vbo); //绑定vbo

            Ebo = Gl.GenBuffer(); //生成Ebo
            Gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, Ebo); //绑定Ebo

            //BufferData是一个专门用来把用户定义的数据复制到当前绑定缓冲的函数。
            //它的第一个参数是目标缓冲的类型：顶点缓冲对象当前绑定到BufferTargetARB.ArrayBuffer目标上。
            //第二个参数指定传输数据的大小(以字节为单位)；用一个简单的sizeof计算出顶点数据大小就行。
            //第三个参数是我们希望发送的实际数据。
            Gl.BufferData((GLEnum) BufferTargetARB.ArrayBuffer, (uint) vertices.Length * sizeof(float), vertices[0],
                BufferUsageARB.StaticDraw);
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (uint) indices.Length * sizeof(float), indices[0],
                BufferUsageARB.StaticDraw);
        }

        public unsafe void Draw(GL Gl)
        {
            Gl.BindVertexArray(Vao);
            Gl.DrawElements(PrimitiveType.Triangles,  (uint) indices.Length, DrawElementsType.UnsignedInt, null);
            // Gl.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        public void Disepose(GL Gl)
        {
            // Gl.DeleteBuffer(Vbo);
            // Gl.DeleteBuffer(Ebo);
            // Gl.DeleteBuffer(Vao);
        }
    }
}