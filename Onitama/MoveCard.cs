using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    //public static int[]
    //        Crab = { 7, 10, 14 },
    //        Elephant = { 6, 8, 11, 13 },
    //        Dragon = { 5, 9, 16, 18 },
    //        Goose = { 6, 11, 13, 18 },
    //        Tiger = { 2, 17 };

    class MoveCard
    {
        public string name;
        public int[] moves;

        public MoveCard(string name, int[] moves)
        {
            this.name = name;
            this.moves = moves;
        }
    }
}
