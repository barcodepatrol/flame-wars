using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
    class World
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables
		
		const int PLAYER_UI_WIDTH  = 200;
		const int PLAYER_UI_HEIGHT = 200;
		const int BUTTON_HEIGHT    = 100;
		const int BUTTON_WIDTH     = 150;
		const int PLAYER_X         = 15;
		const int PLAYER_Y         = 15;
	
		readonly Vector2 TOP_LEFT_POSITION = new Vector2(PLAYER_X, PLAYER_Y);
		readonly Vector2 TOP_RIGHT_POSITION = new Vector2(GameManager.Width - PLAYER_UI_WIDTH - PLAYER_X, PLAYER_Y);
		readonly Vector2 BOTTOM_LEFT_POSITION = new Vector2(PLAYER_X, GameManager.Height - PLAYER_UI_HEIGHT - PLAYER_Y);
		readonly Vector2 BOTTOM_RIGHT_POSIITION = new Vector2(GameManager.Width - PLAYER_UI_WIDTH - PLAYER_X, GameManager.Height - PLAYER_UI_HEIGHT - PLAYER_Y);
		
		SoundManager sm; // TODO
        OptionsManager om; // TODO

		Color[] buttonColors;
		Texture2D buttonTexture; // Roll button texture.
		Player player1;
		Player player2;
		Player player3;
		Player player4;
		List<Player> players;
		Board board;

		// state management variables.
		int mX, mY; // Mouse state variables.
		bool mousePress, prevPress; // Mouse pressed.
		int windowWidth, windowHeight; // Height and width of screen.

		#endregion Variables

		#region Properties
		//* Properties *//

		// Stores the first player object
		public Player PlayerOne
		{
			get { return this.player1; }
			set { this.player1 = value; }
		}

		// Stores the first player object
		public Player PlayerTwo
		{
			get { return this.player2; }
			set { this.player2 = value; }
		}

		// Stores the first player object
		public Player PlayerThree
		{
			get { return this.player3; }
			set { this.player3 = value; }
		}

		// Stores the first player object
		public Player PlayerFour
		{
			get { return this.player4; }
			set { this.player4 = value; }
		}		
		#endregion Properties

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public World(int players)
		{
			// Initialize the managers
			sm = new SoundManager(0,0);
			om = new OptionsManager();

			// Initialize the players.
			InitializePlayers(players);
		}

		// passes texture to board object
		public void Initialize()
		{
			Texture2D[] pathImages = ArtManager.Paths;
			Texture2D boardImage   = ArtManager.Board;

			windowWidth = GameManager.Width;
			windowHeight = GameManager.Height;
			
			buttonColors = new Color[]{ Color.DarkGray, Color.White }; // 0 for inactive, 1 for active.
			buttonTexture = ArtManager.RollButton;

			MakeButtons();

			board = new Board(pathImages, boardImage);
		}

		// This method initializes the players
		public void InitializePlayers(int players)
		{
			// Initialize the players
			// There will always be at least two players
			this.players = new List<Player>();
			player1      = new Player(TOP_LEFT_POSITION);
			player2      = new Player(TOP_RIGHT_POSITION);

			// Four players
			if (players > 3)
			{
				player3 = new Player(BOTTOM_LEFT_POSITION);
				player4 = new Player(BOTTOM_RIGHT_POSIITION);
			}
			// Three Players
			else if (players > 2)
			{
				player3 = new Player(BOTTOM_LEFT_POSITION);
			}
		}

		// Ask each player to make their own button.
		public void MakeButtons()
		{
			foreach (Player player in players)
			{
				// Create a button of a certain width and height.
				// Add it to the player's UI.
				player.MakeButton(buttonTexture, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}

		// Ask each player to determine the value of the button.
		public void Hover()
		{
			foreach (Player player in players)
			{
				player.Hover(mX, mY);
			}
		}

		public void Pressed()
		{
			foreach (Player player in players)
			{
				player.Pressed(mX, mY);
			}
		}

		public void Released()
		{
			foreach (Player player in players)
			{
				player.Released(mX, mY);
			}
		}

		// Loads texture content
		public void LoadContent()
		{
			// Load player texture data
			if (player1 != null) { player1.Icon = ArtManager.PlayerIcon1; this.players.Add(player1); }
			if (player2 != null) { player2.Icon = ArtManager.PlayerIcon2; this.players.Add(player2); }
			if (player3 != null) { player3.Icon = ArtManager.PlayerIcon3; this.players.Add(player3); }
			if (player4 != null) { player4.Icon = ArtManager.PlayerIcon4; this.players.Add(player4); }
		}

		public void Update(GameTime gameTime, int mx, int my)
		{
			Update(gameTime);
			Update(mx, my);
		}

		public void Update(GameTime gameTime)
		{
			// Should cycle through each of the players.
			// Get the player button released function.

			foreach(Player p in players)
			{
				// If buttonReleased is true.
				// And the player is in Roll mode.
				// Call roll die.


			}

		}

		// Passes in a few variables to save for update functions
		public void Update(int mx, int my)
		{
			this.mX = mx;
			this.mY = my;
		}


		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the board
			board.Draw(spriteBatch);

			// Draw the player UI and Token
			foreach (Player p in players)
			{
				// Draw the player token
				p.DrawToken(spriteBatch);

				// Draw the player UI.
				p.DrawUI(spriteBatch);

				//* DEVELOPERS NOTE *//
				/*
					Originally, p.DrawUI() was in a switch structure
					that desiginated each call to a draw depending on its corner.
				
					By passing the Vector2 on construction to each player,
					we gain quicker access to the position values, 
					and can draw buttons for each player.				
				*/
			}
		}
    }
}
