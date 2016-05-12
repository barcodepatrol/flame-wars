using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars.States
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

		// Role Data
		Texture2D tophatImage;
		Texture2D plasticImage;
		Texture2D narcissistImage;
		Texture2D dankestImage;
		Rectangle roleBounds;
		Color roleColor;
		int currentRole = 0;

		// Player amount
		int playerAmount;
		int player = 0;

		// Folder data
		Texture2D folderImageClosed;
		Texture2D folderImageOpen;
		Rectangle folderBounds;
		Color folderColor;
		bool folderClosed = true;

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
			roleColor    = new Color();

			playerAmount = GameManager.NumberOfPlayers;
		}

		// This initializes the folder image
		public void Initialize()
		{
			// Folder closed
			if (folderClosed)
			{
				// Set width and height
				int fw = (int)(folderImageClosed.Bounds.Width * GameManager.ScreenScale);
				int fh = (int)(folderImageClosed.Bounds.Height * GameManager.ScreenScale);

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
				int fw = (int)(folderImageOpen.Bounds.Width * GameManager.ScreenScale);
				int fh = (int)(folderImageOpen.Bounds.Height * GameManager.ScreenScale);

				// Set the x and y position of the folder
				int fx = (int)(GameManager.Center.X - fw/2);
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
						rw = (int)(tophatImage.Bounds.Width * GameManager.ScreenScale);
						rh = (int)(tophatImage.Bounds.Height * GameManager.ScreenScale);
						break;
					case PLASTIC:
						// Set width and height
						rw = (int)(plasticImage.Bounds.Width * GameManager.ScreenScale);
						rh = (int)(plasticImage.Bounds.Height * GameManager.ScreenScale);
						break;
					case NARCISSIST:
						// Set width and height
						rw = (int)(narcissistImage.Bounds.Width * GameManager.ScreenScale);
						rh = (int)(narcissistImage.Bounds.Height * GameManager.ScreenScale);
						break;
					case DANKEST:
						// Set width and height
						rw = (int)(dankestImage.Bounds.Width * GameManager.ScreenScale);
						rh = (int)(dankestImage.Bounds.Height * GameManager.ScreenScale);
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
			}
			// Otherwise, reset the color
			else
			{
				folderColor = Color.White;
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
			}
			// Otherwise, reset the color
			else
			{
				folderColor = Color.White;
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
					// Increase current role
					currentRole++;

					// Check if we are done
					if (currentRole > NUMBER_OF_ROLES)
						StateManager.gameState = StateManager.GameState.Game;
					else
					{
						folderClosed = true;
						Initialize();
					}
				}
			}
			// Otherwise, reset the color
			else
			{
				folderColor = Color.White;
			}
		}

		// Draw method
		public void Draw(SpriteBatch sb)
		{
			// Draw the folder image
			if (folderClosed)
			{
				sb.Draw(folderImageClosed, folderBounds, folderColor);
				
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
				sb.Draw(folderImageOpen, folderBounds, folderColor);


			
		}
	}
}
