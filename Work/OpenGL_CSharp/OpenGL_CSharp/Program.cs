using System;

namespace OpenGL_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
}