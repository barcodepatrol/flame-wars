using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	class RoleSelector
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		// Constants
		const int NUMBER_OF_ROLES = 4;
		const int TOP_HAT		  = 0;
		const int PLASTIC		  = 1;
		const int NARCISSIST	  = 2;
		const int DANKEST		  = 3;
		const int PADDING		  = 40;

		// Role Data
		Texture2D tophatImage;
		Texture2D plasticImage;
		Texture2D narcissistImage;
		Texture2D dankestImage;
		Rectangle roleBounds;
		Color roleColor;
		int currentRole = 0;
		int roleIndex   = 0;

		// Player amount
		int playerAmount;
		int player = 1;

		// Folder data
		Texture2D folderImageClosed;
		Texture2D folderImageOpen;
		Rectangle folderBounds;
		Color folderColor;
		bool folderClosed = true;

		// Width and Height for each image
		int folderClosedWidth;
		int folderClosedHeight;
		int folderOpenWidth;
		int folderOpenHeight;
		int tophatWidth;
		int tophatHeight;
		int plasticWidth;
		int plasticHeight;
		int narcissisitWidth;
		int narcissisitHeight;
		int dankestWidth;
		int dankestHeight;

		int mX;	// mouse x
		int mY;	// mouse y

		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public RoleSelector()
		{
			// Initialize data
			folderBounds = new Rectangle();
			folderColor  = new Color();
			roleBounds   = new Rectangle();
			roleColor	 = Color.White;

			// Init amount of players
			playerAmount = GameManager.NumberOfPlayers;

			// Init image height and width
			folderClosedWidth  = 1020;
			folderClosedHeight = 853;
			folderOpenWidth    = 1124;
			folderOpenHeight   = 853;
			tophatWidth        = 497;
			tophatHeight       = 497;
			plasticWidth       = 364;
			plasticHeight      = 471;
			narcissisitWidth   = 510;
			narcissisitHeight  = 529;
			dankestWidth       = 390;
			dankestHeight      = 390;
		}

		// First initialization
		public void FirstInit()
		{
			// Init amount of players and roles
			playerAmount = GameManager.NumberOfPlayers;
			currentRole  = GameManager.PlayerRoles[0];
		}

		// This initializes the folder image
		public void Initialize()
		{

			// Folder closed
			if (folderClosed)
			{
				// Set width and height
				int fw = (int)(folderClosedWidth * GameManager.ScreenScale);
				int fh = (int)(folderClosedHeight * GameManager.ScreenScale);

				// Set the x and y position of the folder
				int fx = (int)(GameManager.Center.X - fw/2);
				int fy = (int)(GameManager.Center.Y - fh/2);

				// Set new bounds for image
				folderBounds = new Rectangle(fx, fy, fw, fh);
			}
			// Folder open
			else
			{
				// Set width and height
				int fw = (int)(folderOpenWidth * GameManager.ScreenScale);
				int fh = (int)(folderOpenHeight * GameManager.ScreenScale);

				// Set the x and y position of the folder
				int fx = (int)(GameManager.Center.X - fw/1.5 - PADDING);
				int fy = (int)(GameManager.Center.Y - fh/2);

				// Set new bounds for image
				folderBounds = new Rectangle(fx, fy, fw, fh);

				// Generate width and height based off of role
				int rw = 0;
				int rh = 0;
				switch (currentRole)
				{
					case TOP_HAT:
						// Set width and height
						rw = (int)(tophatWidth * GameManager.ScreenScale);
						rh = (int)(tophatHeight * GameManager.ScreenScale);
						break;
					case PLASTIC:
						// Set width and height
						rw = (int)(plasticWidth * GameManager.ScreenScale);
						rh = (int)(plasticHeight * GameManager.ScreenScale);
						break;
					case NARCISSIST:
						// Set width and height
						rw = (int)(narcissisitWidth * GameManager.ScreenScale);
						rh = (int)(narcissisitHeight * GameManager.ScreenScale);
						break;
					case DANKEST:
						// Set width and height
						rw = (int)(dankestWidth * GameManager.ScreenScale);
						rh = (int)(dankestHeight * GameManager.ScreenScale);
						break;
				}

				// Set the x and y position of the folder
				int rx = (int)(GameManager.Center.X - rw/2);
				int ry = (int)(GameManager.Center.Y - rh/2);

				// Set new bounds for image
				roleBounds = new Rectangle(rx, ry, rw, rh);
			}
		}

		// This loads the content for the folder
		public void LoadContent()
		{
			folderImageClosed = ArtManager.FolderClosed;
			folderImageOpen   = ArtManager.FolderOpen;

			tophatImage     = ArtManager.TopHat;
			plasticImage    = ArtManager.Plastic;
			narcissistImage = ArtManager.Narcissist;
			dankestImage    = ArtManager.Dankest;

			Initialize();
		}

		// Passes in a few variables to save for update functions
		public void Update(int mx, int my)
		{
			this.mX = mx;
			this.mY = my;
		}

		// This determines if the mouse is hovering over the folder
		public void Hover()
		{
			// If the mouse x and mouse y values are within the rectangle
			if (folderBounds.X <= mX && mX <= folderBounds.X+folderBounds.Width &&
				folderBounds.Y <= mY && mY <= folderBounds.Y+folderBounds.Height)
			{
				folderColor = Color.DarkGray;
				roleColor = Color.DarkGray;
			}
			// Otherwise, reset the color
			else
			{
				folderColor = Color.White;
				roleColor = Color.White;
			}
		}

		// This determines if the mouse is pressing on the folder
		public void Pressed()
		{
			// If the mouse x and mouse y values are within the rectangle
			if (folderBounds.X <= mX && mX <= folderBounds.X+folderBounds.Width &&
				folderBounds.Y <= mY && mY <= folderBounds.Y+folderBounds.Height)
			{
				folderColor = Color.Gray;
				roleColor = Color.Gray;
			}
			// Otherwise, reset the color
			else
			{
				folderColor = Color.White;
				roleColor = Color.White;
			}
		}

		// This determines if the mouse was just released over the folder
		public void Released()
		{
			// If the mouse x and mouse y values are within the rectangle
			if (folderBounds.X <= mX && mX <= folderBounds.X+folderBounds.Width &&
				folderBounds.Y <= mY && mY <= folderBounds.Y+folderBounds.Height)
			{
				// If the folder is closed
				if (folderClosed)
				{
					// Open folder, reinitialize image
					folderClosed = false;
					Initialize();
				}
				// Go to the next folder
				else
				{
					// Increment player and role index
					roleIndex++;
					player++;

					// Check if we are done
					if (player > playerAmount)
						StateManager.gameState = StateManager.GameState.Game;
					else
					{
						// Close the folder, select new role
						folderClosed = true;
						currentRole = GameManager.PlayerRoles[roleIndex];
						Initialize();
					}
				}
			}
			// Otherwise, reset the color
			else
			{
				folderColor = Color.White;
				roleColor = Color.White;
			}
		}

		// Draw method
		public void Draw(SpriteBatch sb)
		{
			// Draw the folder image
			if (folderClosed == false)
			{
				sb.Draw(folderImageOpen, folderBounds, folderColor);
				
				// Draw the current role
				switch (currentRole)
				{
					case TOP_HAT:
						sb.Draw(tophatImage, roleBounds, roleColor);
						break;
					case PLASTIC:
						sb.Draw(plasticImage, roleBounds, roleColor);
						break;
					case NARCISSIST:
						sb.Draw(narcissistImage, roleBounds, roleColor);
						break;
					case DANKEST:
						sb.Draw(dankestImage, roleBounds, roleColor);
						break;
				}
			}
			else
			{
				sb.DrawString(ArtManager.DisplayFont, 
							  "Player " + player + " click to reveal your role!\nEveryone else look away!!!", 
							  new Vector2(GameManager.Center.X/2, 50), 
							  Color.Black);
				sb.Draw(folderImageClosed, folderBounds, folderColor);
			}
			
		}
	}
}
