using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	class HowTo
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		const int NUMBER_OF_BUTTONS = 2;
		const int RETURN_INDEX      = 0;
		const int EXIT_INDEX        = 1;
		const int BUTTON_HEIGHT     = 100;
		const int BUTTON_WIDTH      = 150;
		
		Color[] bColors;
		Texture2D[] bTexs;
		Rectangle[] bRects;

		int mx;		 // mouse x
		int my;		 // mouse y

		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		// Parameters: width and height of the window
		public HowTo(int w, int h)
		{
			// Initialize data
			bColors = new Color[NUMBER_OF_BUTTONS];
			bTexs   = new Texture2D[NUMBER_OF_BUTTONS];
			bRects  = new Rectangle[NUMBER_OF_BUTTONS];

			// Create the button data for our game
			MakeButtons(w, h);
		}

		// This method constructs the buttons
		// Parameters: width and height of the window
		public void MakeButtons(int winW, int winH)
		{
			// Create the Origin Coordinates for the buttons
			int xOrigin = winW/2 - BUTTON_WIDTH/2;
			int yOrigin = winH/2 - BUTTON_HEIGHT/2 + 200;

			// Create all of the buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// Set state, color, and rectangle
				bColors[i] = Color.White;
				bRects[i] = new Rectangle(xOrigin, yOrigin, BUTTON_WIDTH, BUTTON_HEIGHT);

				// Increment y position
				yOrigin += BUTTON_HEIGHT + 25;
			}
		}

		// This method sets the texture values
		// Parmaters: the textures to save
		public void LoadContent(Texture2D tex1, Texture2D tex2)
		{
			bTexs[0] = tex1;
			bTexs[1] = tex2;
		}

		// Passes in a few variables to save for update functions
		public void Update(int mx, int my)
		{
			this.mx = mx;
			this.my = my;
		}

		// This method determines if the mouse is hovering over any buttons
		public void Hover()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				if (bRects[i].X <= mx && mx <= bRects[i].X+BUTTON_WIDTH &&
					bRects[i].Y <= my && my <= bRects[i].Y+BUTTON_HEIGHT)
				{
					bColors[i] = Color.DarkGray;
				}
				// Otherwise, reset the color
				else
				{
					bColors[i] = Color.White;
				}
			}
		}

		// This method determines if a button is being pressed
		public void Pressed()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				if (bRects[i].X <= mx && mx <= bRects[i].X+BUTTON_WIDTH &&
					bRects[i].Y <= my && my <= bRects[i].Y+BUTTON_HEIGHT)
				{
					bColors[i] = Color.Gray;
				}
				// Otherwise, reset the color
				else
				{
					bColors[i] = Color.White;
				}
			}
		}

		// This method determines if a button is being pressed
		public void Released()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				// If the button has already been pressed
				if (bRects[i].X <= mx && mx <= bRects[i].X+BUTTON_WIDTH &&
					bRects[i].Y <= my && my <= bRects[i].Y+BUTTON_HEIGHT &&
					bColors[i] == Color.Gray)
				{
					// Check each case to determine which button is being pressed to change state
					switch (i)
					{
						case RETURN_INDEX:
							StateManager.gameState = StateManager.lastState;
							break;
						case EXIT_INDEX:
							StateManager.gameState = StateManager.GameState.Exit;
							break;
					}
				}
				// Otherwise, reset the color
				else
				{
					bColors[i] = Color.White;
				}
			}
		}

		// This draws all of the buttons
		public void Draw(SpriteBatch sb)
		{
			// Iterate through all buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				sb.Draw(bTexs[i], bRects[i], bColors[i]);
			}
		}
	}
}