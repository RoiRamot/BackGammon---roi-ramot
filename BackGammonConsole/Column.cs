using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Column
    {
        public enum ColorsColumState
        {
            White,
            Black,
            Empty
        }
        public Column()
        {
            Count = 0;
            State = ColorsColumState.Empty;
        }
        public int Count { get; set; }
        public ColorsColumState State { get; set; }
    }

}
