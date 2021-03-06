﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	public static class Message
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		// Used to control whether the messagebox is active
		static private bool active = false;

		// Stores the card and bond data
		static private Card card;
		static private Bond bond;
		static private int  cost;
		static private bool buying;
		static private bool bought;

		// Used for drawing the background box
		static private Texture2D image;		 // Stores the box's texture
		static private Vector2 position;	 // Position. X and Y Co-ordinates.
		static private Vector2 center;		 // Provides a center point for players to be drawn upon.
		static private Vector2 textPosition; // Position for the text
		static private Rectangle boundaries; // Bounds. X and Y are arbitrary. Width and Height.
		static private Color tint;			 // DrawColor. Not everything will be drawn in white.

		// Used for displaying content
		static private string message;		 // Contains the message to display
		static private bool cancel = false;  // Determines if a cancel button is displayed

		// Used for message box
		static private int BOX_WIDTH  = 200; // This one cannot be constant since we don't know message length
		static private int BOX_HEIGHT = 200; // This one cannot be constant since we don't know message length
		const int MAX_CHARACTERS = 80;
		const int PADDING = 20;

		// Used for buttons
		const int NUMBER_OF_BUTTONS = 2;
		const int OK_INDEX          = 0;
		const int CANCEL_INDEX      = 1;
		const int BUTTON_HEIGHT     = 50;
		const int BUTTON_WIDTH      = 75;
		
		// Stores button info
		static Color[] buttonColors;
		static Texture2D[] buttonTextures;
		static Rectangle[] buttonBounds;

		// Stores mouse info
		static int mX;
		static int mY;

		#endregion

		#region Properties

		// Stores whether or not the messagebox is active
		public static bool isActive
		{
			get { return active; }
			set { active = value; }
		}

		// Stores the card the messagebox is displaying
		public static Card CurrentCard
		{
			get { return card; }
			set { card = value; }
		}
		
		// Stores the card the messagebox is displaying
		public static Bond CurrentBond
		{
			get { return bond; }
			set { bond = value; }
		}

		// Stores the cost of the messagebox's card
		public static int Cost
		{
			get { return cost; }
			set { cost = value; }
		}

		// Stores whether or not something was bought
		public static bool Bought
		{
			get { return bought; }
			set { bought = value; }
		}

		// Stores the position of the box
		public static Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		// Stores the Width and Height of the path object.
		public static Rectangle Bounds
		{
			get { return boundaries; }
			set { boundaries = value; }
		}

		// Stores the X position.
		public static int X
		{
			get { return (int) position.X; }
			set { position.X = value; }
		}

		// Stores the Y position.
		public static int Y
		{
			get { return (int)position.Y; }
			set { position.Y = value; }
		}

		// Stores the center vector.
		public static Vector2 Center
		{
			get { return center; }
			set { center = value; }
		}

		// Gets the current draw color for the path object.
		public static Color DrawColor {
			get { return tint; }
			set { tint = value; }
		}

		#endregion

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// This method activates/reactivates the messagebox
		static public void Activate()
		{
			active = true;
			cancel = false;
			bought = false;
			card = null;
			bond = null;
			cost   = 0;
		}

		// Creates Messages - Includes a change to the default cancel value
		static public void CreateMessage(string message)
		{
			// Set up color
			tint = Color.White;

			// Nullfiy card
			card = null;

			// Set up button data
			buttonColors   = new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds   = new Rectangle[NUMBER_OF_BUTTONS];

			// Save message data
			Message.message = message;

			// Load the button textures
			LoadContent();

			// Create the message box
			MakeMessageBox();
		}
		
		// Creates Messages - Asks user if they want to purchase card
		static public void CreateMessage(Card c)
		{
			// Set up color
			tint = Color.White;

			// Set up button data
			buttonColors =   new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds =   new Rectangle[NUMBER_OF_BUTTONS];

			// Save the cost and card - default card buying message
			message = "Would you like to purchase the card?\n" +
					  "Cost: " + c.Cost + "\n";
			card = c;
			Cost = c.Cost;
			cancel = true;
			buying = true;

			// Check if premium - Premium cards can be canceled
			if (c.Premium)
			{
				message = "Would you like to purchase the PREMIUM card?\n" +
						  "Cost: " + c.Cost + "\n";
			}

			// Load the button textures
			LoadContent();

			// Create the message box
			MakeMessageBox();
		}

		// Creates Messages - Asks user if they want to purchase a bond
		static public void CreateMessage(Bond b)
		{
			// Set up color
			tint = Color.White;

			// Set up button data
			buttonColors =   new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds =   new Rectangle[NUMBER_OF_BUTTONS];

			// Save the cost and card - default card buying message
			message = "Would you like to purchase the bond?\n" +
					  "Cost: " + b.Cost + "\n";
			bond = b;
			Cost = b.Cost;
			cancel = true;
			buying = true;

			// Load the button textures
			LoadContent();

			// Create the message box
			MakeMessageBox();
		}

		// Creates Messages - Displays card
		// Also saves and loads a card's information
		static public void CreateMessage()
		{
			// Set up color
			tint = Color.White;

			// Set up button data
			buttonColors   = new Color[NUMBER_OF_BUTTONS];
			buttonTextures = new Texture2D[NUMBER_OF_BUTTONS];
			buttonBounds   = new Rectangle[NUMBER_OF_BUTTONS];

			// Save card and message data
			message = card.Description;
			buying  = false;

            // Determine if card can be canceled or not
            // Premium Card: Can be canceled
            // Plebian Card: Cannot be canceled
            if (card.Premium) cancel = true;
            else			  cancel = false;

			// Load the button textures
			LoadContent();

			// Create the message box
			MakeMessageBox();
		}

		// Creats Card display
		// NOTE: Only called when we use the CreateMessage(int cost) method
		// This method is called internally

		// This method loads the button textures
		static public void LoadContent()
		{
			buttonTextures[0] = ArtManager.OkButton;
			buttonTextures[1] = ArtManager.CancelButton;
			image             = ArtManager.MessageBox;
		}

		// This method constructs the message box
		static public void MakeMessageBox()
		{
			// Create the box and string location
			MakeBox();

			// Calculate button placement
			int bx = 0, by = 0;

			// Calculate for two buttons
			if (cancel)
			{
				bx = (int)Center.X - BUTTON_WIDTH/2 - BUTTON_WIDTH + PADDING;
				by = (int)position.Y + BOX_HEIGHT - BUTTON_HEIGHT - PADDING;
			}
			// Calculate just an "Ok" button
			else
			{
				bx = (int)Center.X - BUTTON_WIDTH/2;
				by = (int)position.Y + BOX_HEIGHT - BUTTON_HEIGHT - PADDING;
			}

			// Create the buttons
			MakeButtons(bx, by);
		}

		// This method constructs the box rectangle and message location
		static public void MakeBox()
		{
			// Update string if this messagebox involves a card
			if (card != null && buying != true)
			{
				CardString();
			}

			// Get the size of the string
			Vector2 messageSize = ArtManager.MainFont.MeasureString(message);
			
			// Determine size of box
			BOX_WIDTH = (int)messageSize.X + (2*PADDING);
			BOX_HEIGHT = (int)messageSize.Y + PADDING*2 + BUTTON_HEIGHT;

			// Create box placement data
			center     = GameManager.Center;
			position   = new Vector2 (GameManager.Center.X-(BOX_WIDTH/2), GameManager.Center.Y-(BOX_HEIGHT/2));
			boundaries = new Rectangle((int)position.X, (int)position.Y, BOX_WIDTH, BOX_HEIGHT);

			// Set message vector
			textPosition = new Vector2 (position.X+PADDING, position.Y+PADDING);
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
				xOrigin += BUTTON_WIDTH + PADDING;
			}
		}

		// This method creates and sets up the box texture
		static public void CreateBoxTexture()
		{
			// Create data
			Color[] data = new Color[BOX_WIDTH*BOX_HEIGHT];
			image = new Texture2D(GameManager.Graphics, BOX_WIDTH, BOX_HEIGHT);

			// Fill with blank color data
			for (int i = 0; i < data.Length; ++i)
				data[i] = Color.DarkGray;

			// Sets the texture equal to the color data
			image.SetData(data);
		}
		
		// This method updates the string to include card data
		static public void CardString()
		{
			message = message.Insert(0, "Name: " + card.Name + "\n");
			message += ("\nTarget: " + card.Target + "\n");
			message += ("Attribute: " + card.Attribute + "\n");
			message += ("Amount: " + card.Amount + "\n");
		}

		// This method determines if the mouse is hovering over any buttons
		static public void Hover()
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
		static public void Pressed()
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
		static public void Released()
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
							case OK_INDEX:
							// We just bought a bond
							if (bond != null && buying)
							{
								if (GameManager.CurrentPlayer.Money >= bond.Cost)
								{
									bought = true;
									active = false;
								}
								else
								{
									cancel = false;
									CreateMessage("INSUFFICIENT FUNDS.");
								}
								GameManager.EndTurn = true;
							}
							// We just bought a card so display it
							else if (card != null && buying)
							{
								if (GameManager.CurrentPlayer.Money >= card.Cost)
								{
									// Create card display
									bought = true;
									CreateMessage();
								}
								else
								{
									cancel = false;
									CreateMessage("INSUFFICIENT FUNDS.");
								}
							}
							// Check to see if there is a card and it targets others
							else if (card != null && card.Target == "Target Others")
							{
								active = false;
								Target.Activate();
								Target.CreateTarget();
							}
							else if (GameManager.EndGame)
							{
								active = false;
								// GO BACK TO MENU HERE STATE CHANGE.
								GameManager.Reset();
							}
							else
							{
								// Return to previous state - perform action if necessary
								active = false;
								GameManager.EndTurn = true;
							}
							break;
						case CANCEL_INDEX:
							// Return to previous state - Do not perform action
							active = false;
							GameManager.EndTurn = true;
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

		// Passes in a few variables to save for update functions
		static public void Update(int mx, int my)
		{
			Message.mX = mx;
			Message.mY = my;
		}

		// Draws the message box and the associated text/buttons
		static public void Draw(SpriteBatch sb)
		{
			// Draw box
			sb.Draw(image, boundaries, Color.White);

			// Draw message
			sb.DrawString(ArtManager.MainFont, message, textPosition, Color.Black);

			// Draw buttons
			if (cancel)
			{
				sb.Draw(buttonTextures[OK_INDEX],
						buttonBounds[OK_INDEX],
						buttonColors[OK_INDEX]);
				sb.Draw(buttonTextures[CANCEL_INDEX],
						buttonBounds[CANCEL_INDEX],
						buttonColors[CANCEL_INDEX]);
			}
			else
			{
				sb.Draw(buttonTextures[OK_INDEX],
						buttonBounds[OK_INDEX],
						buttonColors[OK_INDEX]);
			}
		}
	}
}
