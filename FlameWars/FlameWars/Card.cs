using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Name: Dante Nardo
// Date: 3/8/2016
// Purpose: To create and store card xml data for the game to see

namespace FlameWars_DeckLoading
{
	class Card
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================
		string name = "Default";
		string desc = "Default";
		string targ = "No Target";
		string atrb = "Users";
		int amount  = 0;

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		// Given all of the data for each card
		public Card(string n, string d, string t, string at, string am)
		{
			name = n;
			desc = d;
			targ = t;
			atrb = at;
			int.TryParse(am, out amount);
		}
	}
}
