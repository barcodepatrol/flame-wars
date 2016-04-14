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

namespace FlameWars
{
	class Deck
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================
		XmlDocument xml;
		List<Card> cards;
		Random rnd;

		public List<Card> Cards
		{
			get { return cards; }
			set { cards = value; }
		}

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Deck(string file)
		{
			try
			{
				// Create a new active cards list 
				// and discarded cards list
				// and a new xml document
				// and a random number generator
				cards = new List<Card>();
				xml   = new XmlDocument();
				rnd   = new Random();

				// Load Xml Data
				xml.Load(file);
			
				// Load all of the cards
				LoadCards();

				// If this is a premium deck, mark cards
				if (file.Contains("Premium"))
					SetPremium();
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
			Card c = new Card(xn.SelectSingleNode("./name").FirstChild.Value,
							  xn.SelectSingleNode("./description").FirstChild.Value,
							  xn.SelectSingleNode("./target").FirstChild.Value,
							  xn.SelectSingleNode("./attribute").FirstChild.Value,
							  xn.SelectSingleNode("./amount").FirstChild.Value);

			cards.Add(c);
		}

		// This method sets every card in the deck to be a Premium card
		public void SetPremium()
		{
			// Iterate through all cards
			foreach (Card c in cards)
				c.Premium = true;
		}

		// This method reshuffles the list of cards
		public void Shuffle()
		{
			// Iterate through the entire list
			for (int i = 0; i < cards.Count; i++)
			{
				// Selected a random index
				int r = i + (int)(rnd.NextDouble() * (cards.Count- i));

				// Swap current location in deck with random index
				Card temp = cards[r];
				cards[r]  = cards[i];
				cards[i]  = temp;
			}
		}
	}
}
