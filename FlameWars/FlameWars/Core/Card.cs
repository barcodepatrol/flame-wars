using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Name: Dante Nardo
// Date: 3/8/2016
// Purpose: To create and store card xml data for the game to see

namespace FlameWars
{
	public class Card
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================
		string name = "Default"; // Name.
		string desc = "Default"; // Description.
		string targ = "No Target"; // Target.
		string atrb = "Users"; // Users.
		int amount  = 0; // Value to change the attribute by.
		int malice  = 0; // Value to add to malice
		int charity = 0; // Value to add to charity

		#region Properties
		public string Name
		{
			get {return name; }
			set {this.name = value; }
		}
		public string Description
		{
			get {return desc; }
			set {this.desc = value; }
		}
		public string Target
		{
			get {return targ; }
			set {this.targ = value; }
		}
		public string Attribute
		{
			get {return atrb; }
			set {this.atrb = value; }
		}
		public int Amount
		{
			get {return amount; }
			set {this.amount = value; }
		}
		public int Malice
		{
			get {return malice; }
			set {this.malice = value; }
		}
		public int Charity
		{
			get {return charity; }
			set {this.charity = value; }
		}
		#endregion Properties

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
			
			// Determine malice/charity

			// MALICE: Negative impact on others
			if (targ == "Target Others" && amount < 0)
				malice = amount;
			// CHARITY: Positive impact on others
			if (targ == "Target Others" && amount > 0)
				charity = amount;
		}
	}
}
