using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Pawn
    {
        public Boolean isMaster, isCaptured;
        public char symbol;
        public int pos;

        public Pawn(Boolean isMaster, char symbol)
        {
            this.isMaster = isMaster;
            this.isCaptured = false;
            this.symbol = symbol;
        }
    }
}
