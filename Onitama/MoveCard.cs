using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class MoveCard
    {
        private string name;
        private int[] moves;
        private MoveDelta[] moveDeltas;

        // Create a new move card
        public MoveCard(string name, int[] moves)
        {
            this.name = name.ToUpper();
            this.moves = moves;
            this.moveDeltas = GetMoveDeltas(moves);
        }

        private MoveDelta[] GetMoveDeltas(int[] moves)
        {
            MoveDelta[] moveDeltas = new MoveDelta[moves.Length];

            for (int i = 0; i < moves.Length; i++)
            {
                switch (moves[i])
                {
                    case 0:
                        moveDeltas[i] = new MoveDelta(-2, 2);
                        break;
                    case 1:
                        moveDeltas[i] = new MoveDelta(-1, 2);
                        break;
                    case 2:
                        moveDeltas[i] = new MoveDelta(0, 2);
                        break;
                    case 3:
                        moveDeltas[i] = new MoveDelta(1, 2);
                        break;
                    case 4:
                        moveDeltas[i] = new MoveDelta(2, 2);
                        break;
                    case 5:
                        moveDeltas[i] = new MoveDelta(-2, 1);
                        break;
                    case 6:
                        moveDeltas[i] = new MoveDelta(-1, 1);
                        break;
                    case 7:
                        moveDeltas[i] = new MoveDelta(0, 1);
                        break;
                    case 8:
                        moveDeltas[i] = new MoveDelta(1, 1);
                        break;
                    case 9:
                        moveDeltas[i] = new MoveDelta(2, 1);
                        break;
                    case 10:
                        moveDeltas[i] = new MoveDelta(-2, 0);
                        break;
                    case 11:
                        moveDeltas[i] = new MoveDelta(-1, 0);
                        break;
                    case 12:
                        moveDeltas[i] = new MoveDelta(0, 0);
                        break;
                    case 13:
                        moveDeltas[i] = new MoveDelta(1, 0);
                        break;
                    case 14:
                        moveDeltas[i] = new MoveDelta(2, 0);
                        break;
                    case 15:
                        moveDeltas[i] = new MoveDelta(-2, -1);
                        break;
                    case 16:
                        moveDeltas[i] = new MoveDelta(-1, -1);
                        break;
                    case 17:
                        moveDeltas[i] = new MoveDelta(0, -1);
                        break;
                    case 18:
                        moveDeltas[i] = new MoveDelta(1, -1);
                        break;
                    case 19:
                        moveDeltas[i] = new MoveDelta(2, -1);
                        break;
                    case 20:
                        moveDeltas[i] = new MoveDelta(-2, -2);
                        break;
                    case 21:
                        moveDeltas[i] = new MoveDelta(-1, -2);
                        break;
                    case 22:
                        moveDeltas[i] = new MoveDelta(0, -2);
                        break;
                    case 23:
                        moveDeltas[i] = new MoveDelta(1, -2);
                        break;
                    case 24:
                        moveDeltas[i] = new MoveDelta(2, -2);
                        break;
                    default:
                        break;
                }
            }
            return moveDeltas;
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

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int[] GetMoves()
        {
            return this.moves;
        }

        public void SetMoves(int[] moves)
        {
            this.moves = moves;
        }

        public MoveDelta[] GetMoveDeltas()
        {
            return this.moveDeltas;
        }

        public void SetMoveDeltas(MoveDelta[] moveDeltas)
        {
            this.moveDeltas = moveDeltas;
        }
    }
}
