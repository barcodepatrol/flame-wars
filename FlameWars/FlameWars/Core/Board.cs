using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	public class Board
	{
		// TODO
		/********
			- Center board. Stretch to edges of the display screen.
			- Center the path. Shrink so it fits neatly within the designated spaces.
			- Create the UI for the corners of the game board.
			- Set up player movement.
		********/

		// ============================================================================
		// ========================== Constants / Readonly ============================
		// ============================================================================

		private const int SQUARE_WIDTH = 100;
		private const int SQUARE_HEIGHT = 100;
		private const int BOARD_HEIGHT = 700;
		private const int BOARD_WIDTH = 1200;

		private readonly Color RESOURCE_COLOR = Color.Green;
		private readonly Color CARD_COLOR = Color.Red;
		private readonly Color BONUS_COLOR = Color.Blue;
		private readonly Color STOCK_COLOR = Color.Orange;
		private readonly Color RANDOM_COLOR = Color.Yellow;
		private readonly Color EMPTY_COLOR = Color.White;

		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables
		/****
			enum SpaceType - Indicators for path type.

			Resource - This path provides resources (users, memes, bandwitdh, or money).
			Card - This path triggers a card drawing.
			Bonus - This path triggers a bonus for the player.
			Stock - This path triggers a stock market mechanic.
			Random - This path triggers the stock.
			Empty - This path has no triggers. It's an empty, brick pavestone.

			****/

		// Enumerator.
		public enum SpaceType { Resource, Card, Bonus, Stock, Random, Empty };

		// Collections.
		Path[] track; // Array containing the board's path objects.
		Color[] tints; // Contains the tints for painting the objects.
		Texture2D[] pathTextures; // Contains the different textures that can be drawn depending on path texture ID.

		// Textures.
		Texture2D boardTexture; // The board texture.

		// Boundaries.
		Rectangle boardBounds; // The board's boundaries and position.
		Rectangle pathBounds; // The path object's bounds and position.

		// Random.
		Random random;

		// Number of vertical squares
		// Number of horizontal squares
		private const int VERTICAL_LENGTH = 7;	
		private const int HORIZONTAL_LENGTH = 12;

		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Board(Texture2D[] pathImage, Texture2D boardImage)
		{
			track          = new Path[34];
			tints          = new Color[6];
			// 200, 200 are just starter values, this must be determined some other time
			boardBounds	   = new Rectangle(200, 200, BOARD_WIDTH, BOARD_HEIGHT);
			pathBounds     = new Rectangle(0, 0, SQUARE_WIDTH, SQUARE_HEIGHT);
			random         = new Random();

			// Load the texture for all path objects
			this.pathTextures = pathImage;
			this.boardTexture = boardImage;

			// create the tints
			CreateTints();
			
			// Generate the board
			CreateBoard();
		}

		// EDIT: Do we even need an update function here?
		public void Update(GameTime gameTime)
		{

		}

		// This method draws all of the path objects to the screen
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(boardTexture, new Rectangle(0, 0, boardTexture.Width, boardTexture.Height), Color.White);
			// Iterate through all path objects
			for (int i = 0; i < track.Length; i++)
			{
				spriteBatch.Draw(GetPathTexture(track[i]), track[i].Bounds, track[i].DrawColor);
			}
		}

		// This method generates Path objects to fill the Track Array
		public void CreateBoard()
		{
			// Fill the entire track array
			for (int i = 0; i < track.Length; i++)
			{
				// Create position
				// If intervals set to handle each side of board

				// TESTING: Necessary testing for the equations
				// I spent some time thinking about them and I believe they are correct
				// (more or less) but we still need to test them

				#region CreatePosition
				
				// Initialize a position vector
				Vector2 positionVector = new Vector2();

				// Bottom of the board
				if(i >= 0 && i < 12)
				{
					positionVector = new Vector2(i * SQUARE_WIDTH, 
									 (BOARD_HEIGHT - SQUARE_HEIGHT));
				}
				// Right column of the board
				if(i >= 12 && i < 17)
				{
					positionVector = new Vector2((BOARD_WIDTH - SQUARE_WIDTH), 
									 (BOARD_HEIGHT - ((i-10) * SQUARE_HEIGHT)));
				}
				// Top row of the board
				if(i >= 17 && i < 29)
				{
					positionVector = new Vector2(((12 * SQUARE_WIDTH) - (i-16)*SQUARE_WIDTH), 
									 (BOARD_HEIGHT / VERTICAL_LENGTH) - SQUARE_HEIGHT);
				}
				// Left column of the board
				if(i >= 29 && i < 34)
				{
					positionVector = new Vector2(0, (i-29) * SQUARE_HEIGHT + SQUARE_HEIGHT);
				}
				#endregion CreatePosition

				#region CreateRec,Tint,Type

				// Create the position Rectangle
				pathBounds = new Rectangle((int)positionVector.X, (int)positionVector.Y, SQUARE_WIDTH, SQUARE_HEIGHT);

				// Select the tint randomly
				Color tint = tints[random.Next(0, tints.Length)];

				// Select the path texture randomly
				int id = random.Next(0, pathTextures.Length);

				// Select space type
				// Enums cast to ints
				// Save an int from 0 to 5
				Array values   = Enum.GetValues(typeof(SpaceType));
				SpaceType spaceType = (SpaceType)values.GetValue(random.Next(values.Length));

				#endregion CreateRec,Tint,Type

				// Create the Path Object
				// Save the data into the Path object
				Path pathSquare      = new Path(positionVector,	// Sets the position for the path square.
												pathBounds,		// Sets the boundaries for the path square.
												id,				// Sets the textureID for the path square.
												tint,			// Sets the draw color for the path square.
												spaceType);		// Sets the space type for the path square.

				// Add the Path Object to our current path array
				track[i] = pathSquare;
			}
		}

		public void CreateTints()
		{
			tints[0] = RESOURCE_COLOR;	// resource
			tints[1] = CARD_COLOR;		// card
			tints[2] = BONUS_COLOR;		// bonus
			tints[3] = STOCK_COLOR;		// stock
			tints[4] = RANDOM_COLOR;	// Random
			tints[5] = EMPTY_COLOR;		// empty
		}


		public Texture2D GetPathTexture(Path path)
		{
			// Returns path texture based on the path object's id.
			return pathTextures[path.TextureID];
		}
	}
}
