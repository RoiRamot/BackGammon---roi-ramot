
using Backgammon;

namespace BackGammonLogic
{
    public class Column
    {
        public int Count { get; set; }
        public ColorsColumState State { get; set; }
        public Column()
        {
            Count = 0;
            State = ColorsColumState.Empty;
        }
    }

}
