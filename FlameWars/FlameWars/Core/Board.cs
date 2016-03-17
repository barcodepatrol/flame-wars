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
		Texture2D back;
		const int SQUARE_WIDTH  = 100;
		const int SQUARE_HEIGHT = 100;
		const int BOARD_HEIGHT  = 700;
		const int BOARD_WIDTH   = 1200;
		Color[] tints;
		Rectangle boardPos;
		Random rng;

		// Number of vertical squares
		// Number of horizontal squares
		int vertLength = 7;	
		int horiLength = 12;

		#endregion Variables

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Board(Texture2D pimage, Texture2D bimage)
		{
			track       = new Path[34];
			tints       = new Color[6];
			// 200, 200 are just starter values, this must be determined some other time
			boardPos	= new Rectangle(200, 200, BOARD_WIDTH, BOARD_HEIGHT);
			rng         = new Random();

			// Load the texture for all path objects
			this.image = pimage;
			back = bimage;

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
			spriteBatch.Draw(back, new Rectangle(0, 0, back.Width, back.Height), Color.White);
			// Iterate through all path objects
			for (int i = 0; i < track.Length; i++)
			{
				spriteBatch.Draw(image, track[i].Bounds, track[i].DrawColor);
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
				Vector2 vec = new Vector2();

				// Bottom of the board
				if(i >= 0 && i < 12)
				{
					vec = new Vector2(i * SQUARE_WIDTH, 
									 (BOARD_HEIGHT - SQUARE_HEIGHT));
				}
				// Right column of the board
				if(i >= 12 && i < 17)
				{
					vec = new Vector2((BOARD_WIDTH - SQUARE_WIDTH), 
									 (BOARD_HEIGHT - ((i-10) * SQUARE_HEIGHT)));
				}
				// Top row of the board
				if(i >= 17 && i < 29)
				{
					vec = new Vector2(((12 * SQUARE_WIDTH) - (i-16)*SQUARE_WIDTH), 
									 (BOARD_HEIGHT / vertLength) - SQUARE_HEIGHT);
				}
				// Left column of the board
				if(i >= 29 && i < 34)
				{
					vec = new Vector2(0, (i-29) * SQUARE_HEIGHT + SQUARE_HEIGHT);
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
				Path p      = new Path();
				p.DrawColor = tint;
				p.Position  = vec;
				p.Bounds    = boardPos;
				p.Space     = type;

				// Add the Path Object to our current path array
				track[i] = p;
			}
		}

		public void CreateTints()
		{
			tints[0] = Color.Green;// resource
			tints[1] = Color.Red;// card
			tints[2] = Color.Blue;// bonus
			tints[3] = Color.Orange;// stock
			tints[4] = Color.Yellow;// Random
			tints[5] = Color.White;// empty
		}
	}
}
