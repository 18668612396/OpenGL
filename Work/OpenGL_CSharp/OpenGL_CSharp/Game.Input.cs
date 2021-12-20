using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace OpenGL_CSharp
{
     public partial class Game
    {
        //键盘输入
        #region KeyboardInput
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Keys.Escape)
            {
                base.Close();
            }

            base.OnKeyDown(e);
        }

        #endregion
        //鼠标输入
        #region MouseInput

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                CursorVisible = false;
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Right)
            {
                CursorVisible = true;
            }

            base.OnMouseUp(e);
        }

        #endregion
      
    }
}