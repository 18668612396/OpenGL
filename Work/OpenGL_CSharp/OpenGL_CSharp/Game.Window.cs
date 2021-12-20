
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;


namespace OpenGL_CSharp
{
     public partial class Game
    {
        #region CreateRenderWindow

        public Game() :
            base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(800, 600)); //重定义Window大小
        }
        
        //调整窗口大小时调用
        protected override void OnResize(ResizeEventArgs e)
        {
            //GL.Viewport  1:中心坐标偏移x 2:中心坐标偏移y 3:屏幕宽度 4:屏幕高度
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        #endregion
    }
}