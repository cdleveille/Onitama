using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class HumanPlayer : Player
    {
        public HumanPlayer(string name, string symbol) : base(name, symbol) {}

        public override Pawn GetPawnToMove(Game game)
        {
            List<int> activePawnPositions = new List<int>();
            
            for (int i = 0; i < this.pawns.Length; i++)
            {
                if (!this.pawns[i].isCaptured)
                {
                    activePawnPositions.Add(this.pawns[i].pos);
                }
            }
            activePawnPositions.Sort();

            Console.Write("Select Pawn: { ");
            for (int i = 0; i < activePawnPositions.Count(); i++)
            {
                if (i == activePawnPositions.Count() - 1)
                {
                    Console.WriteLine(activePawnPositions.ElementAt(i) + " }");
                }
                else
                {
                    Console.Write(activePawnPositions.ElementAt(i) + ", ");
                }
                
            }

            int input = GetInputPawnToMove(activePawnPositions);
            return this.GetPawnAt(input);
        }

        private int GetInputPawnToMove(List<int> activePawnPositions)
        {
            int input;
            try
            {
                input = int.Parse(Console.ReadLine());
                if (activePawnPositions.Contains(input)) {
                    return input;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input!");
                return GetInputPawnToMove(activePawnPositions);
            }
        }

        public override MoveCard GetMoveCardToUse(Game game)
        {
            return null;
        }
    }
}
