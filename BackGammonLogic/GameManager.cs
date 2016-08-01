using System.Collections.Generic;
using System.Diagnostics;
using Backgammon;
using ComputerPlayer;

namespace BackGammonLogic
{
    public class GameManager
    {

        public GameManager(string player1Name, string player2Name)
        {
            IsGameEnded = false;
            playersList.Add(string.IsNullOrEmpty(player1Name)
                ? new Player("comp1", ColorsColumState.White, true)
                : new Player(player1Name, ColorsColumState.White));

            playersList.Add(string.IsNullOrEmpty(player2Name)
                ? new Player("comp2", ColorsColumState.Black, true)
                : new Player(player2Name, ColorsColumState.Black));
            gameBoard.SetBoard();
            GetDiceResults();
            CurrntPlayer = playersList[0];
        }

        private readonly Board gameBoard = new Board();
        private readonly Dice dice = new Dice(1, 7);
        private readonly List<Player> playersList = new List<Player>();
        private int playerCounter;

        private int userInputOriginColumn;
        private int userInputSelectedColumn;
        public readonly List<int> DiceResults =new List<int>();
        private readonly AIPlayer ComputerPlayer = new AIPlayer();

        public Player CurrntPlayer { get; private set; }
        public bool IsGameEnded { get; private set; }
        public string Winner { get; private set; }

        public string WhitePrison
        {
            get { return gameBoard.WhitePrison.ToString(); }
        }
        public string BlackPrison
        {
            get { return gameBoard.BlackPrison.ToString(); }
        }
        public string WhiteHome
        {
            get { return gameBoard.WhiteCounter.ToString(); }
        }
        public string BlackHome
        {
            get { return gameBoard.BlackCounter.ToString(); }
        }


        //Methods

        public Column[] GetBoardState()
        {
            return gameBoard.GetBoardState();
        }
        public List<int> GetDiceResults()
        {
            if (DiceResults.Count == 0)
            {
                int result1 = dice.RollDice();
                int result2 = dice.RollDice();
                DiceResults.Add(result1);
                DiceResults.Add(result2);
                if (result1 == result2)
                {
                    DiceResults.Add(result1);
                    DiceResults.Add(result1);
                }
            }
            return DiceResults;
        }

        private void ChangePlayer()
        {
            if (playerCounter == 0)
            {
                playerCounter=1;
            }
            else
            {
                playerCounter = 0;
            }
            CurrntPlayer = playersList[playerCounter];

        }

        public bool CanMoveBeMade()
        {
            EndGame();

            if (!IsGameEnded)
            {
                if (DiceResults.Count > 0)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        if (ValidateColumn(i))
                        {
                            return true;
                        }
                    }
                }
            }
            DiceResults.Clear();
            DiceResults.TrimExcess();
            GetDiceResults();
                ChangePlayer();
                return false;

        }

        public bool ValidateColumn(int column)
        {
            int direction = 1;
            int whiteCheckerCounter;
            int BlackCheckerCounter;
            if (gameBoard.GetBoardState()[column].Count == 0 || gameBoard.GetBoardState()[column].State==ColorsColumState.Empty)
            {
                return false;
            }
            //check if white is at end game state
            #region
            if (CurrntPlayer.Color == ColorsColumState.White)
            {
                whiteCheckerCounter = gameBoard.WhiteCounter;
                for (int i = 17; i < 23; i++)
                {
                    if (gameBoard.GetBoardState()[i].State == ColorsColumState.White)
                    {
                        whiteCheckerCounter = whiteCheckerCounter + gameBoard.GetBoardState()[i].Count;
                    }
                }
                if (whiteCheckerCounter == 15)
                {

                        foreach (var result in DiceResults)
                        {
                            if (gameBoard.GetBoardState()[column].State == CurrntPlayer.Color && column <= result)
                            {
                                return true;
                            }
                        }
                    
                        return false;
                }
            }
            #endregion

            //check if black is at end game state
            #region
            if (CurrntPlayer.Color == ColorsColumState.Black)
            {
                BlackCheckerCounter = gameBoard.BlackCounter;
                for (int i = 0; i <= 6; i++)
                {
                    if (gameBoard.GetBoardState()[i].State == ColorsColumState.Black)
                    {
                        BlackCheckerCounter = BlackCheckerCounter + gameBoard.GetBoardState()[i].Count;
                    }
                }
                if (BlackCheckerCounter == 15)
                {
   
                        foreach (var result in DiceResults)
                        {
                            if (gameBoard.GetBoardState()[column].State == CurrntPlayer.Color && column <= result)
                            {
                                return true;
                            }
                        }

                        return false;
  
                }
            }
            #endregion

            //Check move from prison
            #region
            if (CurrntPlayer.Color == ColorsColumState.White && gameBoard.WhitePrison > 0)
            {
                foreach (var result in DiceResults)
                {
                    if (gameBoard.GetBoardState()[result-1].State == ColorsColumState.White || gameBoard.GetBoardState()[result-1].State == ColorsColumState.Empty || gameBoard.GetBoardState()[result-1].Count == 1)
                    {
                        if (result==column)
                        {
                            return true;
                        }
                    }
                }
                return false;

            }

            if (CurrntPlayer.Color == ColorsColumState.Black && gameBoard.BlackPrison > 0)
            {
                foreach (var result in DiceResults)
    
                {
                    int optionalColumn = result + 16;

                    if (gameBoard.GetBoardState()[optionalColumn].State == ColorsColumState.Black || gameBoard.GetBoardState()[optionalColumn].State == ColorsColumState.Empty || gameBoard.GetBoardState()[optionalColumn].Count == 1)
                    {
                        if (result == column)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            #endregion

            //Sets Direction
            if (CurrntPlayer.Color == ColorsColumState.Black)
            {
                direction = -1;
            }

            //Checks Moves
            #region
            if (column >= 0 && column <= 23)
                {
                    if (CurrntPlayer.Color == gameBoard.GetBoardState()[column].State && gameBoard.GetBoardState()[column].Count > 0)
                    {
                        foreach (int result in DiceResults)
                        {
                            int moves = result * direction;
                            int newColumn = column + moves;
                            #region
                            if (newColumn >= 0 && newColumn <= 23)
                            {
                                if (gameBoard.GetBoardState()[newColumn].State == CurrntPlayer.Color || gameBoard.GetBoardState()[newColumn].State == ColorsColumState.Empty)
                                {
                                    return true;
                                }
                                if (gameBoard.GetBoardState()[newColumn].State != CurrntPlayer.Color && gameBoard.GetBoardState()[newColumn].Count == 1)
                                {
                                    return true;
                                }
                            }
                            #endregion
                        }
                    }
            #endregion   
            }
            return false;

        }

        public bool SetOriginColumn(string userInput)
            {
            int direction = 1;
            if (!IsGameEnded)
            {


                //check if white is at end game state

                #region

                int userInputCheck;
                int checkerCounter;
                if (CurrntPlayer.Color == ColorsColumState.White)
                {
                    checkerCounter = gameBoard.WhiteCounter;
                    for (int i = 17; i < 24; i++)
                    {
                        if (gameBoard.GetBoardState()[i].State == ColorsColumState.White)
                        {
                            checkerCounter = checkerCounter + gameBoard.GetBoardState()[i].Count;
                        }
                    }
                    if (checkerCounter == 15)
                    {
                        if (int.TryParse(userInput, out userInputCheck))
                        {

                            foreach (var result in DiceResults)
                            {
                                if (gameBoard.GetBoardState()[userInputCheck].State == CurrntPlayer.Color &&
                                    userInputCheck >= result)
                                {
                                    DiceResults.Remove(result);
                                    gameBoard.MoveCheckerHome(userInputCheck, ColorsColumState.White);
                                    return true;
                                }
                            }

                        }
                        return false;

                    }
                }

                #endregion

                //check if black is at end game state

                #region

                if (CurrntPlayer.Color == ColorsColumState.Black)
                {
                    checkerCounter = gameBoard.BlackCounter;
                    for (int i = 0; i < 7; i++)
                    {
                        if (gameBoard.GetBoardState()[i].State == ColorsColumState.Black)
                        {
                            checkerCounter = checkerCounter + gameBoard.GetBoardState()[i].Count;
                        }
                    }
                    if (checkerCounter == 15)
                    {
                        if (int.TryParse(userInput, out userInputCheck))
                        {
                            if (gameBoard.GetBoardState()[userInputCheck].Count == 0)
                            {
                                return false;
                            }
                            foreach (var result in DiceResults)
                            {
                                if (gameBoard.GetBoardState()[userInputCheck].State == CurrntPlayer.Color &&
                                    userInputCheck <= result)
                                {
                                    DiceResults.Remove(result);
                                    gameBoard.MoveCheckerHome(userInputCheck, ColorsColumState.Black);
                                    if (gameBoard.GetBoardState()[userInputCheck].Count == 0)
                                    {
                                        return false;
                                    }
                                    return true;
                                }
                            }

                        }
                        return false;
                    }
                }

                #endregion

                //if move from prison origin is not relevant

                #region

                if (CurrntPlayer.Color == ColorsColumState.White && gameBoard.WhitePrison > 0)
                {
                    return true;
                }

                //if move from prison origin is not relevant
                if (CurrntPlayer.Color == ColorsColumState.Black && gameBoard.BlackPrison > 0)
                {
                    return true;
                }

                #endregion

                //Sets Direction
                if (CurrntPlayer.Color == ColorsColumState.Black)
                {
                    direction = -1;
                }

                //Checks Moves
                if (int.TryParse(userInput, out userInputCheck))
                {
                    if (gameBoard.GetBoardState()[userInputCheck].Count > 0 && gameBoard.GetBoardState()[userInputCheck].State==CurrntPlayer.Color)
                    {
                        if (userInputCheck >= 0 && userInputCheck <= 23)
                        {

                            if (CurrntPlayer.Color == gameBoard.GetBoardState()[userInputCheck].State)
                            {
                                foreach (int result in DiceResults)
                                {
                                    int moves = result*direction;
                                    int newColumn = userInputCheck + moves;

                                    #region


                                    if (newColumn >= 0 && newColumn <= 23)
                                    {
                                        if (gameBoard.GetBoardState()[newColumn].State == CurrntPlayer.Color ||
                                            gameBoard.GetBoardState()[newColumn].State == ColorsColumState.Empty)
                                        {
                                            userInputOriginColumn = userInputCheck;

                                            return true;
                                        }
                                        if (gameBoard.GetBoardState()[newColumn].State != CurrntPlayer.Color &&
                                            gameBoard.GetBoardState()[newColumn].Count == 1)
                                        {
                                            userInputOriginColumn = userInputCheck;

                                            return true;
                                        }
                                    }


                                    #endregion
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return false;
            }

        public bool SetTargetColumn(string userInput)
        {
            int direction = 1;

            if (!IsGameEnded)
            {

                //Sets Direction
                if (CurrntPlayer.Color == ColorsColumState.Black)
                {
                    direction = -1;
                }

                //check if white is at end game state

                #region

                int checkerCounter;
                if (CurrntPlayer.Color == ColorsColumState.White)
                {
                    checkerCounter = gameBoard.WhiteCounter;
                    for (int i = 17; i <= 23; i++)
                    {
                        if (gameBoard.GetBoardState()[i].State == ColorsColumState.White)
                        {
                            checkerCounter = checkerCounter + gameBoard.GetBoardState()[i].Count;
                        }
                    }
                    if (checkerCounter == 15)
                    {
                        userInputSelectedColumn = -5;
                        return true;
                    }
                }

                #endregion

                //check if black is at end game state

                #region

                if (CurrntPlayer.Color == ColorsColumState.Black)
                {
                    checkerCounter = gameBoard.BlackCounter;
                    for (int i = 0; i <= 6; i++)
                    {
                        if (gameBoard.GetBoardState()[i].State == ColorsColumState.Black)
                        {
                            checkerCounter = checkerCounter + gameBoard.GetBoardState()[i].Count;
                        }
                    }
                    if (checkerCounter == 15)
                    {
                        userInputSelectedColumn = -5;
                        return true;
                    }
                }

                #endregion


                //checks if white checker in prison

                #region

                int userInputCheck;
                if (CurrntPlayer.Color == ColorsColumState.White && gameBoard.WhitePrison > 0)
                {
                    if (int.TryParse(userInput, out userInputCheck))
                    {
                        if (userInputCheck >= 0 && userInputCheck <= 6)
                        {
                            foreach (var result in DiceResults)
                            {
                                if (userInputCheck == result - 1)
                                {
                                    if (gameBoard.GetBoardState()[userInputCheck].State == ColorsColumState.White ||
                                        gameBoard.GetBoardState()[userInputCheck].State == ColorsColumState.Empty ||
                                        gameBoard.GetBoardState()[userInputCheck].Count == 1)
                                    {
                                        DiceResults.Remove(result);
                                        gameBoard.ReleaseCheckerFromPrison(userInputCheck, ColorsColumState.White);
                                        return true;
                                    }

                                }
                            }


                        }
                    }
                    return false;
                }

                #endregion

                //checks if black checker in prison

                #region

                if (CurrntPlayer.Color == ColorsColumState.Black && gameBoard.BlackPrison > 0)
                {
                    if (int.TryParse(userInput, out userInputCheck))
                    {
                        if (userInputCheck >= 17 && userInputCheck <= 23)
                        {
                            foreach (var result in DiceResults)
                            {
                                if (userInputCheck == result + 16)
                                {
                                    if (gameBoard.GetBoardState()[userInputCheck].State == ColorsColumState.Black ||
                                        gameBoard.GetBoardState()[userInputCheck].State == ColorsColumState.Empty ||
                                        gameBoard.GetBoardState()[userInputCheck].Count == 1)
                                    {
                                        DiceResults.Remove(result);
                                        gameBoard.ReleaseCheckerFromPrison(userInputCheck, ColorsColumState.Black);
                                        return true;
                                    }
                                }

                            }

                        }
                    }
                    return false;
                }

                #endregion


                if (int.TryParse(userInput, out userInputCheck))
                {
                    if (userInputCheck >= 0 && userInputCheck <= 23)
                    {
                        if (CurrntPlayer.Color == gameBoard.GetBoardState()[userInputCheck].State ||
                            gameBoard.GetBoardState()[userInputCheck].State == ColorsColumState.Empty)
                        {
                            if (gameBoard.GetBoardState()[userInputOriginColumn].Count > 0)
                            {
                                foreach (int result in DiceResults)
                                {

                                    if (userInputOriginColumn + (result*direction) == userInputCheck)
                                    {

                                        DiceResults.Remove(result);
                                        userInputSelectedColumn = userInputCheck;
                                        gameBoard.MoveChecker(userInputOriginColumn, userInputSelectedColumn);
                                        return true;
                                    }
                                }
                            }
                        }

                        if (CurrntPlayer.Color != gameBoard.GetBoardState()[userInputCheck].State &&
                                     gameBoard.GetBoardState()[userInputCheck].Count == 1)
                            {
                                foreach (int result in DiceResults)
                                {

                                    if (gameBoard.GetBoardState()[userInputOriginColumn].Count > 0)
                                    {
                                        if (userInputOriginColumn + (result*direction) == userInputCheck)
                                        {
                                            DiceResults.Remove(result);
                                            gameBoard.MoveCheckerToPrison(userInputCheck, userInputOriginColumn);
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                        return false;
                    }
                }
            
            return false;
        }
        

        private void EndGame()
        {
            if (gameBoard.WhiteCounter == 15)
            {
                Winner = playersList[0].Name;
                IsGameEnded = true;
            }
            if (gameBoard.BlackCounter == 15)
            {
                Winner = playersList[1].Name;
                IsGameEnded = true;
            }
            
        }

        public int GetComputerChoice()
        {
            return ComputerPlayer.ComputerChoice();
        }

    }
}


