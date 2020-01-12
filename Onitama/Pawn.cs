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

        public Pawn(string symbol)
        {
            this.isCaptured = false;
            this.symbol = symbol;
        }

        public String toString()
        {
            return symbol;
        }
    }
}
