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
            Game game = new Game(new HumanPlayer("P1", "x"), new HumanPlayer("P2", "o"));
            game.Start();
        }
    }
}
