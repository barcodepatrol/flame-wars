using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
    class Player
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		public enum Role
		{
			TopHat,
			Plastic,
			Narcissist,
			Befriender,
			Dankest,
			Sprinter
		}

		private Texture2D playerIcon;
		private Texture2D playerPiece;
		private Rectangle playerPos;
		private Random rng;

		private Role role;
		private int boardPos   = 0;
		private int money      = 0;
		private int users      = 0;
		private int memes      = 0;
		private int bandwidthA = 0;
		private int bandwidthP = 100;
		private int malice     = 0;
		private int charity    = 0;

		#endregion Variables

		#region Properties
		//* Properties *//
		// Stores the 2D texture for the player's icon
		public Texture2D pIcon
		{
			get { return this.playerIcon; }
			set { this.playerIcon = value; }
		}
		// Stores the 2D texture for the player's board piece
		public Texture2D pPiece
		{
			get { return this.playerPiece; }
			set { this.playerPiece = value; }
		}
		// Stores the rectangle of the player's position on the screen
		public Rectangle pPos
		{
			get { return this.playerPos; }
			set { this.playerPos = value; }
		}
		// Stores the int value that evaluates to board position
		public Role pRole
		{
			get { return this.role; }
			set { this.role = value; }
		}
		// Stores the int value that evaluates to board position
		public int bPos
		{
			get { return this.boardPos; }
			set { this.boardPos = value; }
		}
		// Stores the int value for the player's money
		public int Money
		{
			get { return this.money; }
			set { this.money = value; }
		}
		// Stores the int value for the player's users
		public int Users
		{
			get { return this.users; }
			set
			{
				if (this.users - value < 0) this.users = 0;
				else this.users = value;
			}
		}
		// Stores the int value for the player's memes
		public int Memes
		{
			get { return this.memes; }
			set { this.memes = value; }
		}
		// Stores the int value for the player's bandwidth amount
		public int BandwidthA
		{
			get { return this.bandwidthA; }
			set { this.bandwidthA = value; }
		}
		// Stores the int value for the player's bandwidth percentage
		public int BandwidthP
		{
			get { return this.bandwidthP; }
			set { this.bandwidthP = value; }
		}
		// Stores the int value for the player's malice
		public int Malice
		{
			get { return this.malice; }
			set { this.malice = value; }
		}
		// Stores the int value for the player's charity
		public int Charity
		{
			get { return this.charity; }
			set { this.charity = value; }
		}
		#endregion

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Player()
		{
			playerPos = new Rectangle();
			rng       = new Random();
		}

		// Determines how many users the player gets
		public void GenerateUsers()
		{
			// Memes increase the amount of users you get each round
			// Bandwidth determines the upper and lower bounds of how many users you gain/lose

			// Memes increase your users by an exponential addition
			int memeAddicts = (memes^2)/20;

			// The left bound is determined by how far below 100 your bandwidth is
			// The right bound is positive as long as the bandwidth is above 60%
			int left  =  5 + users * (100-bandwidthP);
			int right = 10 + users * (bandwidthP-60);

			// Select the user's new user amount to add
			// Add the new users to the player's users
			users += rng.Next(left, right);
		}
	}
}
