using System;
namespace OpenGL_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello OpenTK!");

            using (Game game = new Game(400, 300, "MyOpenGL"))
            {
                game.Run();
            }
        }
    }
}