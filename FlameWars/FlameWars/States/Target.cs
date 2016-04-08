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
	public static class Target
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		// Used to control whether the messagebox is active
		static private bool active = false;

		// Used for drawing the background box
		static private Texture2D image; // Stores the box's texture
		static private Vector2 position; // Position. X and Y Co-ordinates.
		static private Vector2 center; // Provides a center point for players to be drawn upon.
		static private Rectangle boundaries; // Bounds. X and Y are arbitrary. Width and Height.
		static private Color tint; // DrawColor. Not everything will be drawn in white.

		// Used for message box
		static private int BOX_WIDTH  = 100; // This one cannot be constant since we don't know message length
		static private int BOX_HEIGHT = 200; // This one cannot be constant since we don't know message length
		const int PADDING = 20;

		// Used for buttons
		const int NUMBER_OF_BUTTONS = 4;
		const int PLAYER1_INDEX     = 0;
		const int PLAYER2_INDEX     = 1;
		const int PLAYER3_INDEX     = 2;
		const int PLAYER4_INDEX     = 3;
		const int BUTTON_WIDTH      = 100;
		const int BUTTON_HEIGHT     = 50;

		// Stores button info
		static Color[] buttonColors;
		static Texture2D[] buttonTextures;
		static Rectangle[] buttonBounds;

		// Stores mouse info
		static int mX;
		static int mY;

		// Stores whether or not they accepted the card
		static private int playerTarget = 0;

		#endregion

		#region Properties

		public static bool isActive
		{
			get { return active; }
			set { active = value; }
		}

		// Stores the card acceptance boolean.
		public static int PlayerTarget
		{
			get { return playerTarget; }
			set { playerTarget = value; }
		}

		#endregion

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// This method activates/reactivates the messagebox
		static public void Activate()
		{
			active = true;
		}

		// This method deactivates the messagebox
		static public void Deactivate()
		{
			active = false;
		}

		// Creates Messages - Includes a change to the default cancel value
		static public void CreateTarget()
		{
			// Set up color
			tint = Color.White;

			// Nullfiy playerTarget;
			playerTarget = 0;

			// Set up button data
			buttonColors   = new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds   = new Rectangle[NUMBER_OF_BUTTONS];

			// Load the button textures
			LoadContent();

			// Create the message box
			MakeTargetBox();

			// Set up texture
			//CreateBoxTexture();
		}

		// This method loads the button textures
		static public void LoadContent()
		{
			buttonTextures[0] = ArtManager.Player1Button;
			buttonTextures[1] = ArtManager.Player2Button;
			buttonTextures[2] = ArtManager.Player3Button;
			buttonTextures[3] = ArtManager.Player4Button;
			image             = ArtManager.TargetBox;
		}

		// This method constructs the message box
		static public void MakeTargetBox()
		{
			// Create the box and string location
			MakeBox();

			// Calculate button placement
			int bx = 0, by = 0;

			bx = (int)center.X - (BUTTON_WIDTH/2);
			by = (int)position.Y + PADDING;

			// Create the buttons
			MakeButtons(bx, by);
		}

		// This method constructs the box rectangle and message location
		static public void MakeBox()
		{
			// Create box placement data
			position = new Vector2(GameManager.Center.X - (BOX_WIDTH / 2) - PADDING, GameManager.Center.Y - (BOX_HEIGHT / 2));
			center = GameManager.Center;
			boundaries = new Rectangle((int)position.X, (int)position.Y, BOX_WIDTH+(2*PADDING), BOX_HEIGHT+(5*PADDING));
		}

		// This method constructs the buttons
		// Parameters: width and height of the window
		static public void MakeButtons(int xOrigin, int yOrigin)
		{
			// Create all of the buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// Set state, color, and rectangle
				buttonColors[i] = Color.White;
				buttonBounds[i] = new Rectangle(xOrigin, yOrigin, BUTTON_WIDTH, BUTTON_HEIGHT);

				// Increment y position
				yOrigin += BUTTON_HEIGHT + PADDING;
			}
		}

		// This method creates and sets up the box texture
		static public void CreateBoxTexture()
		{
			// Create data
			Color[] data = new Color[BOX_WIDTH * BOX_HEIGHT];
			image = new Texture2D(GameManager.Graphics, BOX_WIDTH, BOX_HEIGHT);

			// Fill with blank color data
			for (int i = 0; i < data.Length; ++i)
				data[i] = Color.DarkGray;

			// Sets the texture equal to the color data
			image.SetData(data);
		}

		// This method determines if the mouse is hovering over any buttons
		static public void Hover()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				if (buttonBounds[i].X <= mX && mX <= buttonBounds[i].X + BUTTON_WIDTH &&
					buttonBounds[i].Y <= mY && mY <= buttonBounds[i].Y + BUTTON_HEIGHT)
				{
					buttonColors[i] = Color.DarkGray;
				}
				// Otherwise, reset the color
				else
				{
					buttonColors[i] = Color.White;
				}
			}
		}

		// This method determines if a button is being pressed
		static public void Pressed()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				if (buttonBounds[i].X <= mX && mX <= buttonBounds[i].X + BUTTON_WIDTH &&
					buttonBounds[i].Y <= mY && mY <= buttonBounds[i].Y + BUTTON_HEIGHT)
				{
					buttonColors[i] = Color.Gray;
				}
				// Otherwise, reset the color
				else
				{
					buttonColors[i] = Color.White;
				}
			}
		}

		// This method determines if a button is being pressed
		static public void Released()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				// If the button has already been pressed
				if (buttonBounds[i].X <= mX && mX <= buttonBounds[i].X + BUTTON_WIDTH &&
					buttonBounds[i].Y <= mY && mY <= buttonBounds[i].Y + BUTTON_HEIGHT &&
					buttonColors[i] == Color.Gray)
				{
					// Set the playerTarget equal to the current looping index
					playerTarget = i;

					// The players turn is over
					GameManager.EndTurn = true;
				}
				// Otherwise, reset the color
				else
				{
					buttonColors[i] = Color.White;
				}
			}
		}

		// Passes in a few variables to save for update functions
		static public void Update(int mx, int my)
		{
			Target.mX = mx;
			Target.mY = my;
		}

		// Draws the message box and the associated text/buttons
		static public void Draw(SpriteBatch sb)
		{
			// Draw box
			sb.Draw(image, boundaries, Color.White);

			// Draw buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				sb.Draw(buttonTextures[i],
						buttonBounds[i],
						buttonColors[i]);
			}
		}
	}
}
