using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    abstract class Player
    {
        public string name;
        public string symbol;
        public MoveCard[] cards;
        public Pawn[] pawns;

        // Create a new player
        public Player (string name, string symbol)
        {
            this.name = name;
            this.symbol = symbol.ToLower().Substring(0, 1);
            this.cards = new MoveCard[2];
            CreatePawns();
        }

        // Create five new pawns for this player
        private void CreatePawns()
        {
            this.pawns = new Pawn[] {
                new Pawn(this.symbol),
                new Pawn(this.symbol),
                new Pawn(this.symbol),
                new Pawn(this.symbol),
                new Pawn(this.symbol)
            };
        }

        // Return true if this player has a pawn at the given board position, false if not
        public bool HasPawnAt(int pos)
        {
            foreach (Pawn pawn in pawns)
            {
                if (pawn.pos == pos && !pawn.isCaptured)
                {
                    return true;
                }
            }
            return false;
        }

        // Return the pawn at the given position, or null if none exists
        public Pawn GetPawnAt(int pos)
        {
            foreach (Pawn pawn in pawns)
            {
                if (pawn.pos == pos)
                {
                    return pawn;
                }
            }
            return null;
        }

        // Return the number of active (uncaptured) pawns the player has
        public int CountActivePawns()
        {
            int count = 0;
            foreach (Pawn pawn in pawns)
            {
                if (!pawn.isCaptured)
                {
                    count++;
                }
            }
            return count;
        }

        // Ask the player which pawn will be moved
        public abstract Pawn GetPawnToMove(Game game);

        // Ask the player which move card will be used
        public abstract MoveCard GetMoveCardToUse(Game game);
    }
}
