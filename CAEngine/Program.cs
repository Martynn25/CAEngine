using CAEngine.Platform;
namespace CAEngine;

class Program
{
    static void Main(string[] args)
    {
        using var game = new CAGame();
        game.Run();
    }
}