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

		private int SQUARE_WIDTH = 100;
		private int SQUARE_HEIGHT = 100;
		private int BOARD_HEIGHT = 700;
		private int BOARD_WIDTH = 1200;

		// Color Palettes.
		// Palette name: 'I can see the Flickers' - { http://www.color-hex.com/color-palette/18650 }
		private static readonly Color[] palette_01 = new Color[]
											{
												new Color(246,84,106), // Resource Square
												new Color(70,132,153), // Card Square
												new Color(127,255,212), // Premium Square
												new Color(168,230,207), // Bonus Square.
												new Color(255,228,225), // Stock Square.
												new Color(215, 212, 203), // Random Square.
												new Color(215, 212, 203) // Empty.
											};

		// Palette name: 'Everything I Do Is Bittersweet' - { http://www.color-hex.com/color-palette/18647 }
		private static readonly Color[] palette_02 = new Color[]
											{
												new Color(255,148,230),
												new Color(255,118,160),
												new Color(214,255,126),
												new Color(195,251,0),
												new Color(157,251,0),
												new Color(215, 212, 203),
												new Color(215, 212, 203)
											};

		// Palette name: 'Pixel Pastel' - { http://www.color-hex.com/color-palette/18646 } 
		private static readonly Color[] palette_03 = new Color[]
											{
												new Color(240,173,204),
												new Color(231,148,177),
												new Color(214,128,128),
												new Color(227,99,99),
												new Color(227,46,46),
												new Color(215, 212, 203),
												new Color(215, 212, 203)
											};

		// Palette name: 'Are You Dead Or Are You Sleeping' - { http://www.color-hex.com/color-palette/18681 }
		private static readonly Color[] palette_04 = new Color[]
											{
												new Color(51,65,56),
												new Color(81,121,132),
												new Color(85,158,150),
												new Color(179,98,77),
												new Color(249,159,136),
												new Color(215, 212, 203),
												new Color(215, 212, 203)
											};

		// List of palletes.
		private static readonly Color[][] PALETTES = new Color[][]
											{
												palette_01, palette_02, palette_03, palette_04
											};

		// Color indexes.
		private const int RESOURCE_COLOR_INDEX = 0;
		private const int CARD_COLOR_INDEX = 1;
		private const int PREMIUM_COLOR_INDEX = 2;
		private const int BONUS_COLOR_INDEX = 3;
		private const int STOCK_COLOR_INDEX = 4;
		private const int RANDOM_COLOR_INDEX = 5;
		private const int EMPTY_COLOR_INDEX = 6;

		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables
		/****
			enum SpaceType - Indicators for path type.

			Resource - This path provides resources (users, memes, bandwidth, or money).
			Card - This path triggers a card drawing.
			BondReturn - This path triggers a bond return action.
			BondBuying - This path triggers a bond buying action.
			Random - This path triggers a random trigger().
			Empty - This path has no triggers. It's an empty, brick pavestone.

			****/

		// Enumerator.
		public enum SpaceType { Resource, Card, PremiumCard, BondReturn, BondBuying, Random, Empty };

		// Collections.
		Path[] track; // Array containing the board's path objects.
		Color[] tints; // Contains the tints for painting the objects.
		Texture2D[] resourceTextures; // Contains the different textures that can be drawn depending on path texture ID.
		Texture2D[] cardTextures; // Contains the different textures that can be drawn depending on path texture ID.
		Texture2D[] bondTextures; // Contains the different textures that can be drawn depending on path texture ID.
		Texture2D[] bondReturnTextures; // Contains the different textures that can be drawn depending on path texture ID.
		Texture2D[] emptyPathTextures; // Contains the different textures that can be drawn depending on path texture ID.
		Texture2D[] randomPathTextures; // Contains the different textures that can be drawn depending on path texture ID.

		// Textures.
		Texture2D boardTexture; // The board texture.

		// Boundaries.
		Rectangle boardBounds; // The board's boundaries and position.
		Rectangle pathBounds; // The path object's bounds and position.

		// Random.
		Random random;

		// Current path object.
		Path currentPath;

		// Current palette index.
		int paletteIndex;

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

		public Color[] CurrentPalette
		{
			get { return PALETTES[paletteIndex]; }
		}
		
		private Color RESOURCE_COLOR
		{
			get { return CurrentPalette[RESOURCE_COLOR_INDEX]; }
		}

		private Color CARD_COLOR
		{
			get { return CurrentPalette[CARD_COLOR_INDEX]; }
		}

		private Color PREMIUM_COLOR
		{
			get { return CurrentPalette[PREMIUM_COLOR_INDEX]; }
		}

		private Color BONUS_COLOR
		{
			get { return CurrentPalette[BONUS_COLOR_INDEX]; }
		}

		private Color STOCK_COLOR
		{
			get { return CurrentPalette[STOCK_COLOR_INDEX]; }
		}

		private Color RANDOM_COLOR
		{
			get { return CurrentPalette[RANDOM_COLOR_INDEX]; }
		}

		private Color EMPTY_COLOR
		{
			get { return CurrentPalette[EMPTY_COLOR_INDEX]; }
		}
		
		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Board(Texture2D[] resources, Texture2D[] cards, Texture2D[] bonds, Texture2D[] bondreclaims, Texture2D[] emptyPaths, Texture2D[] randomPaths, Texture2D boardImage)
		{

			track          = new Path[34];
			tints          = new Color[7];

			BOARD_WIDTH = (int)(BOARD_WIDTH * GameManager.ScreenScale);
			BOARD_HEIGHT = (int)(BOARD_HEIGHT * GameManager.ScreenScale);
			SQUARE_WIDTH = (int)(SQUARE_WIDTH * GameManager.ScreenScale);
			SQUARE_HEIGHT = (int)(SQUARE_HEIGHT * GameManager.ScreenScale);
			// 200, 200 are just starter values, this must be determined some other time
			boardBounds	   = new Rectangle((GameManager.Width/2)-(BOARD_WIDTH/2), 
										   (GameManager.Height/2)-(BOARD_HEIGHT/2), 
											BOARD_WIDTH, BOARD_HEIGHT);
			pathBounds     = new Rectangle((GameManager.Width/2)-(BOARD_WIDTH/2), 
										   (GameManager.Height/2)-(BOARD_HEIGHT/2), 
											SQUARE_WIDTH, SQUARE_HEIGHT);
			random         = new Random();

			// Load the texture for all path objects
			this.resourceTextures = resources;
			this.cardTextures = cards;
			this.bondTextures = bonds;
			this.bondReturnTextures = bondreclaims;
			this.emptyPathTextures = emptyPaths;
			this.randomPathTextures = randomPaths;
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
				spriteBatch.Draw(GetPathTexture(track[i]), track[i].Bounds, track[i].DrawColor);// path resizing
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
				int id = GameManager.RandomGen.Next(0, ArtManager.PATH_VARIATIONS);

				// Select space type
				// Enums cast to ints
				Array values   = Enum.GetValues(typeof(SpaceType));
				SpaceType spaceType = (SpaceType)values.GetValue(GameManager.RandomGen.Next(values.Length));

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
					case SpaceType.PremiumCard:
						tint = tints[2];
						break;
					case SpaceType.BondReturn:
						tint = tints[3];
						break;
					case SpaceType.BondBuying:
						tint = tints[4];
						break;
					case SpaceType.Random:
						tint = tints[5];
						break;
					case SpaceType.Empty:
						tint = tints[6];
						break;
				}

				#endregion CreateRec,Type,Tint

				// Create the Path Object
				// Save the data into the Path object
				Path pathSquare = new Path(positionVector,	// Sets the position for the path square.
												pathBounds,	// Sets the boundaries for the path square.
												id,			// Sets the textureID for the path square.
												tint,		// Sets the draw color for the path square.
												spaceType,  // Sets the space type for the path square.
												index);		

				// Add the Path Object to our current path array
				track[index] = pathSquare;
			}

			// Set the current path to the first in the index.
			currentPath = track[0];
		}

		// Initializes the color tints array
		public void CreateTints()
		{
			// Set up the proper references for the color array.
			paletteIndex = random.Next(0, PALETTES.Length); // Gets the palette for this game.

			tints[0] = RESOURCE_COLOR;	// resource
			tints[1] = CARD_COLOR;		// card
			tints[2] = PREMIUM_COLOR;	// premium card
			tints[3] = BONUS_COLOR;		// bonus
			tints[4] = STOCK_COLOR;		// stock
			tints[5] = RANDOM_COLOR;	// Random
			tints[6] = EMPTY_COLOR;		// empty
		}

		// Gets the texture for a path at a given interval.
		public Texture2D GetPathTexture(Path path)
		{
			switch (path.Space)
			{
				case SpaceType.Resource:
					return resourceTextures[path.TextureID];
				case SpaceType.Card:
					return cardTextures[path.TextureID];
				case SpaceType.PremiumCard:
					return cardTextures[path.TextureID];
				case SpaceType.BondReturn:
					return bondReturnTextures[path.TextureID];
				case SpaceType.BondBuying:
					return bondTextures[path.TextureID];
				case SpaceType.Random:
					return randomPathTextures[path.TextureID];
				case SpaceType.Empty:
					return emptyPathTextures[path.TextureID];
			}

			// Returns path texture based on the path object's id.
			return resourceTextures[path.TextureID];
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
			}
			else
			{ 
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
