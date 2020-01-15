using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class HumanPlayer : Player
    {
        // Create a new human player
        public HumanPlayer(string name, string symbol) : base(name, symbol) {}

        // Generate a list of uncaptured pawns and ask the player to choose one to move
        public override Pawn GetPawnToMove(Game game)
        {
            int value = EvaluateBoardstate(game.GetBoardstate());

            List<int> activePawnPositions = new List<int>();
            for (int i = 0; i < this.GetPawns().Count; i++)
            {
                if (!this.GetPawns()[i].GetIsCaptured())
                {
                    activePawnPositions.Add(this.GetPawns()[i].GetPos());
                }
            }
            activePawnPositions.Sort();

            Console.Write("Select Pawn: { ");
            for (int i = 0; i < activePawnPositions.Count(); i++)
            {
                if (i != activePawnPositions.Count() - 1)
                {
                    Console.Write(activePawnPositions.ElementAt(i) + ", ");
                }
                else
                {
                    Console.WriteLine(activePawnPositions.ElementAt(i) + " }");
                }
                
            }

            int input = GetInputPawnToMove(activePawnPositions);
            return this.GetPawnAt(input);
        }

        // Capture and validate input from the player on which pawn to move
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

        // List the player's two move cards and ask which one to use
        public override MoveCard GetMoveCardToUse(Game game, Player playerToMove)
        {
            Console.Write("Select Move Card: { ");
            Console.WriteLine(playerToMove.GetCards()[0].GetName() + " [1], " + playerToMove.GetCards()[1].GetName() + " [2] }");

            int input = GetInputMoveCardToUse();
            return this.GetCards()[input - 1];
        }

        // Capture and validate input from the player on which move card to use
        private int GetInputMoveCardToUse()
        {
            int input;
            try
            {
                input = int.Parse(Console.ReadLine());
                if (input == 1 || input == 2)
                {
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
                return GetInputMoveCardToUse();
            }
        }

        // List all valid destination positions for the chosen pawn/move card and ask the player to choose one
        public override int GetPawnToMoveDestination(Game game, List<int> validDestinations)
        {
            Console.Write("Select Pawn Destination: { ");

            validDestinations.Sort();
            for (int i = 0; i < validDestinations.Count(); i++)
            {
                if (i != validDestinations.Count() - 1)
                {
                    Console.Write(validDestinations[i] + ", ");
                }
                else
                {
                    Console.WriteLine(validDestinations[i] + " }");
                }
            }
            
            return GetInputPawnToMoveDestination(validDestinations);
        }

        // Capture and validate input from the player on which destination position to use
        private int GetInputPawnToMoveDestination(List<int> validDestinations)
        {
            int input;
            try
            {
                input = int.Parse(Console.ReadLine());
                if (validDestinations.Contains(input))
                {
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
                return GetInputPawnToMoveDestination(validDestinations);
            }
        }
    }
}
