using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
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
