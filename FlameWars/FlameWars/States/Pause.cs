﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	class Pause
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		const int NUMBER_OF_BUTTONS = 4;
		const int RESUME_INDEX      = 0;
		const int HOW_TO_INDEX      = 1;
		const int MENU_INDEX        = 2;
		const int EXIT_INDEX        = 3;
		const int BUTTON_HEIGHT     = 100;
		const int BUTTON_WIDTH      = 150;
		
		Color[] buttonColors;
		Texture2D[] buttonTextures;
		Rectangle[] buttonBounds;

		int mX;		 // mouse x
		int mY;		 // mouse y

		private bool messageExists = false;

		#endregion Variables

		public bool MessageExists
		{
			get { return messageExists; }
			set { messageExists = value; }
		}

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		// Parameters: width and height of the window
		public Pause()
		{
			// Initialize data
			buttonColors   = new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds   = new Rectangle[NUMBER_OF_BUTTONS];

			// Create the button data for our game
			MakeButtons();
		}

		// This method constructs the buttons
		public void MakeButtons()
		{
			// Create the Origin Coordinates for the buttons
			int xOrigin = GameManager.Width/2 - BUTTON_WIDTH/2;
			int yOrigin = GameManager.Height/2 - BUTTON_HEIGHT/2;

			// Create all of the buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// Set state, color, and rectangle
				buttonColors[i] = Color.White;
				buttonBounds[i] = new Rectangle(xOrigin, yOrigin, BUTTON_WIDTH, BUTTON_HEIGHT);

				// Increment y position
				yOrigin += BUTTON_HEIGHT + 25;
			}
		}

		// This method sets the texture values to the default for the state.
		public void LoadContent()
		{
			buttonTextures[0] = ArtManager.ResumeButton;
			buttonTextures[1] = ArtManager.HowToButton;
			buttonTextures[2] = ArtManager.MenuButton;
			buttonTextures[3] = ArtManager.ExitButton;
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
						case RESUME_INDEX:
							// If a message existed before the pause
							if (MessageExists) Message.isActive = true;
							StateManager.gameState = StateManager.GameState.Game;
							break;
						case HOW_TO_INDEX:
							StateManager.lastState = StateManager.gameState;
							StateManager.gameState = StateManager.GameState.HowTo;
							break;
						case MENU_INDEX:
							// Set to the reset state first then it will go to the menu
							StateManager.gameState = StateManager.GameState.Reset;
							break;
						case EXIT_INDEX:
							StateManager.gameState = StateManager.GameState.Exit;
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
			// Iterate through all buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				sb.Draw(buttonTextures[i], buttonBounds[i], buttonColors[i]);
			}
		}
	}
}