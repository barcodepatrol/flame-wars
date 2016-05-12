using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlameWars
{
    internal static class GameManager
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		// These two ints store the height and width of the board.
		// Stores the window's center.
		// Save the game's graphics device manager.
		private static int windowWidth, windowHeight;
		private static Vector2 windowCenter;
		private static GraphicsDeviceManager graphicsManager;
		private static double screenScale;

		// Saves the deck info
		private static Deck mainDeck;
		private static Deck premiumDeck;
		private static int mainDeckIndex = 0;
		private static int premiumDeckIndex = 0;

		// Saves the current player info
		private static Player cPlayer = null;
		private static int numberOfPlayers = 2;

		// A boolean to determine if a player ended their turn
		private static bool endTurn = false;

		public static Random random;

		private static int totalBandwidth = 100;

		#region Properties
		//* Properties *//
		// Stores the window width
		public static int Width
		{
			get { return windowWidth; }
			set { windowWidth = value; }
		}
		// Stores the window height
		public static int Height
		{
			get { return windowHeight; }
			set { windowHeight = value; }
		}
		// Stores the current player info
		public static Player CurrentPlayer
		{
			get { return cPlayer; }
			set { cPlayer = value; }
		}
		// Stores the amount of players in the game
		public static int NumberOfPlayers
		{
			get { return numberOfPlayers; }
			set { numberOfPlayers = value; }
		}
		// Stores the window center position.
		public static Vector2 Center
		{
			get { return windowCenter; }
			set { windowCenter = value; }
		}
		// Retrieves the game's graphics device
		public static GraphicsDevice Graphics
		{
			get {return graphicsManager.GraphicsDevice; }
		}
		// Stores whether or not a player has ended their turn
		public static bool EndTurn
		{
			get { return endTurn; }
			set { endTurn = value; }
		}

		public static int TotalBandwidth
		{
			get { return totalBandwidth; }
			set { totalBandwidth = value; }
		}

		public static Random RandomGen
		{
			get { return random; }
		}

		public static double ScreenScale
		{
			get { return screenScale; }
		}
		#endregion Properties

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// This method initializes the class
		public static void Init(GraphicsDeviceManager gd, int w, int h)
		{
			graphicsManager = gd;
			windowWidth     = w;
			windowHeight    = h;
			windowCenter    = GetElementCenter(w, h);
			screenScale		= h/1080.0f;
			mainDeck        = new Deck("Content\\MainDeck.xml");
			premiumDeck     = new Deck("Content\\PremiumDeck.xml");
			random			= new Random();

			// Set up Premium cards
			premiumDeck.SetPremium();

			// ============================================
			// DISABLED FOR TESTING NECESSARY FOR REAL GAME

			// Shuffle decks
			//mainDeck.Shuffle();
			//premiumDeck.Shuffle();
		}

		// Service method.
		// Call these for quick calculations in other methods.

		// GetElementCenter() returns a center Vector2 value,
		// with no regards to the elements' (X,Y) position.
		// This treats it as if it is in the origin of (0,0).
		// In other words this method is asking for the element's origin.
		public static Vector2 GetElementCenter(int w, int h)
		{
			int xOrigin = (w / 2);
			int yOrigin = (h / 2);
			
			return new Vector2(xOrigin, yOrigin);
		}

		// GetElementCenterPoint() returns a center Vector2 value,
		// with regards to the elements' (X,Y) position.
		// The returned value can be used, say, to draw to a particular point.
		public static Vector2 GetElementCenterPoint(int x, int y, int w, int h)
		{
			int xOrigin = x + (w / 2);
			int yOrigin = y - (h / 2);

			return new Vector2(xOrigin, yOrigin);
		}

		// GetCard() returns the next card in the deck
		public static Card GetCard()
		{
			// Save card and then increment index before returning
			Card c = mainDeck.Cards[mainDeckIndex];
			mainDeckIndex++;

			// If we got to the last card reshuffle the deck
			if (mainDeckIndex == mainDeck.Cards.Count-1)
			{ 
				mainDeck.Shuffle();
				mainDeckIndex = 0;
			}

			return c;
		}

		// GetPremiumCard() returns the next card in the premium deck
		public static Card GetPremiumCard()
		{
			// Save card and then increment index before returning
			Card c = premiumDeck.Cards[premiumDeckIndex];
			premiumDeckIndex++;

			// If we got to the last card reshuffle the deck
			if (premiumDeckIndex == premiumDeck.Cards.Count-1)
			{ 
				premiumDeck.Shuffle();
				premiumDeckIndex = 0;
			}

			return c;
		}
    }
}