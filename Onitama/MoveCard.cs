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

        // Create a new move card
        public MoveCard(string name, int[] moves)
        {
            this.name = name.ToUpper();
            this.moves = moves;
        }

        // Return true if this move card has a move at the given position, false if not
        public bool HasMoveAt(int pos)
        {
            foreach (int move in moves)
            {
                if (move == pos)
                {
                    return true;
                }
            }
            return false;
        }

        // Return true if this move card has a move at the given position, false if not (flipped positions for player 2 perspective)
        public bool HasMoveAtP2(int pos)
        {
            if (pos < 5)
            {
                pos += 20;
            }
            else if (pos < 10)
            {
                pos += 10;
            }
            else if (pos < 15)
            {
                pos += 0;
            }
            else if (pos < 20)
            {
                pos -= 10;
            }
            else if (pos < 25)
            {
                pos -= 20;
            }

            foreach (int move in moves)
            {
                if (move == pos)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
