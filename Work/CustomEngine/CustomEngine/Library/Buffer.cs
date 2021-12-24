using System.Numerics;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Silk_OpenGL
{
    public class Buffer
    {
        private static uint Vbo;
        private static uint Ebo;
        private static uint Vao;

        private static float[] vertices =
        {
        // positions          // normals           // texture coords
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f,  1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f,  1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f,  1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f,  0.0f,

        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f,  0.0f,

        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f,  1.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f,  0.0f,
        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f,  0.0f,

         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f,  1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f,  1.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f,  0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f,  1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f,  1.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f,  0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f,  0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f,  1.0f,

        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f,  0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f,  1.0f
        };

        private static int[] indices =
        {
            0, 1, 3, // first triangle
            1, 2, 3 // second triangle
        };



        public static void LoadVertex(GL Gl)
        {
            Gl.Enable(EnableCap.DepthTest); //开启深度测试

            Gl.DepthMask(true);
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
                BufferUsageARB.StaticDraw); //顶点的数据
            Gl.BufferData(BufferTargetARB.ElementArrayBuffer, (uint) indices.Length * sizeof(float), indices[0],
                BufferUsageARB.StaticDraw); //顶点的索引数据
        }

        public static unsafe void Draw(GL Gl)
        {
            Gl.Clear(ClearBufferMask.ColorBufferBit); //绘制开始前 先清理上一帧的屏幕颜色buffer
            Gl.Clear(ClearBufferMask.DepthBufferBit); //绘制开始前 先清理上一帧的屏幕深度buffer
            Gl.BindVertexArray(Vao); //绑定Vao
            // Gl.DrawElements(PrimitiveType.Triangles, (uint) indices.Length, DrawElementsType.UnsignedInt,
            //     null); //以索引方式绘制
            Gl.DrawArrays(PrimitiveType.Triangles, 0, 36);
            
        }

        public static void Disepose(GL Gl)
        {
            Gl.DeleteBuffer(Vbo);
            Gl.DeleteBuffer(Ebo);
            Gl.DeleteVertexArray(Vao);
            Gl.DeleteBuffer(Vao);
            
        }
    }
}