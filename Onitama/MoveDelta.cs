using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class MoveDelta
    {
        private int deltaX;
        private int deltaY;

        // Create a new move delta
        public MoveDelta(int deltaX, int deltaY)
        {
            this.deltaX = deltaX;
            this.deltaY = deltaY;
        }

        public int GetDeltaX()
        {
            return this.deltaX;
        }

        public void SetDeltaX(int deltaX)
        {
            this.deltaX = deltaX;
        }

        public int GetDeltaY()
        {
            return this.deltaY;
        }

        public void SetDeltaY(int deltaY)
        {
            this.deltaY = deltaY;
        }
    }
}
