using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameWars
{
	class Bond
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================
		int cost = 0;
		int turn = 0;

		// Properties
		public int Cost
		{
			get { return cost; }
			set { cost = value; }
		}
		public int Turn
		{
			get { return turn; }
			set { turn = value; }
		}

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Bond()
		{
			// Randomly generate cost
			cost = 100;
		}

		// This method determines how much money the user makes
		public int GenerateRevenue()
		{
			return (cost + (cost/20));
		}
	}
}
