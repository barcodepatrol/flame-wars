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

		private const int SQUARE_WIDTH  = 100;
		private const int SQUARE_HEIGHT = 100;
		private const int BOARD_HEIGHT  = 700;
		private const int BOARD_WIDTH   = 1200;

		private readonly Color RESOURCE_COLOR = Color.LightGreen;
		private readonly Color CARD_COLOR     = Color.Green;
		private readonly Color BONUS_COLOR    = Color.DarkGreen;
		private readonly Color STOCK_COLOR    = Color.LightBlue;
		private readonly Color RANDOM_COLOR   = Color.Blue;
		private readonly Color EMPTY_COLOR    = Color.DarkBlue;

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

		// Current path object.
		Path currentPath;

		// Number of vertical squares
		// Number of horizontal squares
		private const int VERTICAL_LENGTH = 7;	
		private const int HORIZONTAL_LENGTH = 12;

		//* Properties *//
		public Path CurrentPath
		{
			get { return this.currentPath; }
			set { this.currentPath = value; }
		}

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
			boardBounds	   = new Rectangle((GameManager.Width/2)-(BOARD_WIDTH/2), 
										   (GameManager.Height/2)-(BOARD_HEIGHT/2), 
											BOARD_WIDTH, BOARD_HEIGHT);
			pathBounds     = new Rectangle((GameManager.Width/2)-(BOARD_WIDTH/2), 
										   (GameManager.Height/2)-(BOARD_HEIGHT/2), 
											SQUARE_WIDTH, SQUARE_HEIGHT);
			random         = new Random();

			// Load the texture for all path objects
			this.pathTextures = pathImage;
			this.boardTexture = boardImage;

			// Create the tints
			CreateTints();
			
			// Generate the board
			CreateBoard();
		}

		// This method draws all of the path objects to the screen
		public void Draw(SpriteBatch spriteBatch)
		{
			// Draw the Board background
			spriteBatch.Draw(boardTexture, boardBounds, Color.White);
			
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
			for (int index = 0; index < track.Length; index++)
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
				if(index >= 0 && index < 12)
				{
					positionVector = new Vector2(index * SQUARE_WIDTH, 
									 (BOARD_HEIGHT - SQUARE_HEIGHT));
				}
				// Right column of the board
				if(index >= 12 && index < 17)
				{
					positionVector = new Vector2((BOARD_WIDTH - SQUARE_WIDTH), 
									 (BOARD_HEIGHT - ((index-10) * SQUARE_HEIGHT)));
				}
				// Top row of the board
				if(index >= 17 && index < 29)
				{
					positionVector = new Vector2(((12 * SQUARE_WIDTH) - (index-16)*SQUARE_WIDTH), 
									 (BOARD_HEIGHT / VERTICAL_LENGTH) - SQUARE_HEIGHT);
				}
				// Left column of the board
				if(index >= 29 && index < 34)
				{
					positionVector = new Vector2(0, (index-29) * SQUARE_HEIGHT + SQUARE_HEIGHT);
				}
				#endregion CreatePosition

				#region CreateRec,Type,Tint

				// Create the position Rectangle
				pathBounds = new Rectangle((int)positionVector.X + (GameManager.Width/2)-(BOARD_WIDTH/2), 
										   (int)positionVector.Y + (GameManager.Height/2)-(BOARD_HEIGHT/2), 
										   SQUARE_WIDTH, SQUARE_HEIGHT);

				// Select the path texture randomly
				int id = random.Next(0, pathTextures.Length);

				// Select space type
				// Enums cast to ints
				// Save an int from 0 to 5
				Array values   = Enum.GetValues(typeof(SpaceType));
				SpaceType spaceType = (SpaceType)values.GetValue(random.Next(values.Length));

				// Select the color tint
				// Color tint determined by SpaceType of path object
				Color tint = new Color();
				switch (spaceType)
				{
					case SpaceType.Resource:
						tint = tints[0];
						break;
					case SpaceType.Card:
						tint = tints[1];
						break;
					case SpaceType.Bonus:
						tint = tints[2];
						break;
					case SpaceType.Stock:
						tint = tints[3];
						break;
					case SpaceType.Random:
						tint = tints[4];
						break;
					case SpaceType.Empty:
						tint = tints[5];
						break;
				}

				#endregion CreateRec,Type,Tint

				// Create the Path Object
				// Save the data into the Path object
				Path pathSquare      = new Path(positionVector,	// Sets the position for the path square.
												pathBounds,		// Sets the boundaries for the path square.
												id,				// Sets the textureID for the path square.
												tint,			// Sets the draw color for the path square.
												spaceType,
												index);		// Sets the space type for the path square.

				// Add the Path Object to our current path array
				track[index] = pathSquare;
			}

			// Set the current path to the first in the index.
			currentPath = track[0];
		}

		// Initializes the color tints array
		public void CreateTints()
		{
			tints[0] = RESOURCE_COLOR;	// resource
			tints[1] = CARD_COLOR;		// card
			tints[2] = BONUS_COLOR;		// bonus
			tints[3] = STOCK_COLOR;		// stock
			tints[4] = RANDOM_COLOR;	// Random
			tints[5] = EMPTY_COLOR;		// empty
		}

		// Gets the texture for a path at a given interval.
		public Texture2D GetPathTexture(Path path)
		{
			// Returns path texture based on the path object's id.
			return pathTextures[path.TextureID];
		}

		// Gets the actual path object based on call to index.
		// This validates any number placed inside the parameter,
		// Providing a feasible solution toward invalid data.
		public Path GetPath(int index)
		{
			int minimum = 0;
			int maximum = track.Length;

			// Gets the value if index happens to be over the limit.
			// This gets the difference in value from the overhang.
			// An index of 5 with a track length of 2, for example,
			// Will give you an end result index of 3.

			// Think of this as a wrapping around form of array index searching.
			while (index < minimum)
			{
				index += Math.Abs(index);
			}

			while (index > (track.Length - 1))
			{
				index -= track.Length;
			}

			return track[index];
		}

		// Get the next path in the index.
		public Path GetNextPath()
		{
			// Minimum index.
			int minimum = 0;

			// Maximum index value.
			int maximum = track.Length;

			// Get the current path's index.
			int currentIndex = currentPath.ID;

			// Calculate the next path's index.
			int nextPath = currentIndex++;

			// If the path index for the next path is out of bounds, begin from the start once more.
			if (nextPath >= maximum)
			{
				nextPath = minimum; // Cycle back to the first instance of paths.
			}

			if (nextPath != minimum)
			{
				// Returns the track we are looking for.
				return track[nextPath];
			} else { 
				// In case of error OR In case of index simply equalling 0, return the default path.
				return track[0];
			}
		}

		// Get the previous path in the index.
		public Path GetPreviousPath()
		{
			// Minimum index.
			int minimum = 0;

			// Maximum index value.
			int maximum = track.Length;

			// Get the current path's index.
			int currentIndex = currentPath.ID;

			// Calculate the next path's index.
			int previousPath = currentIndex--;

			// If the path index for the next path is out of bounds, begin from the start once more.
			if (previousPath <= minimum)
			{
				previousPath = maximum; // Cycle back to the first instance of paths.
			}

			// 
			if (previousPath != minimum)
			{
				// Returns the track we are looking for.
				return track[previousPath];
			}
			else {
				// In case of error OR In case of index simply equalling 0, return the default path.
				return track[0];
			}
		}



	}
}
