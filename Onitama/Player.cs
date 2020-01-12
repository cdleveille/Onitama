using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Player
    {
        public string name;
        public string symbol;
        public MoveCard[] cards;
        public Pawn[] pawns;

        public Player (string name, string symbol)
        {
            this.name = name;
            this.symbol = symbol.ToLower().Substring(0, 1);
            this.cards = new MoveCard[2];
            CreatePawns();
        }

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

        //public abstract void getMove();
    }
}
