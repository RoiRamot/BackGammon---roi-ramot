using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backgammon
{
    public class Dice
    {
        private int lowValue;
        private int heighValue;

        public Dice(int low, int heigh)
        {
            lowValue = low;
            heighValue = heigh;
        }

        public int RollDice()
        {
            Random result = new Random();
            Thread.Sleep(20);
            return result.Next(lowValue, heighValue);
        }
    }
}
    