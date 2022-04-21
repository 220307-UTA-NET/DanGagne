using Battleship.Logic;

namespace Battleship
{
    public class Program
    {
        static void Main(string[] args)
        {
            GenerateGame game = new GenerateGame();
            game.StartGame();
        }
    }
}