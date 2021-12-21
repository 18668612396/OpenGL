using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Silk_OpenGL
{
    public class InputContext
    {
        private static IWindow inputForWindow;

        public void Main(IWindow window)
        {
            inputForWindow = window;
            //设置外设输入
            IInputContext inputContext = window.CreateInput();
            for (int i = 0; i < inputContext.Keyboards.Count; i++)
            {
                inputContext.Keyboards[i].KeyDown += KeyDown;
            }
        }

        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
            //Check to close the window on escape.
            if (arg2 == Key.Escape)
            {
                inputForWindow.Close();
            }
        }
    }
}