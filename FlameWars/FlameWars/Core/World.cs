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
		SoundManager sm;
        OptionsManager om;
        ArtManager am;
        GameManager gm;

		Player player1;
		Player player2;
		Player player3;
		Player player4;
		List<Player> players;
		Board brd;
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
			am = new ArtManager();
			gm = new GameManager();

			InitializePlayers(players);
		}

		// passes texture to board object
		public void InitializeBoard(Texture2D bImg, Texture2D backimg)
		{
			brd = new Board(bImg, backimg);
		}

		// This method initializes the players
		public void InitializePlayers(int players)
		{
			// Initialize the players
			// There will always be at least two players
			this.players = new List<Player>();
			player1      = new Player();
			player2      = new Player();

			// Add players to list
			this.players.Add(player1);
			this.players.Add(player2);

			// Four players
			if (players > 3)
			{
				player3 = new Player();
				player4 = new Player();
				this.players.Add(player3);
				this.players.Add(player4);
			}
			// Three Players
			else if (players > 2)
			{
				player3 = new Player();
				this.players.Add(player3);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			brd.Draw(spriteBatch);
		}
    }
}
