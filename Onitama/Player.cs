using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    abstract class Player
    {
        private string name;
        private string symbol;
        private List<MoveCard> cards;
        private List<Pawn> pawns;

        // Create a new player
        public Player (string name, string symbol)
        {
            this.name = name;
            this.symbol = symbol.ToLower().Substring(0, 1);
            this.cards = new List<MoveCard>();
            CreatePawns();
        }

        // Ask the player which pawn will be moved
        public abstract Pawn GetPawnToMove(Game game);

        // Ask the player which move card will be used
        public abstract MoveCard GetMoveCardToUse(Game game, Player playerToMove);

        // Ask the player where the selected pawn will be moved to based on the selected move card
        public abstract int GetPawnToMoveDestination(Game game, List<int> validDestinations);

        // Create five new pawns for this player
        private void CreatePawns()
        {
            this.pawns = new List<Pawn> {
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
                if (pawn.GetPos() == pos && !pawn.GetIsCaptured())
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
                if (pawn.GetPos() == pos)
                {
                    return pawn;
                }
            }
            return null;
        }

        public int Minimax(List<Pawn> node, int depth, bool isMaximizingPlayer)
        {
            if (depth == 0) // or is terminal node
            {
                return EvaluateBoardstate(node);
            }
            else
            {
                int value;
                if (isMaximizingPlayer)
                {
                    value = -9999;
                    
                }
                else
                {
                    value = 9999;
                }
            }

            return -1;
        }

        public int EvaluateBoardstate(List<Pawn> pawns)
        {
            int score = 0;
            for (int i = 0; i < pawns.Count; i++)
            {
                Pawn pawn = pawns[i];
                int shrineSquare = i < pawns.Count / 2 ? 2 : 22;
                if (!pawn.GetIsCaptured())
                {
                    if (pawn.GetSymbol() == this.GetSymbol())
                    {
                        if (pawn.GetIsMaster())
                        {
                            score += 10;

                            if (pawn.GetPos() == shrineSquare)
                            {
                                score += 5;
                            }
                        }
                        else
                        {
                            score += 1;
                        }
                    }
                    else
                    {
                        if (pawn.GetIsMaster())
                        {
                            score -= 10;
                        }
                        else
                        {
                            score -= 1;
                        }
                    }
                }
            }
            return score;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetSymbol()
        {
            return this.symbol;
        }

        public void SetSymbol(string symbol)
        {
            this.symbol = symbol;
        }

        public List<Pawn> GetPawns()
        {
            return this.pawns;
        }

        public void SetPawns(List<Pawn> pawns)
        {
            this.pawns = pawns;
        }

        public List<MoveCard> GetCards()
        {
            return this.cards;
        }

        public void SetCards(List<MoveCard> cards)
        {
            this.cards = cards;
        }
    }
}
