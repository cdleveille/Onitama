using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Game
    {
        private Player p1, p2, toMove;
        private Deck deck;
        private MoveCard boardCard;

        // Create a new game
        public Game(Player p1, Player p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.deck = new Deck();
        }

        // Start the game
        public void Start()
        {
            DealMoveCards();
            SetUpPawns();
            GameLoop(this.p1);
        }

        // Main game logic flow
        private void GameLoop(Player toMoveFirst)
        {
            this.toMove = toMoveFirst;

            bool gameOver = false;
            while (!gameOver)
            {
                PrintGameState();
                Console.WriteLine(this.toMove.name + " (" + this.toMove.symbol + ") to move");
                Pawn pawnToMove = this.toMove.GetPawnToMove(this);
            }
        }

        // Shuffle the deck, deal two move cards to each player, and one to the board
        private void DealMoveCards()
        {
            this.deck.Shuffle();
            this.p1.cards[0] = this.deck.Deal();
            this.p1.cards[1] = this.deck.Deal();
            this.p2.cards[0] = this.deck.Deal();
            this.p2.cards[1] = this.deck.Deal();
            this.boardCard = this.deck.Deal();
        }

        // Arrange the pawns for the start of the game
        private void SetUpPawns()
        {
            int i = 20;
            foreach (Pawn pawn in this.p1.pawns)
            {
                pawn.pos = i;
                pawn.isMaster = i == 22 ? true : false;
                pawn.symbol = pawn.isMaster ? pawn.symbol.ToUpper() : pawn.symbol;
                i++;
            }

            i = 0;
            foreach (Pawn pawn in this.p2.pawns)
            {
                pawn.pos = i;
                pawn.isMaster = i == 2 ? true : false;
                pawn.symbol = pawn.isMaster ? pawn.symbol.ToUpper() : pawn.symbol;
                i++;
            }
        }

        // Show the current state of the game
        public void PrintGameState()
        {
            string[] board = new string[25];
            string[] p1MoveCard0 = new string[25];
            string[] p1MoveCard1 = new string[25];
            string[] p2MoveCard0 = new string[25];
            string[] p2MoveCard1 = new string[25];
            string[] boardCard = new string[25];

            for (int i = 0; i < 25; i++)
            {
                if (this.p1.HasPawnAt(i))
                {
                    board[i] = this.p1.GetPawnAt(i).ToString();
                }
                else if (this.p2.HasPawnAt(i))
                {
                    board[i] = this.p2.GetPawnAt(i).ToString();
                }
                else
                {
                    board[i] = " ";
                }

                p1MoveCard0[i] = this.p1.cards[0].HasMoveAt(i) ? "." : " ";
                p1MoveCard1[i] = this.p1.cards[1].HasMoveAt(i) ? "." : " ";
                p2MoveCard0[i] = this.p2.cards[0].HasMoveAtP2(i) ? "." : " ";
                p2MoveCard1[i] = this.p2.cards[1].HasMoveAtP2(i) ? "." : " ";
                boardCard[i] = this.boardCard.HasMoveAt(i) ? "." : " "; //TODO: flip if it is p2's turn
            }

            int p1MoveCard0Offset = (11 - this.p1.cards[0].name.Length) / 2;
            int p1MoveCard1Offset = (11 - this.p1.cards[1].name.Length) / 2;
            int p2MoveCard0Offset = (11 - this.p2.cards[0].name.Length) / 2;
            int p2MoveCard1Offset = (11 - this.p2.cards[1].name.Length) / 2;
            int boardCardOffset = (11 - this.boardCard.name.Length) / 2;

            string nameLineP2 = "                  ";
            for (int i = 0; i < p2MoveCard0Offset; i++) { nameLineP2 += " "; }
            nameLineP2 += this.p2.cards[0].name;
            nameLineP2 = nameLineP2.PadRight(32, ' ');
            for (int i = 0; i < p2MoveCard1Offset; i++) { nameLineP2 += " "; }
            nameLineP2 += this.p2.cards[1].name;
            Console.WriteLine(nameLineP2);
            Console.WriteLine("                  |" + p2MoveCard0[0] + "|" + p2MoveCard0[1] + "|" + p2MoveCard0[2] + "|" + p2MoveCard0[3] + "|" + p2MoveCard0[4] + "| 	|" + p2MoveCard1[0] + "|" + p2MoveCard1[1] + "|" + p2MoveCard1[2] + "|" + p2MoveCard1[3] + "|" + p2MoveCard1[4] + "|");
            Console.WriteLine("                  |" + p2MoveCard0[5] + "|" + p2MoveCard0[6] + "|" + p2MoveCard0[7] + "|" + p2MoveCard0[8] + "|" + p2MoveCard0[9] + "| 	|" + p2MoveCard1[5] + "|" + p2MoveCard1[6] + "|" + p2MoveCard1[7] + "|" + p2MoveCard1[8] + "|" + p2MoveCard1[9] + "|");
            Console.WriteLine("                  |" + p2MoveCard0[10] + "|" + p2MoveCard0[11] + "|+|" + p2MoveCard0[13] + "|" + p2MoveCard0[14] + "|	|" + p2MoveCard1[10] + "|" + p2MoveCard1[11] + "|+|" + p2MoveCard1[13] + "|" + p2MoveCard1[14] + "|");
            Console.WriteLine("                  |" + p2MoveCard0[15] + "|" + p2MoveCard0[16] + "|" + p2MoveCard0[17] + "|" + p2MoveCard0[18] + "|" + p2MoveCard0[19] + "| 	|" + p2MoveCard1[15] + "|" + p2MoveCard1[16] + "|" + p2MoveCard1[17] + "|" + p2MoveCard1[18] + "|" + p2MoveCard1[19] + "|");
            Console.WriteLine("                  |" + p2MoveCard0[20] + "|" + p2MoveCard0[21] + "|" + p2MoveCard0[22] + "|" + p2MoveCard0[23] + "|" + p2MoveCard0[24] + "| 	|" + p2MoveCard1[20] + "|" + p2MoveCard1[21] + "|" + p2MoveCard1[22] + "|" + p2MoveCard1[23] + "|" + p2MoveCard1[24] + "|");
            Console.WriteLine();
            Console.WriteLine("                    |---|---|~~~|---|---|");
            Console.WriteLine("              0-4   | " + board[0] + " | " + board[1] + " | " + board[2] + " | " + board[3] + " | " + board[4] + " |");
            Console.WriteLine("                    |---|---|---|---|---|");
            Console.WriteLine("              5-9   | " + board[5] + " | " + board[6] + " | " + board[7] + " | " + board[8] + " | " + board[9] + " |   |" + boardCard[0] + "|" + boardCard[1] + "|" + boardCard[2] + "|" + boardCard[3] + "|" + boardCard[4] + "|");
            Console.WriteLine("                    |---|---|---|---|---|   |" + boardCard[5] + "|" + boardCard[6] + "|" + boardCard[7] + "|" + boardCard[8] + "|" + boardCard[9] + "|");
            Console.WriteLine("            10-14   | " + board[10] + " | " + board[11] + " | " + board[12] + " | " + board[13] + " | " + board[14] + " |   |" + boardCard[10] + "|" + boardCard[11] + "|+|" + boardCard[13] + "|" + boardCard[14] + "|");
            Console.WriteLine("                    |---|---|---|---|---|   |" + boardCard[15] + "|" + boardCard[16] + "|" + boardCard[17] + "|" + boardCard[18] + "|" + boardCard[19] + "|");
            Console.WriteLine("            15-19   | " + board[15] + " | " + board[16] + " | " + board[17] + " | " + board[18] + " | " + board[19] + " |   |" + boardCard[20] + "|" + boardCard[21] + "|" + boardCard[22] + "|" + boardCard[23] + "|" + boardCard[24] + "|");
            Console.Write("                    |---|---|---|---|---|   ");
            for (int i = 0; i < boardCardOffset; i++) { Console.Write(" "); }
            Console.WriteLine(this.boardCard.name);
            Console.WriteLine("            20-24   | " + board[20] + " | " + board[21] + " | " + board[22] + " | " + board[23] + " | " + board[24] + " |");
            Console.WriteLine("                    |---|---|~~~|---|---|");
            Console.WriteLine();
            Console.WriteLine("                  |" + p1MoveCard0[0] + "|" + p1MoveCard0[1] + "|" + p1MoveCard0[2] + "|" + p1MoveCard0[3] + "|" + p1MoveCard0[4] + "| 	|" + p1MoveCard1[0] + "|" + p1MoveCard1[1] + "|" + p1MoveCard1[2] + "|" + p1MoveCard1[3] + "|" + p1MoveCard1[4] + "|");
            Console.WriteLine("                  |" + p1MoveCard0[5] + "|" + p1MoveCard0[6] + "|" + p1MoveCard0[7] + "|" + p1MoveCard0[8] + "|" + p1MoveCard0[9] + "| 	|" + p1MoveCard1[5] + "|" + p1MoveCard1[6] + "|" + p1MoveCard1[7] + "|" + p1MoveCard1[8] + "|" + p1MoveCard1[9] + "|");
            Console.WriteLine("                  |" + p1MoveCard0[10] + "|" + p1MoveCard0[11] + "|+|" + p1MoveCard0[13] + "|" + p1MoveCard0[14] + "|	|" + p1MoveCard1[10] + "|" + p1MoveCard1[11] + "|+|" + p1MoveCard1[13] + "|" + p1MoveCard1[14] + "|");
            Console.WriteLine("                  |" + p1MoveCard0[15] + "|" + p1MoveCard0[16] + "|" + p1MoveCard0[17] + "|" + p1MoveCard0[18] + "|" + p1MoveCard0[19] + "| 	|" + p1MoveCard1[15] + "|" + p1MoveCard1[16] + "|" + p1MoveCard1[17] + "|" + p1MoveCard1[18] + "|" + p1MoveCard1[19] + "|");
            Console.WriteLine("                  |" + p1MoveCard0[20] + "|" + p1MoveCard0[21] + "|" + p1MoveCard0[22] + "|" + p1MoveCard0[23] + "|" + p1MoveCard0[24] + "| 	|" + p1MoveCard1[20] + "|" + p1MoveCard1[21] + "|" + p1MoveCard1[22] + "|" + p1MoveCard1[23] + "|" + p1MoveCard1[24] + "|");

            string nameLineP1 = "                  ";
            for (int i = 0; i < p1MoveCard0Offset; i++) { nameLineP1 += " "; }
            nameLineP1 += this.p1.cards[0].name;
            nameLineP1 = nameLineP1.PadRight(32, ' ');
            for (int i = 0; i < p1MoveCard1Offset; i++) { nameLineP1 += " "; }
            nameLineP1 += this.p1.cards[1].name;
            Console.WriteLine(nameLineP1);
            Console.WriteLine();
        }
    }
}
