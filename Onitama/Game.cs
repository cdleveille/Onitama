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
        private bool isP1Turn;

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
            Console.Clear();
            DealMoveCards();
            SetUpPawns();

            this.isP1Turn = true;
            GameLoop(this.p1, this.p2);
        }

        // Main game logic flow - each loop represents one turn
        private void GameLoop(Player playerToMove, Player playerToMoveNext)
        {
            string winMessage = "";
            while (winMessage == "")
            {
                // Show current state of the game
                PrintGameState();
                Console.WriteLine(playerToMove.GetName() + " (" + playerToMove.GetSymbol().ToUpper() + ") to move");

                Pawn pawnToMove = null;
                MoveCard moveCardToUse = null;
                List<int> validDestinations = null;

                // Get from acting player: pawn to move, move card, move destination
                bool needInput = true;
                while (needInput)
                {
                    pawnToMove = playerToMove.GetPawnToMove(this);
                    moveCardToUse = playerToMove.GetMoveCardToUse(this, playerToMove);

                    validDestinations = GetValidMoveDestinations(playerToMove, pawnToMove, moveCardToUse);
                    if (validDestinations.Count > 0)
                    {
                        needInput = false;
                    }
                    else
                    {
                        Console.WriteLine("Selected Pawn & Move Card combo has no valid moves!");
                    }
                }
                int pawnToMoveDestination = playerToMove.GetPawnToMoveDestination(this, validDestinations);

                Console.Write(playerToMove.GetName() + " (" + playerToMove.GetSymbol().ToUpper() + ") played " + moveCardToUse.GetName() + " - moved from " + pawnToMove.GetPos() + " to " + pawnToMoveDestination);

                // Update the position of the pawn being moved
                pawnToMove.SetPos(pawnToMoveDestination);

                // If a master pawn has been moved to the opponent's temple square, end the game (Way Of The Stream)
                if ((this.isP1Turn && pawnToMove.GetIsMaster() && pawnToMove.GetPos() == 2) || (!this.isP1Turn && pawnToMove.GetIsMaster() && pawnToMove.GetPos() == 22))
                {
                    winMessage = "Game Over! " + playerToMove.GetName() + " (" + playerToMove.GetSymbol().ToUpper() + ") wins by Way Of The Stream!";
                }

                // If an opponent pawn occupies the destination position, capture it
                foreach (Pawn pawn in playerToMoveNext.GetPawns())
                {
                    if (pawn.GetPos() == pawnToMove.GetPos())
                    {
                        pawn.SetIsCaptured(true);
                        Console.Write(" (captured pawn)");

                        // If the opponent's master pawn was captured, end the game (Way Of The Stone)
                        if (pawn.GetIsMaster())
                        {
                            winMessage = "Game Over! " + playerToMove.GetName() + " (" + playerToMove.GetSymbol().ToUpper() + ") wins by Way Of The Stone!";
                        }
                    }
                }
                Console.WriteLine();

                // Exchange used move card with board card
                MoveCard tempCard = moveCardToUse;
                playerToMove.GetCards().Remove(moveCardToUse);
                playerToMove.GetCards().Add(this.boardCard);
                this.boardCard = tempCard;

                // Swap player variables for next turn
                Player tempPlayer = playerToMoveNext;
                playerToMoveNext = playerToMove;
                playerToMove = tempPlayer;
                this.isP1Turn = !this.isP1Turn;
            }

            // Game Over
            PrintGameState();
            Console.WriteLine(winMessage);
        }

        // Shuffle the deck, deal two move cards to each player, and one to the board
        private void DealMoveCards()
        {
            this.deck.Shuffle();
            this.p1.GetCards().Add(this.deck.Deal());
            this.p1.GetCards().Add(this.deck.Deal());
            this.p2.GetCards().Add(this.deck.Deal());
            this.p2.GetCards().Add(this.deck.Deal());
            this.boardCard = this.deck.Deal();
        }

        // Arrange the pawns for the start of the game
        private void SetUpPawns()
        {
            int i = 20;
            foreach (Pawn pawn in this.p1.GetPawns())
            {
                pawn.SetPos(i);
                if (i == 22)
                {
                    pawn.SetIsMaster(true);
                }
                else
                {
                    pawn.SetIsMaster(false);
                }
                if (pawn.GetIsMaster())
                {
                    pawn.SetSymbol(pawn.GetSymbol().ToUpper());
                }
                i++;
            }

            i = 0;
            foreach (Pawn pawn in this.p2.GetPawns())
            {
                pawn.SetPos(i);
                if (i == 2)
                {
                    pawn.SetIsMaster(true);
                }
                else
                {
                    pawn.SetIsMaster(false);
                }
                if (pawn.GetIsMaster())
                {
                    pawn.SetSymbol(pawn.GetSymbol().ToUpper());
                }
                i++;
            }
        }

        // Return a list of valid move destinations based on the given player, pawn, and move card
        private List<int> GetValidMoveDestinations(Player playerToMove, Pawn pawnToMove, MoveCard moveCardToUse)
        {
            int pawnInitialPos = pawnToMove.GetPos();
            List<int> candidateDestinations = new List<int>();
            int yFlip = this.isP1Turn ? -1 : 1;

            // Populate destinations list based on the selected move card
            for (int i = 0; i < moveCardToUse.GetMoveDeltas().Length; i++)
            {
                candidateDestinations.Add(pawnInitialPos + moveCardToUse.GetMoveDeltas()[i].GetDeltaX() + ((moveCardToUse.GetMoveDeltas()[i].GetDeltaY() * 5) * yFlip));
            }

            // Remove destinations that contain a friendly pawn
            for (int i = 0; i < playerToMove.GetPawns().Count; i++)
            {
                if (candidateDestinations.Contains(playerToMove.GetPawns()[i].GetPos()) && !playerToMove.GetPawns()[i].GetIsCaptured()) {
                    candidateDestinations.Remove(playerToMove.GetPawns()[i].GetPos());
                }
            }

            // Remove destinations that are out of bounds
            List<int> validDestinations = ValidDestinationLookup(pawnInitialPos);
            List<int> finalDestinations = new List<int>();
            foreach (int dest in candidateDestinations)
            {
                if (validDestinations.Contains(dest))
                {
                    finalDestinations.Add(dest);
                }
            }
            
            return finalDestinations;
        }

        // Return a list of valid move destinations based on the given position (regardless of move card)
        private List<int> ValidDestinationLookup(int pos)
        {
            switch (pos)
            {
                //return new List<int>() {  };
                case 0: return new List<int>() { 1, 2, 5, 6, 7, 10, 11, 12 };
                case 1: return new List<int>() { 0, 2, 3, 5, 6, 7, 8, 10, 11, 12, 13 };
                case 2: return new List<int>() { 0, 1, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                case 3: return new List<int>() { 1, 2, 4, 6, 7, 8, 9, 11, 12, 13, 14 };
                case 4: return new List<int>() { 2, 3, 7, 8, 9, 12, 13, 14 };
                case 5: return new List<int>() { 0, 1, 2, 6, 7, 10, 11, 12, 15, 16, 17 };
                case 6: return new List<int>() { 0, 1, 2, 3, 5, 7, 8, 10, 11, 12, 13, 15, 16, 17, 18 };
                case 7: return new List<int>() { 0, 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                case 8: return new List<int>() { 1, 2, 3, 4, 6, 7, 9, 11, 12, 13, 14, 16, 17, 18, 19 };
                case 9: return new List<int>() { 2, 3, 4, 7, 8, 12, 13, 14, 17, 18, 19 };
                case 10: return new List<int>() { 0, 1, 2, 5, 6, 7, 11, 12, 15, 16, 17, 20, 21, 22 };
                case 11: return new List<int>() { 0, 1, 2, 3, 5, 6, 7, 8, 10, 12, 13, 15, 16, 17, 18, 20, 21, 22, 23 };
                case 12: return new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
                case 13: return new List<int>() { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 14, 16, 17, 18, 19, 21, 22, 23, 24 };
                case 14: return new List<int>() { 2, 3, 4, 7, 8, 9, 12, 13, 17, 18, 19, 22, 23, 24 };
                case 15: return new List<int>() { 5, 6, 7, 10, 11, 12, 16, 17, 20, 21, 22 };
                case 16: return new List<int>() { 5, 6, 7, 8, 10, 11, 12, 13, 15, 17, 18, 20, 21, 22, 23 };
                case 17: return new List<int>() { 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 19, 20, 21, 22, 23, 24 };
                case 18: return new List<int>() { 6, 7, 8, 9, 11, 12, 13, 14, 16, 17, 19, 21, 22, 23, 24 };
                case 19: return new List<int>() { 7, 8, 9, 12, 13, 14, 17, 18, 22, 23, 24 };
                case 20: return new List<int>() { 10, 11, 12, 15, 16, 17, 21, 22 };
                case 21: return new List<int>() { 10, 11, 12, 13, 15, 16, 17, 18, 20, 22, 23 };
                case 22: return new List<int>() { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 21, 23, 24 };
                case 23: return new List<int>() { 11, 12, 13, 14, 16, 17, 18, 19, 21, 22, 24 };
                case 24: return new List<int>() { 12, 13, 14, 17, 18, 19, 22, 23 };
                default: return new List<int>() {};
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
                    board[i] = this.p1.GetPawnAt(i).GetSymbol();
                }
                else if (this.p2.HasPawnAt(i))
                {
                    board[i] = this.p2.GetPawnAt(i).GetSymbol();
                }
                else
                {
                    board[i] = " ";
                }

                p1MoveCard0[i] = this.p1.GetCards()[0].HasMoveAt(i) ? "." : " ";
                p1MoveCard1[i] = this.p1.GetCards()[1].HasMoveAt(i) ? "." : " ";
                p2MoveCard0[i] = this.p2.GetCards()[0].HasMoveAtP2(i) ? "." : " ";
                p2MoveCard1[i] = this.p2.GetCards()[1].HasMoveAtP2(i) ? "." : " ";
                if (this.isP1Turn)
                {
                    boardCard[i] = this.boardCard.HasMoveAt(i) ? "." : " ";
                }
                else
                {
                    boardCard[i] = this.boardCard.HasMoveAtP2(i) ? "." : " ";
                }
            }

            int p1MoveCard0Offset = (11 - this.p1.GetCards()[0].GetName().Length) / 2;
            int p1MoveCard1Offset = (11 - this.p1.GetCards()[1].GetName().Length) / 2;
            int p2MoveCard0Offset = (11 - this.p2.GetCards()[0].GetName().Length) / 2;
            int p2MoveCard1Offset = (11 - this.p2.GetCards()[1].GetName().Length) / 2;
            int boardCardOffset = (11 - this.boardCard.GetName().Length) / 2;

            string nameLineP2 = "                  ";
            for (int i = 0; i < p2MoveCard0Offset; i++) { nameLineP2 += " "; }
            nameLineP2 += this.p2.GetCards()[0].GetName();
            nameLineP2 = nameLineP2.PadRight(32, ' ');
            for (int i = 0; i < p2MoveCard1Offset; i++) { nameLineP2 += " "; }
            nameLineP2 += this.p2.GetCards()[1].GetName();
            Console.WriteLine();
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
            Console.WriteLine(this.boardCard.GetName());
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
            nameLineP1 += this.p1.GetCards()[0].GetName();
            nameLineP1 = nameLineP1.PadRight(32, ' ');
            for (int i = 0; i < p1MoveCard1Offset; i++) { nameLineP1 += " "; }
            nameLineP1 += this.p1.GetCards()[1].GetName();
            Console.WriteLine(nameLineP1);
            Console.WriteLine();
        }
    }
}
