using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class ComputerPlayer : Player
    {
        // Create a new computer player
        public ComputerPlayer(string name, string symbol) : base(name, symbol) { }

        // Generate a list of uncaptured pawns and ask the player to choose one to move
        public override Pawn GetPawnToMove(Game game)
        {
            return null;
        }

        // List the player's two move cards and ask which one to use
        public override MoveCard GetMoveCardToUse(Game game, Player playerToMove)
        {
            return null;
        }

        // List all valid destination positions for the chosen pawn/move card and ask the player to choose one
        public override int GetPawnToMoveDestination(Game game, List<int> validDestinations)
        {
            return -1;
        }

        
    }
}
