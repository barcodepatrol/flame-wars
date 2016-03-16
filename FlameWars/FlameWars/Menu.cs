using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	class Menu
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		enum BState
		{
			HOVER,
			UP,
			DOWN,
			RELEASED
		}

		const int NUMBER_OF_BUTTONS = 3;
		const int PLAY_INDEX        = 0;
		const int HOW_TO_INDEX      = 1;
		const int EXIT_INDEX        = 2;
		const int BUTTON_HEIGHT     = 50;
		const int BUTTON_WIDTH      = 75;

		Color bgColor;
		Color[] bColors;
		Texture2D[] bTexs;
		Rectangle[] bRects;
		BState[] bStates;


		bool mPress; // mouse press
		bool pPress; // previous pressed
		int mx;		 // mouse x
		int my;		 // mouse y

		#endregion Variables

		#region Properties
		//* Properties *//
		// Stores the 2D texture for the play button
		public Texture2D PlayB
		{
			get { return this.PlayButton; }
			set { this.PlayButton = value; }
		}
		// Stores the 2D texture for the howto button
		public Texture2D HowToB
		{
			get { return this.HowToButton; }
			set { this.HowToButton = value; }
		}
		// Stores the 2D texture for the exit button
		public Texture2D ExitB
		{
			get { return this.ExitButton; }
			set { this.ExitButton = value; }
		}
		#endregion

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		// Parameters: width and height of the window
		public Menu(int w, int h)
		{
			// Initialize data
			bgColor = new Color();
			bColors = new Color[NUMBER_OF_BUTTONS];
			bTexs   = new Texture2D[NUMBER_OF_BUTTONS];
			bRects  = new Rectangle[NUMBER_OF_BUTTONS];
			bStates = new BState[NUMBER_OF_BUTTONS];

			// Set color to black
			bgColor = Color.Black;

			// Create the button data for our game
			MakeButtons(w, h);
		}

		// This method constructs the buttons
		// Parameters: width and height of the window
		public void MakeButtons(int winW, int winH)
		{
			// Create the Origin Coordinates for the buttons
			int xOrigin = winW/2 - BUTTON_WIDTH/2;
			int yOrigin = winH/2 - BUTTON_HEIGHT/2;

			// Create all of the buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// Set state, color, and rectangle
				bStates[i] = BState.UP;
				bColors[i] = Color.White;
				bRects[i] = new Rectangle(xOrigin, yOrigin, BUTTON_WIDTH, BUTTON_HEIGHT);

				// Increment y position
				yOrigin += BUTTON_HEIGHT;
			}
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
					bColors[i] = Color.Gray;
					return;
				}
				// Otherwise, reset the color
				else
				{
					bColors[i] = Color.White;
				}
			}
		}

		// This method determines if a button is being pressed
		public void Press()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				if (bRects[i].X <= mx && mx <= bRects[i].X+BUTTON_WIDTH &&
					bRects[i].Y <= my && my <= bRects[i].Y+BUTTON_HEIGHT)
				{
					bColors[i] = Color.DarkGray;

					// Check each case to determine which button is being pressed to change state
					switch (i)
					{
						case PLAY_INDEX:
							StateManager.gameState = StateManager.GameState.Game;
							break;
						case HOW_TO_INDEX:
							StateManager.gameState = StateManager.GameState.HowTo;
							break;
						case EXIT_INDEX:
							// Exit Game
							break;
					}

					return;
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
