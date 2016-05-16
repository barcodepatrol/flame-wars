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

		// Constants
		const int NUMBER_OF_SLIDES  = 10;  // TO DO: UPDATE ONCE WE HAVE THE REAL NUMBER OF SLIDES
		const int NUMBER_OF_BUTTONS = 4;
		const int RETURN_INDEX      = 0;
		const int EXIT_INDEX        = 1;
		const int BACK_INDEX        = 2;
		const int NEXT_INDEX		= 3;
		const int PADDING			= 20;
		const int BUTTON_HEIGHT     = 100;
		const int BUTTON_WIDTH      = 150;
		      int SLIDE_X			= GameManager.Width/4;
		      int SLIDE_Y			= 100;
		      int SLIDE_HEIGHT		= 150;
		      int SLIDE_WIDTH		= GameManager.Width/2;
		
		// Button data
		Color[] buttonColors;
		Texture2D[] buttonTextures;
		Rectangle[] buttonBounds;

		// Slide data
		Texture2D[] slideTextures;
		Rectangle slideBounds;
		int slide = 0;

		int mX;		 // mouse x
		int mY;		 // mouse y

		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		// Parameters: width and height of the window
		public HowTo()
		{
			// Initialize data
			buttonColors   = new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds   = new Rectangle[NUMBER_OF_BUTTONS];
			slideTextures  = new Texture2D[NUMBER_OF_SLIDES];

			// Create the button data for our game
			MakeButtons();
		}

		// This method constructs the buttons
		// Parameters: width and height of the window
		public void MakeButtons()
		{
			// Create the Origin Coordinates for the buttons
			int xOrigin = GameManager.Width/2 - BUTTON_WIDTH/2;
			int yOrigin = GameManager.Height/2 - BUTTON_HEIGHT/2 + 200;

			// Create the state buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS-2; i++)
			{
				// Set color and rectangle
				buttonColors[i] = Color.White;
				buttonBounds[i] = new Rectangle(xOrigin, yOrigin, BUTTON_WIDTH, BUTTON_HEIGHT);

				// Increment y position
				yOrigin += BUTTON_HEIGHT + 25;
			}

			// Create the slide
			slideBounds = new Rectangle(SLIDE_X, SLIDE_Y, SLIDE_WIDTH, SLIDE_HEIGHT);
			
			// Back button
			buttonColors[BACK_INDEX] = Color.White;
			buttonBounds[BACK_INDEX] = new Rectangle(SLIDE_X-BUTTON_WIDTH-PADDING, 
													 SLIDE_Y+SLIDE_HEIGHT/2-BUTTON_HEIGHT/2,
													 BUTTON_WIDTH, BUTTON_HEIGHT);

			// Next button
			buttonColors[NEXT_INDEX] = Color.White;
			buttonBounds[NEXT_INDEX] = new Rectangle(SLIDE_X+SLIDE_WIDTH, 
													 SLIDE_Y+SLIDE_HEIGHT/2-BUTTON_HEIGHT/2,
													 BUTTON_WIDTH, BUTTON_HEIGHT);
		}

		// This method sets the texture values to the default for the state.
		public void LoadContent()
		{
			buttonTextures[0] = ArtManager.ReturnButton;
			buttonTextures[1] = ArtManager.ExitButton;

			// THESE NEED TO BE CHANGED TO THE REAL TEXTURES
			buttonTextures[2] = ArtManager.ExitButton; // PREV
			buttonTextures[3] = ArtManager.PlayButton; // NEXT

			// TO DO:
			// LOAD THE SLIDE CONTENT HERE
			slideTextures = ArtManager.Slides;

			SLIDE_HEIGHT = slideTextures[0].Height;
			SLIDE_WIDTH = slideTextures[0].Width;
			SLIDE_X = CalculateXOrigin(SLIDE_WIDTH);
			SLIDE_Y = CalculateYOrigin(SLIDE_HEIGHT);
		}

		// Passes in a few variables to save for update functions
		public void Update(int mx, int my)
		{
			this.mX = mx;
			this.mY = my;
		}

		// This method determines if the mouse is hovering over any buttons
		public void Hover()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				if (buttonBounds[i].X <= mX && mX <= buttonBounds[i].X+BUTTON_WIDTH &&
					buttonBounds[i].Y <= mY && mY <= buttonBounds[i].Y+BUTTON_HEIGHT)
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
		public void Pressed()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				if (buttonBounds[i].X <= mX && mX <= buttonBounds[i].X+BUTTON_WIDTH &&
					buttonBounds[i].Y <= mY && mY <= buttonBounds[i].Y+BUTTON_HEIGHT)
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
		public void Released()
		{
			// Iterate through every button
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// If the mouse x and mouse y values are within the rectangle
				// If the button has already been pressed
				if (buttonBounds[i].X <= mX && mX <= buttonBounds[i].X+BUTTON_WIDTH &&
					buttonBounds[i].Y <= mY && mY <= buttonBounds[i].Y+BUTTON_HEIGHT &&
					buttonColors[i] == Color.Gray)
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
						case BACK_INDEX:
							if ((slide-=1) < 0)
								slide = (slideTextures.Length - 1);
							break;
						case NEXT_INDEX:
							if ((slide+=1) == slideTextures.Length)
								slide = 0;
							break;
					}
				}
				// Otherwise, reset the color
				else
				{
					buttonColors[i] = Color.White;
				}
			}
		}

		// This draws all of the buttons
		public void Draw(SpriteBatch sb)
		{
			// Draw how to instructions
			sb.Draw(slideTextures[slide], new Rectangle(SLIDE_X, SLIDE_Y, SLIDE_WIDTH, SLIDE_HEIGHT), Color.White);

			// Iterate through all buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				sb.Draw(buttonTextures[i], buttonBounds[i], buttonColors[i]);
			}
		}

		// Calculate the X and Y for a given slide.
		public int CalculateXOrigin(int sWidth)
		{
			int xOrigin = 0;

			// Find the center.
			int center = GameManager.Width / 2;
			int origin = sWidth / 2;

			xOrigin = center - origin;

			return xOrigin;
		}

		// Calculate the Y for a given slide.
		public int CalculateYOrigin(int sHeight)
		{
			int yOrigin = 0;

			// Find the center.
			int center = GameManager.Height / 2;
			int origin = sHeight / 2;

			yOrigin = center - origin;


			return yOrigin;
		}
	}
}