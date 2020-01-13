using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Create and start a new Game
                Game game = new Game(new HumanPlayer("P1", "x"), new HumanPlayer("P2", "o"));
                game.Start();

                Console.Write("Press ENTER to play again...");
                Console.ReadLine();
            }
        }
    }
}
