

using Backgammon;

namespace BackGammonLogic
{
    
    class Board
    {
        Column[] BoardArry { get; set; }
        public int BlackCounter { get; private set; }
        public int WhiteCounter { get; private set; }
        public int BlackPrison { get; private set; }
        public int WhitePrison { get; private set; }
        public void SetBoard()
        {
            BoardArry=new Column[24];
            for (int i = 0; i <= 23; i++)
            {
                BoardArry[i]=new Column();
            }
            BoardArry[0].State= ColorsColumState.White;
            BoardArry[4].State= ColorsColumState.Black;
            BoardArry[6].State= ColorsColumState.Black;
            BoardArry[11].State= ColorsColumState.White;
            BoardArry[12].State= ColorsColumState.Black;
            BoardArry[16].State= ColorsColumState.White;
            BoardArry[18].State= ColorsColumState.White;
            BoardArry[23].State= ColorsColumState.Black;

            BoardArry[0].Count = 5;//white
            BoardArry[4].Count = 3;//black
            BoardArry[6].Count = 5;//black
            BoardArry[11].Count =2;//white
            BoardArry[12].Count = 5;//black
            BoardArry[16].Count = 3;//white
            BoardArry[18].Count = 5;//white
            BoardArry[23].Count = 2;//black
            //total = 15 white, 15 black

        }
        public void MoveChecker(int originalColumn, int newColumn)
        {

            BoardArry[originalColumn].Count--;
            BoardArry[newColumn].Count++;

            if (BoardArry[originalColumn].State==ColorsColumState.White)
            {
               BoardArry[newColumn].State = ColorsColumState.White;
            }
            if (BoardArry[originalColumn].State==ColorsColumState.Black)
            {
                BoardArry[newColumn].State = ColorsColumState.Black;
            }
            
            if (BoardArry[originalColumn].Count==0)
            {
                BoardArry[originalColumn].State= ColorsColumState.Empty;
            }

        }
        public void MoveCheckerToPrison(int targetColumn,int originColumn)
        {
            
            if (BoardArry[targetColumn].State == ColorsColumState.Black)
            {
                BoardArry[targetColumn].State = ColorsColumState.White;
                BlackPrison++;
                BoardArry[targetColumn].Count = 1;
                BoardArry[originColumn].Count--;
            }
            else if (BoardArry[targetColumn].State == ColorsColumState.White)
            {
                BoardArry[targetColumn].State = ColorsColumState.Black;
                WhitePrison++;
                BoardArry[targetColumn].Count = 1;
                BoardArry[originColumn].Count--;
            }



            if (BoardArry[originColumn].Count==0)
            {
              BoardArry[originColumn].State= ColorsColumState.Empty;  
            }

        }
        public void ReleaseCheckerFromPrison(int selectedColumn, ColorsColumState color)
        {
            if (color == ColorsColumState.White)
            {
                if (BoardArry[selectedColumn].State == ColorsColumState.Black)
                {
                    BlackPrison++;
                    BoardArry[selectedColumn].Count=0;
                }
                BoardArry[selectedColumn].State = ColorsColumState.White;
                WhitePrison--;
                BoardArry[selectedColumn].Count++;
            }
            if (color == ColorsColumState.Black)
            {
                if (BoardArry[selectedColumn].State == ColorsColumState.White)
                {
                    WhitePrison++;
                    BoardArry[selectedColumn].Count=0;
                }
                BoardArry[selectedColumn].State = ColorsColumState.Black;
                BlackPrison--;
                BoardArry[selectedColumn].Count++;
            }

        }
        public void MoveCheckerHome(int selectedColumn, ColorsColumState color)
        {
            BoardArry[selectedColumn].Count--;
            if (color == ColorsColumState.White)
            {
                WhiteCounter++;
            }
            else
            {
                BlackCounter++;
            }
            if (BoardArry[selectedColumn].Count == 0)
            {
                BoardArry[selectedColumn].State = ColorsColumState.Empty;
            }

        }
        public Column[] GetBoardState()
        {
            return BoardArry;
        }
    }
}
