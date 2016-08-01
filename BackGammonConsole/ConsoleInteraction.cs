using System;
using Backgammon;
using BackGammonLogic;

namespace BackGammonConsole
{
    public class ConsoleInteraction
    {
        private string originColumn;
        internal void StartGame()
        {
            Console.WriteLine("Welcome to the best game in the world!!");
            string player1 = GetPlayer();
            string player2 = GetPlayer();

            var gameLogic = new GameManager(player1,player2);
            while (gameLogic.IsGameEnded==false)
            {
                Console.Clear();
                DrawBoard(gameLogic);
                displayDiceResults(gameLogic);
                
                while (gameLogic.CanMoveBeMade())
                {
                    GetOriginColumnFromUser(gameLogic);
                    GetTargetColumnFromUser(gameLogic);
                    Console.Clear();
                    DrawBoard(gameLogic);
                    displayDiceResults(gameLogic);
                }
                    Console.WriteLine("you have no moves to make");   
            }
           

            Console.Clear();
            DrawBoard(gameLogic);
            Console.WriteLine("game over!!!! " + gameLogic.Winner +" you are the winner");
            Console.ReadLine();
        }
        private void GetOriginColumnFromUser(GameManager gameLogic)
        {
            if (gameLogic.CurrntPlayer.IsComputer)
            {
                originColumn = gameLogic.GetComputerChoice().ToString();


                if (gameLogic.CanMoveBeMade())
                {
                    while (!gameLogic.SetOriginColumn(originColumn) && gameLogic.CanMoveBeMade())
                    {
                        originColumn = gameLogic.GetComputerChoice().ToString();

                    }
                }


            }
            else
            {
                originColumn = Console.ReadLine();

                while (!gameLogic.SetOriginColumn(originColumn) && gameLogic.CanMoveBeMade())
                {
                    Console.Clear();
                    DrawBoard(gameLogic);
                    Console.WriteLine("you have entered an invalid column please Choose a column to move from");
                    originColumn = Console.ReadLine();
                }
                Console.WriteLine("you chose to move form column" + originColumn);

            }

        }
        private void GetTargetColumnFromUser(GameManager gameLogic)
        {
            string userInput;
            if (gameLogic.CurrntPlayer.IsComputer)
            {
                userInput=gameLogic.GetComputerChoice().ToString();
 
                    while (!gameLogic.SetTargetColumn(userInput)&&gameLogic.CanMoveBeMade())
                    {
                        userInput = gameLogic.GetComputerChoice().ToString();
                    }
                
            }
            else
            {
                userInput = Console.ReadLine();

                while (!gameLogic.SetTargetColumn(userInput) && gameLogic.CanMoveBeMade())
                    {
                        Console.Clear();
                        DrawBoard(gameLogic);
                        Console.WriteLine("you have entered an invalid column please enter a column to move to");
                        Console.WriteLine("you chose column number " + originColumn);
                        Console.Write("your dice results are");
                        foreach (var diceResult in gameLogic.DiceResults)
                        {
                            Console.Write(" " + diceResult);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Please choose again");
                        userInput = Console.ReadLine();
                    }
                DrawBoard(gameLogic);
                Console.WriteLine("you moved to column" + userInput);
                
            }

        }


        private void displayDiceResults(GameManager gameLogic)
        {
            Console.WriteLine("its {0} turn", gameLogic.CurrntPlayer.Name);
            Console.Write("your dice results are:");
            foreach (var result in gameLogic.DiceResults)
            {
                Console.Write(" "+ result);
            }
            Console.WriteLine();
        }

        public void DrawBoard(GameManager gameLogic)
        {
            Column[] board = gameLogic.GetBoardState();
            for (
                int i = 0; i <= 11; i++)
            {
                Console.Write("{0,3}",i);
            } 
            Console.BackgroundColor = ConsoleColor.DarkRed;

            Console.WriteLine();

            #region
            for (int i = 0; i < 24; i++)
            {
                if (board[i].State == ColorsColumState.White)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("{0,3}", board[i].Count);
                }
                else if (board[i].State == ColorsColumState.Black)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,3}", board[i].Count);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("{0,3}", board[i].Count);
                }



                if (i == 11)
                {
                    Console.WriteLine();
                    Console.WriteLine();
                }
                #endregion

            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int j = 12; j < 24; j++)
            {
                Console.Write("{0,3}", j);
            }
            Console.WriteLine();
            Console.WriteLine("white prison has: " + gameLogic.WhitePrison);
            Console.WriteLine("Black prison has: "+gameLogic.BlackPrison);
            Console.WriteLine("white home has: "+gameLogic.WhiteHome);
            Console.WriteLine("black home has: "+gameLogic.BlackHome);
            Console.WriteLine();
            Console.WriteLine();
        }

        public string GetPlayer()
        {
               Console.WriteLine("please enter a players name (or press any key to add a computer player)");
               return Console.ReadLine();
         }
    }

}
