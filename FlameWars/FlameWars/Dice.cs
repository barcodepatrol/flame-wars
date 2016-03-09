using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlameWars
{
    class Dice
    {
        public int Roll(int dNum)
        {
            int val = 0;
            int dice = 0;
            Random rgen = new Random();
            do
            {
                val += rgen.Next(1, 7);
                dice++;
            } while (dice < dNum);

            return val;
        }
    }
}
