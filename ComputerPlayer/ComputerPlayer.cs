using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComputerPlayer
{
    public class AIPlayer
    {
        private int choice;
        public int ComputerChoice()
        {
            var compChoice = new Random();
            choice = compChoice.Next(0, 24);
            Thread.Sleep(5);
            return choice;
        }
    }
}
