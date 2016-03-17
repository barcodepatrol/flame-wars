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

		public enum SpaceType { Resource, Card, Bonus, Stock, Random, Empty };

		Path[] track;
		Texture2D image;
		const int SQUARE_WIDTH  = 50;
		const int SQUARE_HEIGHT = 50;
		const int BOARD_HEIGHT  = 250;
		const int BOARD_WIDTH   = 600;
		Color[] tints;
		Rectangle boardPos;
		Random rng;

		// Number of vertical squares
		// Number of horizontal squares
		int vertLength = 5;	
		int horiLength = 12;

		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Board()
		{
			track       = new Path[34];
			tints       = new Color[3];
			// 200, 200 are just starter values, this must be determined some other time
			boardPos	= new Rectangle(200, 200, BOARD_WIDTH, BOARD_HEIGHT);
			rng         = new Random();
			CreateBoard();
		}

		// EDIT: Do we even need an update function here?
		public void Update(GameTime gameTime)
		{

		}

		// This method draws all of the path objects to the screen
		public void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Draw(image);

		}

		// This method generates Path objects to fill the Track Array
		public void CreateBoard()
		{
			// Fill the entire track array
			for (int i = 1; i <= track.Length; i++)
			{
				// Create position
				// If intervals set to handle each side of board

				// TESTING: Necessary testing for the equations
				// I spent some time thinking about them and I believe they are correct
				// (more or less) but we still need to test them

				#region CreatePosition
				
				// Initialize a position vector
				Vector2 vec = new Vector2();

				// Bottom of the board
				if(i > 0 && i <= 12)
				{
					vec = new Vector2(i * SQUARE_WIDTH, (BOARD_HEIGHT - (BOARD_HEIGHT / vertLength)));
				}
				// Right column of the board
				if(i > 12 && i <= 17)
				{
					vec = new Vector2((BOARD_WIDTH - (BOARD_WIDTH / horiLength)), (SQUARE_HEIGHT - (i * SQUARE_HEIGHT)));
				}
				// Top row of the board
				if(i > 17 && i <= 29)
				{
					vec = new Vector2(((12 * SQUARE_WIDTH) - (i-17)*SQUARE_WIDTH), (BOARD_HEIGHT / vertLength));
				}
				// Left column of the board
				if(i > 29 && i <= 34)
				{
					vec = new Vector2((BOARD_WIDTH / horiLength), i * SQUARE_HEIGHT);
				}
				#endregion CreatePosition

				#region CreateRec,Tint,Type

				// Create the position Rectangle
				boardPos = new Rectangle((int)vec.X, (int)vec.Y, SQUARE_WIDTH, SQUARE_HEIGHT);

				// Select the tint randomly
				Color tint = tints[rng.Next(0, tints.Length)];

				// Select space type
				// Enums cast to ints
				// Save an int from 0 to 5
				Array values   = Enum.GetValues(typeof(SpaceType));
				SpaceType type = (SpaceType)values.GetValue(rng.Next(values.Length));

				#endregion CreateRec,Tint,Type

				// Create the Path Object
				// Save the data into the Path object
				Path p     = new Path();
				p.Position = vec;
				p.Bounds   = boardPos;
				p.Space    = type;

				// Add the Path Object to our current path array
				track[i-1] = p;
			}
		}
	}
}
