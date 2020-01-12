using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Game
    {
        private Player p1, p2;
        private Deck deck;
        private MoveCard boardCard;

        public Game(Player p1, Player p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.deck = new Deck();
        }

        public void Start()
        {
            DealMoveCards();
            SetUpPawns();
            GameLoop();
        }

        private void GameLoop()
        {
            PrintGameState();
            Console.ReadLine();
        }

        private void DealMoveCards()
        {
            this.deck.Shuffle();
            this.p1.cards[0] = this.deck.Deal();
            this.p1.cards[1] = this.deck.Deal();
            this.p2.cards[0] = this.deck.Deal();
            this.p2.cards[1] = this.deck.Deal();
            this.boardCard = this.deck.Deal();
        }

        private void SetUpPawns()
        {
            int i = 20;
            foreach (Pawn pawn in this.p1.pawns)
            {
                pawn.pos = i;
                pawn.isMaster = i == 22 ? true : false;
                i++;
            }

            i = 0;
            foreach (Pawn pawn in this.p2.pawns)
            {
                pawn.pos = i;
                pawn.isMaster = i == 2 ? true : false;
                i++;
            }
        }

        public void PrintGameState()
        {
            Console.WriteLine("Hello world!");
        }
    }
}
