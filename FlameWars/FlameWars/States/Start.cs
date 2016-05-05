﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	class Start
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		const int NUMBER_OF_BUTTONS = 3;
		const int _2PLAYERS_INDEX   = 0;
		const int _3PLAYERS_INDEX   = 1;
		const int _4PLAYERS_INDEX   = 2;
		const int BUTTON_HEIGHT     = 100;
		const int BUTTON_WIDTH      = 150;
		const int ICON_HEIGHT		= 100;
		const int PADDING			= 20;
		
		// Button Data
		Color[] buttonColors;
		Texture2D[] buttonTextures;
		Rectangle[] buttonBounds;

		// Icon Data
		Color[] iconColors;
		Texture2D[] iconTextures;
		Rectangle[] iconBounds;

		int mX;		 // mouse x
		int mY;		 // mouse y

		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		// Parameters: width and height of the window
		public Start()
		{
			// Initialize data
			buttonColors   = new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds   = new Rectangle[NUMBER_OF_BUTTONS];
			iconColors   = new Color[NUMBER_OF_BUTTONS];
			iconTextures = new Texture2D[NUMBER_OF_BUTTONS];
			iconBounds   = new Rectangle[NUMBER_OF_BUTTONS];
		}

		// This method constructs the buttons
		public void MakeButtons()
		{
			// Create the Origin Coordinates for the buttons
			int xOrigin = GameManager.Width/2 - BUTTON_WIDTH/2 - BUTTON_WIDTH - PADDING;
			int yOrigin = GameManager.Height/2 - BUTTON_HEIGHT/2 - BUTTON_HEIGHT - PADDING;

			// Create all of the buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				// Set state, color, and rectangle
				buttonColors[i] = Color.White;
				buttonBounds[i] = new Rectangle(xOrigin, yOrigin, BUTTON_WIDTH, BUTTON_HEIGHT);

				// Increment y position
				xOrigin += BUTTON_WIDTH + PADDING;
			}
		}

		// This method constructs the icons
		public void MakeIcons()
		{
			// Iterate through buttons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				iconColors[i] = Color.White;
				// Middle of button - half icon width
				// Same Y, move down via padding
				// Width and Height
				iconBounds[i] = new Rectangle(buttonBounds[i].X + buttonBounds[i].Width/2 - buttonTextures[i].Width/2, 
											  buttonBounds[i].Y + BUTTON_HEIGHT + 2*PADDING,							  
											  buttonTextures[i].Width, ICON_HEIGHT);									  
			}
		}

		// This method sets the texture values
		// Parameters: the textures to save
		public void LoadContent(Texture2D tex1, Texture2D tex2, Texture2D tex3)
		{
			buttonTextures[0] = tex1;
			buttonTextures[1] = tex2;
			buttonTextures[2] = tex3;
		}

		// NEEDS TO BE UPDATED
		// This method sets the texture values to the default for the state.
		public void LoadContent()
		{
			// NEEDS TO BE UPDATED 
			buttonTextures[0] = ArtManager.PlayButton;
			buttonTextures[1] = ArtManager.HowToButton;
			buttonTextures[2] = ArtManager.ExitButton;
			iconTextures[0] = ArtManager.PlayButton;
			iconTextures[1] = ArtManager.HowToButton;
			iconTextures[2] = ArtManager.ExitButton;

			// Create the button data for our game
			MakeButtons();
			MakeIcons();
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
						case _2PLAYERS_INDEX: GameManager.NumberOfPlayers = 2; break;
						case _3PLAYERS_INDEX: GameManager.NumberOfPlayers = 3; break;
						case _4PLAYERS_INDEX: GameManager.NumberOfPlayers = 4; break;
					}

					// Set to game state
					StateManager.gameState = StateManager.GameState.Game;
				}
				// Otherwise, reset the color
				else
				{
					buttonColors[i] = Color.White;
				}
			}
		}

		// This draws all of the buttons and icons
		public void Draw(SpriteBatch spriteBatch)
		{
			// Iterate through all buttons and icons
			for (int i = 0; i < NUMBER_OF_BUTTONS; i++)
			{
				spriteBatch.Draw(buttonTextures[i], buttonBounds[i], buttonColors[i]);
				spriteBatch.Draw(iconTextures[i], iconBounds[i], iconColors[i]);
			}
		}
	}
}