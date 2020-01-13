using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Deck
    {
        private List<MoveCard> cards;
        private Random rng;
        private int top;

        // Create a new deck
        public Deck()
        {
            this.cards = new List<MoveCard>();
            this.cards.Add(new MoveCard("Boar", new int[] { 7, 11, 13 }));
            this.cards.Add(new MoveCard("Cobra", new int[] { 8, 11, 18 }));
            this.cards.Add(new MoveCard("Crab", new int[] { 7, 10, 14 }));
            this.cards.Add(new MoveCard("Crane", new int[] { 7, 16, 18 }));
            this.cards.Add(new MoveCard("Dragon", new int[] { 5, 9, 16, 18 }));
            this.cards.Add(new MoveCard("Eel", new int[] { 6, 13, 16 }));
            this.cards.Add(new MoveCard("Elephant", new int[] { 6, 8, 11, 13 }));
            this.cards.Add(new MoveCard("Frog", new int[] { 6, 10, 18 }));
            this.cards.Add(new MoveCard("Goose", new int[] { 6, 11, 13, 18 }));
            this.cards.Add(new MoveCard("Horse", new int[] { 7, 11, 17 }));
            this.cards.Add(new MoveCard("Mantis", new int[] { 6, 8, 17 }));
            this.cards.Add(new MoveCard("Monkey", new int[] { 6, 8, 16, 18 }));
            this.cards.Add(new MoveCard("Ox", new int[] { 7, 13, 17 }));
            this.cards.Add(new MoveCard("Rabbit", new int[] { 8, 14, 16}));
            this.cards.Add(new MoveCard("Rooster", new int[] { 8, 11, 13, 16}));
            this.cards.Add(new MoveCard("Tiger", new int[] { 2, 17 }));

            this.rng = new Random();
            this.top = 0;
        }

        // Deal one card from the deck
        public MoveCard Deal()
        {
            this.top++;
            return this.cards.ElementAt<MoveCard>(top - 1);
        }

        // Randomize the order of the cards in the deck
        public void Shuffle()
        {
            this.cards = this.cards.OrderBy(a => rng.Next()).ToList();
            this.top = 0;
        }

        public List<MoveCard> GetCards()
        {
            return this.cards;
        }

        public void SetCards(List<MoveCard> cards)
        {
            this.cards = cards;
        }

        public Random GetRng()
        {
            return this.rng;
        }

        public void SetRng(Random rng)
        {
            this.rng = rng;
        }

        public int GetTop()
        {
            return this.top;
        }

        public void SetTop(int top)
        {
            this.top = top;
        }
    }
}