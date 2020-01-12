using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Deck
    {
        public List<MoveCard> cards;
        private Random rng;
        private int top;

        public Deck()
        {
            this.cards = new List<MoveCard>();
            this.cards.Add(new MoveCard("Crab", new int[] { 7, 10, 14 }));
            this.cards.Add(new MoveCard("Dragon", new int[] { 5, 9, 16, 18 }));
            this.cards.Add(new MoveCard("Elephant", new int[] { 6, 8, 11, 13 }));
            this.cards.Add(new MoveCard("Goose", new int[] { 6, 11, 13, 18 }));
            this.cards.Add(new MoveCard("Tiger", new int[] { 2, 17 }));

            this.rng = new Random();
            this.top = 0;
        }

        public MoveCard Deal()
        {
            this.top++;
            return this.cards.ElementAt<MoveCard>(top - 1);
        }

        public void Shuffle()
        {
            this.cards = this.cards.OrderBy(a => rng.Next()).ToList();
            this.top = 0;
        }
    }
}