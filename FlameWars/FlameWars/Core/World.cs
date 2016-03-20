﻿using System;
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

		const int PLAYER_UI_WIDTH = 200;
		const int PLAYER_UI_HEIGHT = 200;
		const int PLAYER_X = 15;
		const int PLAYER_Y = 15;

		SoundManager sm; // TODO
        OptionsManager om; // TODO

		Player player1;
		Player player2;
		Player player3;
		Player player4;
		List<Player> players;
		Board board;

		int windowWidth, windowHeight;

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

			InitializePlayers(players);
		}

		// passes texture to board object
		public void Initialize()
		{
			Texture2D[] pathImages = ArtManager.Paths;
			Texture2D boardImage   = ArtManager.Board;
			windowWidth = GameManager.Width;
			windowHeight = GameManager.Height;

			board = new Board(pathImages, boardImage);
		}

		// This method initializes the players
		public void InitializePlayers(int players)
		{
			// Initialize the players
			// There will always be at least two players
			this.players = new List<Player>();
			player1      = new Player();
			player2      = new Player();

			// Four players
			if (players > 3)
			{
				player3 = new Player();
				player4 = new Player();
			}
			// Three Players
			else if (players > 2)
			{
				player3 = new Player();
			}
		}

		// Loads texture content
		public void LoadContent()
		{
			// Load player texture data
			if (player1 != null) player1.Icon = ArtManager.PlayerIcon1; this.players.Add(player1);
			if (player2 != null) player2.Icon = ArtManager.PlayerIcon2; this.players.Add(player2);
			if (player3 != null) player3.Icon = ArtManager.PlayerIcon3; this.players.Add(player3);
			if (player4 != null) player4.Icon = ArtManager.PlayerIcon4; this.players.Add(player4);
		}

		public void Update(GameTime gameTime)
		{
			// Should cycle through each of the players.


		}

		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the board
			board.Draw(spriteBatch);

			// Draw the player UI and Token
			int index = 0;
			foreach (Player p in players)
			{
				// Draw the player token
				p.DrawToken(spriteBatch);

				// Switch to draw in each corner
				switch(index)
				{
					case 0:
						p.DrawUI(PLAYER_X, PLAYER_Y, spriteBatch);
						break;
					case 1:
						p.DrawUI(GameManager.Width-PLAYER_UI_WIDTH-PLAYER_X, PLAYER_Y, spriteBatch);
						break;
					case 2:
						p.DrawUI(PLAYER_X, GameManager.Height-PLAYER_UI_HEIGHT-PLAYER_Y, spriteBatch);
						break;
					case 3:
						p.DrawUI(GameManager.Width-PLAYER_UI_WIDTH-PLAYER_X, GameManager.Height-PLAYER_UI_HEIGHT-PLAYER_Y, spriteBatch);
						break;
				}

				index++;
			}
		}
    }
}
