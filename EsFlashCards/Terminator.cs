using System;
using System.Collections.Generic;
using System.Text;

namespace EsFlashCards
{
    public class Terminator
    {
        private int _round;
        public bool DoTheyWantToStop()
        {
            if (_round++ % 10 != 0)
                return false;

            Console.Write("Want to stop? (Y/N):");
            var isStop = Console.ReadLine();
            return isStop.ToLower() == "y";
        }
    }
}
