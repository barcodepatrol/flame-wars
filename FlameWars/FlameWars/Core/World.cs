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

		const int PLAYER_UI_WIDTH = 200;
		const int PLAYER_UI_HEIGHT = 200;
		const int px = 15;
		const int py = 15;

		SoundManager sm;
        OptionsManager om;

		Player player1;
		Player player2;
		Player player3;
		Player player4;
		List<Player> players;
		Board board;

		int winW, winH;

		#endregion Variables

		#region Properties
		//* Properties *//
		// Stores the first player object
		public Player P1
		{
			get { return this.player1; }
			set { this.player1 = value; }
		}
		// Stores the first player object
		public Player P2
		{
			get { return this.player2; }
			set { this.player2 = value; }
		}
		// Stores the first player object
		public Player P3
		{
			get { return this.player3; }
			set { this.player3 = value; }
		}
		// Stores the first player object
		public Player P4
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

		public void Draw(SpriteBatch sb)
		{
			// Draw the board
			board.Draw(sb);

			// Draw the player UI and Token
			int index = 0;
			foreach (Player p in players)
			{
				// Draw the player token
				p.DrawToken(sb);

				// Switch to draw in each corner
				switch(index)
				{
					case 0:
						p.DrawUI(px, py, sb);
						break;
					case 1:
						p.DrawUI(GameManager.winW-PLAYER_UI_WIDTH-px, py, sb);
						break;
					case 2:
						p.DrawUI(px, GameManager.winH-PLAYER_UI_HEIGHT-py, sb);
						break;
					case 3:
						p.DrawUI(GameManager.winW-PLAYER_UI_WIDTH-px, GameManager.winH-PLAYER_UI_HEIGHT-py, sb);
						break;
				}

				index++;
			}
		}
    }
}
