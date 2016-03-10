using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

// Name: Dante Nardo
// Date: 3/8/2016
// Purpose: To create and store card xml data for the game to see

namespace FlameWars_DeckLoading
{
	class Deck
	{
		// ========================================
		// =============== Variables ==============
		// ========================================
		XmlDocument xml;
		List<Card> cards;
		List<Card> discard;

		// ========================================
		// ================ Methods ===============
		// ========================================

		// Constructor
		public Deck(string file)
		{
			try
			{
				// Create a new active cards list 
				// and discarded cards list
				// and a new xml document
				cards   = new List<Card>();
				discard = new List<Card>();
				xml     = new XmlDocument();

				// Load Xml Data
				xml.Load(file);
			
				// Load all of the cards
				LoadCards();
			}
			catch (FileNotFoundException)
			{
				Console.WriteLine("File not found");
			}
			catch (XmlException)
			{
				Console.WriteLine("XML exception");
			}
		}

		// Loads card data and saves it in card objects
		public void LoadCards()
		{
			// Loop through every single card in the xml
			foreach (XmlNode xn in xml.SelectNodes("./deck/card"))
			{
				CreateCard(xn);
			}
		}

		// This method generates and saves a card object
		// It has an xmlnode parameter and saves the card in the list
		public void CreateCard(XmlNode xn)
		{
			Card c = new Card(xn.SelectSingleNode("./main/name").FirstChild.Value,
							  xn.SelectSingleNode("./main/description").FirstChild.Value,
							  xn.SelectSingleNode("./main/target").FirstChild.Value,
							  xn.SelectSingleNode("./main/attribute").FirstChild.Value,
							  xn.SelectSingleNode("./main/amount").FirstChild.Value);

			cards.Add(c);
		}
	}
}
