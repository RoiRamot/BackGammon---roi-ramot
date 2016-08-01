

using Backgammon;

namespace BackGammonLogic
{
    public class Player
    {
       public string Name { get; private set; }
        public bool IsComputer { get; private set; }
        public ColorsColumState Color { get; private set; }

        public Player(string n, ColorsColumState color, bool iscomp)
        {
            Name = n;
            Color = color;
            IsComputer = iscomp;
        }

        public Player(string n,ColorsColumState color)
        {
            Color = color;
            Name = n;
            IsComputer = false;
        }       
    }
}
