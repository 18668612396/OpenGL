using Silk_OpenGL;

namespace CustomEngine;

public partial class CustomEngine : Form
{
    public CustomEngine()
    {
        GameView.Run();
        InitializeComponent();
    }
}
