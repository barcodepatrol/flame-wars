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

		// Enumerator.
		public enum Role
		{
			TopHat,
			Plastic,
			Narcissist,
			Befriender,
			Dankest,
			Sprinter
		}

		// Textures.
		private Texture2D iconTexture;  // UI Icon for the player.
		private Rectangle iconBounds;   // Display position for the player's UI icon.
		private Texture2D tokenTexture; // Actual texture for the player's token.
		private Rectangle tokenBounds;  // Display position for the player's token.
		private Random random;

		private Role role; // The role of the player.
		private int boardPosition       = 0;   // The position the player has on the board.
		private int money               = 0;   // The capital a given player has.
		private int users               = 0;   // The number of users the player has.
		private int memes               = 0;   // The number of memes the player can use.
		private int bandwidthAmount     = 0;   // The bandwidth amount the player owns.
		private int bandwidthPercentage = 100; // The percentage of bandwidth the player can utilize.
		private int malice              = 0;   // The malice amount the player has accrued.
		private int charity             = 0;   // The charity amount the player has accrued.

		#endregion Variables

		#region Properties

		// Stores the 2D texture for the player's icon
		public Texture2D Icon
		{
			get { return this.iconTexture; }
			set { this.iconTexture = value; }
		}

		// Stores the 2D texture for the player's board piece
		public Texture2D Token
		{
			get { return this.tokenTexture; }
			set { this.tokenTexture = value; }
		}

		// Stores the rectangle of the player's position on the screen
		public Rectangle Bounds
		{
			get { return this.tokenBounds; }
			set { this.tokenBounds = value; }
		}

		// Stores the int value that evaluates to board position
		public Role PlayerRole
		{
			get { return this.role; }
			set { this.role = value; }
		}

		// Stores the int value that evaluates to board position
		public int BoardPosition
		{
			get { return this.boardPosition; }
			set { this.boardPosition = value; }
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
		public int Bandwidth
		{
			get { return this.bandwidthAmount; }
			set { this.bandwidthAmount = value; }
		}

		// Stores the int value for the player's bandwidth percentage
		public int BandwidthPercentage
		{
			get { return this.bandwidthPercentage; }
			set { this.bandwidthPercentage = value; }
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
			tokenBounds = new Rectangle();
			random       = new Random();
			Initialize();
		}

		// Initialize the Player.
		public void Initialize()
		{
			// Set the initial path index to zero.

		}

		// Determines how many users the player gets
		public void GenerateUsers()
		{
			// Memes increase the amount of users you get each round
			// Bandwidth determines the upper and lower bounds of how many users you gain/lose

			// Memes increase your users by an exponential addition
			int memeAddicts = (memes^2)/20;

			// The left bound is positive as long as the bandwidth is above 80%
			// The right bound is positive as long as the bandwidth is above 60%
			int left  =  5 + users * (bandwidthPercentage-80);
			int right = 10 + users * (bandwidthPercentage-60);

			// Select the user's new user amount to add
			// Add the new users to the player's users
			users += random.Next(left, right);
		}

		// Draws the player token on the board
		public void DrawToken(SpriteBatch sb)
		{

		}

		// Draws the UI portion for the player
		// Parameter: The initial x and y position to start from
		public void DrawUI(int ix, int iy, SpriteBatch sb)
		{
			// Draw the icon
			sb.Draw(iconTexture, new Rectangle(ix, iy, Icon.Width, Icon.Height), Color.White);

			// Draw the stats below
			sb.DrawString(ArtManager.BrownieFont, "Money: " + money, new Vector2(ix, iy+Icon.Height+10), Color.Black);
			sb.DrawString(ArtManager.BrownieFont, "Users: " + users, new Vector2(ix, iy+Icon.Height+30), Color.Black);
			sb.DrawString(ArtManager.BrownieFont, "Memes: " + memes, new Vector2(ix, iy+Icon.Height+50), Color.Black);
			sb.DrawString(ArtManager.BrownieFont, "Bandwidth: " + bandwidthAmount, new Vector2(ix, iy+Icon.Height+70), Color.Black);
			sb.DrawString(ArtManager.BrownieFont, "Malice: " + Malice, new Vector2(ix, iy+Icon.Height+90), Color.Black);
			sb.DrawString(ArtManager.BrownieFont, "Charity: " + Charity, new Vector2(ix, iy+Icon.Height+110), Color.Black);
		}
	}
}
