using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlameWars
{
    public static class Dice
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		// Create a static random generator object
        public static Random rgen = new Random();

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Rolls dice
		// The paramater determines the number of times that you roll a d6
        public static int Roll(int dNum)
        {
            int val = 0;
            int dice = 0;

			// Roll dice until we have rolled the given amount of times
			// Rolls at least once
            do
            {
                val += rgen.Next(1, 7);
                dice++;
            } while (dice < dNum);

			// Returns the total dice value
            return val;
        }
    }
}
