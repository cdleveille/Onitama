using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Pawn
    {
        public bool isMaster, isCaptured;
        public string symbol;
        public int pos;

        // Create a new pawn
        public Pawn(string symbol)
        {
            this.symbol = symbol;
            this.isCaptured = false;
        }

        // Print the symbol of this pawn
        public override String ToString()
        {
            return symbol;
        }
    }
}
