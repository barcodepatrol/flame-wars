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

		// Saves player info
		private static int[] playerRoles;
		private static int numberOfPlayers = 2;

		// Saves the current player info
		private static Player cPlayer = null;
		private static int cTurnNumber = 0;

		// Saves the winning player info.
		private static Player wPlayer = null;
		private static string wPlayerRole = null;
		private static int[] wPlayerResources = null;
		private static int endGameTurnNumber = 0;

		// A boolean to determine if a player ended their turn
		private static bool endTurn = false;

		// A boolean to determine if the game has ended.
		private static bool endGame = false;

		// A boolean to determine whether or not to reset the game.
		private static bool resetGame = false;

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

		// Stores the roles for each player
		public static int[] PlayerRoles
		{
			get { return playerRoles; }
		}

		// Stores the amount of players in the game
		public static int NumberOfPlayers
		{
			get { return numberOfPlayers; }
			set { numberOfPlayers = value; }
		}

		// Stores the current turn number.
		public static int CurrentTurnNumber
		{
			get { return cTurnNumber; }
			set { cTurnNumber = value; }
		}

		// Stores the winning player info.
		public static Player WinningPlayer
		{
			get { return wPlayer; }
			set { wPlayer = value; }
		}

		// Stores an integer array of the winning player's resources at win-game time.
		public static int[] WinningPlayerResources
		{
			get { return wPlayerResources; }
			set { wPlayerResources = value; }
		}

		// Stores the winning player's role.
		public static string WinningPlayerRole
		{
			get { return wPlayerRole; }
			set { wPlayerRole = value; }
		}

		// Stores the turn number for the winning role.
		public static int WinTurn
		{
			get { return endGameTurnNumber; }
			set { endGameTurnNumber = value; }
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
			set
			{
				if (!EndGame)
				{
					endTurn = value;
				}
				else
				{
					endTurn = false;
				}
			}
		}

		// Stores whether or not a player has ended the game.
		public static bool EndGame
		{
			get { return endGame; }
			set { endGame = value; }
		}

		// Stores whether or not the game board needs to be reset
		public static bool ResetGame
		{
			get { return resetGame; }
			set { resetGame = value; }
		}

		// Stores the total accumlated bandwidth.
		public static int TotalBandwidth
		{
			get { return totalBandwidth; }
			set { totalBandwidth = value; }
		}

		// Stores the random for random number generation.
		public static Random RandomGen
		{
			get { return random; }
		}

		// Stores the scale of the screen.
		public static double ScreenScale
		{
			get { return screenScale; }
		}

		// Winning Information.
		public static string WinInformation
		{
			get { return "Placeholder Text."; }
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

			// Set up roles - shuffle them
			playerRoles = new int[4] { 0, 1, 2, 3 };
			Shuffle(ref playerRoles);

			// Set up Premium cards
			premiumDeck.SetPremium();

			// Shuffle decks
			mainDeck.Shuffle();
			premiumDeck.Shuffle();
		}

		// Reset the values for GameManager in event of a restart.
		public static void Reset()
		{
			Init(graphicsManager, windowWidth, windowHeight);

			mainDeckIndex = 0;
			premiumDeckIndex = 0;

			// Saves the current player info
			cPlayer = null;
			numberOfPlayers = 2;
			cTurnNumber = 0;

			// Saves the winning player info.
			wPlayer = null;
			wPlayerRole = null;
			wPlayerResources = null;
			endGameTurnNumber = 0;

			// A boolean to determine if a player ended their turn
			endTurn = false;

			// A boolean to determine if the game has ended.
			endGame = false;

			// A boolean to determine whether or not to reset the game.
			resetGame = false;

			totalBandwidth = 100;

			Game1.CURRENT_GAME.Reset();
			StateManager.gameState = StateManager.GameState.Start;
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

		// This method shuffles an array
		public static void Shuffle(ref int[] array)
		{
			// Iterate through the entire list
			for (int i = 0; i < array.Length; i++)
			{
				// Selected a random index
				int r = i + (int)(RandomGen.NextDouble() * (array.Length- i));

				// Swap current location in deck with random index
				int temp = array[r];
				array[r]  = array[i];
				array[i]  = temp;
			}
		}

		public static void Reset()
		{

		}
    }
}