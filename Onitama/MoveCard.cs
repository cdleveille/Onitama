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
        private List<int> moves;
        private List<MoveDelta> moveDeltas;

        // Create a new move card
        public MoveCard(string name, List<int> moves)
        {
            this.name = name.ToUpper();
            this.moves = moves;
            this.moveDeltas = GetMoveDeltas(moves);
        }

        // Convert each move to a delta-X, delta-Y coordinate pair
        private List<MoveDelta> GetMoveDeltas(List<int> moves)
        {
            List<MoveDelta> moveDeltas = new List<MoveDelta>();

            foreach (int move in moves)
            {
                switch (move)
                {
                    case 0:
                        moveDeltas.Add(new MoveDelta(-2, 2));
                        break;
                    case 1:
                        moveDeltas.Add(new MoveDelta(-1, 2));
                        break;
                    case 2:
                        moveDeltas.Add(new MoveDelta(0, 2));
                        break;
                    case 3:
                        moveDeltas.Add(new MoveDelta(1, 2));
                        break;
                    case 4:
                        moveDeltas.Add(new MoveDelta(2, 2));
                        break;
                    case 5:
                        moveDeltas.Add(new MoveDelta(-2, 1));
                        break;
                    case 6:
                        moveDeltas.Add(new MoveDelta(-1, 1));
                        break;
                    case 7:
                        moveDeltas.Add(new MoveDelta(0, 1));
                        break;
                    case 8:
                        moveDeltas.Add(new MoveDelta(1, 1));
                        break;
                    case 9:
                        moveDeltas.Add(new MoveDelta(2, 1));
                        break;
                    case 10:
                        moveDeltas.Add(new MoveDelta(-2, 0));
                        break;
                    case 11:
                        moveDeltas.Add(new MoveDelta(-1, 0));
                        break;
                    case 12:
                        moveDeltas.Add(new MoveDelta(0, 0));
                        break;
                    case 13:
                        moveDeltas.Add(new MoveDelta(1, 0));
                        break;
                    case 14:
                        moveDeltas.Add(new MoveDelta(2, 0));
                        break;
                    case 15:
                        moveDeltas.Add(new MoveDelta(-2, -1));
                        break;
                    case 16:
                        moveDeltas.Add(new MoveDelta(-1, -1));
                        break;
                    case 17:
                        moveDeltas.Add(new MoveDelta(0, -1));
                        break;
                    case 18:
                        moveDeltas.Add(new MoveDelta(1, -1));
                        break;
                    case 19:
                        moveDeltas.Add(new MoveDelta(2, -1));
                        break;
                    case 20:
                        moveDeltas.Add(new MoveDelta(-2, -2));
                        break;
                    case 21:
                        moveDeltas.Add(new MoveDelta(-1, -2));
                        break;
                    case 22:
                        moveDeltas.Add(new MoveDelta(0, -2));
                        break;
                    case 23:
                        moveDeltas.Add(new MoveDelta(1, -2));
                        break;
                    case 24:
                        moveDeltas.Add(new MoveDelta(2, -2));
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
            foreach (int move in this.moves)
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

            foreach (int move in this.moves)
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

        public List<int> GetMoves()
        {
            return this.moves;
        }

        public void SetMoves(List<int> moves)
        {
            this.moves = moves;
        }

        public List<MoveDelta> GetMoveDeltas()
        {
            return this.moveDeltas;
        }

        public void SetMoveDeltas(List<MoveDelta> moveDeltas)
        {
            this.moveDeltas = moveDeltas;
        }
    }
}
