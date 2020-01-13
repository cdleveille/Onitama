using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Pawn
    {
        private int pos;
        private bool isMaster, isCaptured;
        private string symbol;

        // Create a new pawn
        public Pawn(string symbol)
        {
            this.symbol = symbol;
            this.isCaptured = false;
        }

        public int GetPos()
        {
            return this.pos;
        }

        public void SetPos(int pos)
        {
            this.pos = pos;
        }

        public bool GetIsMaster()
        {
            return this.isMaster;
        }

        public void SetIsMaster(bool isMaster)
        {
            this.isMaster = isMaster;
        }

        public bool GetIsCaptured()
        {
            return this.isCaptured;
        }

        public void SetIsCaptured(bool isCaptured)
        {
            this.isCaptured = isCaptured;
        }

        public string GetSymbol()
        {
            return this.symbol;
        }

        public void SetSymbol(string symbol)
        {
            this.symbol = symbol;
        }
    }
}
