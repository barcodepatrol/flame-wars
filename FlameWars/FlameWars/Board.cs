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
		const int SQUARE_WIDTH = 50;
		const int SQUARE_HEIGHT = 50;
		int boardHeight;
		int boardWidth;
		Color[] tints = new Color[3];
		Vector2 boardBounds = new Vector2();

		// Number of vertical squares
		// Number of horizontal squares
		int vertLength = 7;	
		int horiLength = 12;

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Board()
		{
			track = new Path[34];

		}

		public void Initialize()
		{

		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Draw(image);

		}

		public void CreateBoard()
		{
			// Create random object
			Random rng = new Random();

			for (int i = 0; i < track.Length; i++)
			{
				// Create position
				// If intervals set to handle each side of board

				#region CreatePosition
				// Bottom of the board
				if(i >=0 && i < 12)
				{
					// equations being worked out
					Vector2 vec = new Vector2(i * SQUARE_WIDTH, (boardHeight / vertLength));
				}
				// Right column of the board
				if(i >= 12 && i < 18)
				{
					// see above comment
					Vector2 vec = new Vector2((boardWidth / horiLength), i * SQUARE_HEIGHT);
				}
				// Top row of the board
				if(i >= 18 && i < 29)
				{
					Vector2 vec = new Vector2();
				}
				// Left column of the board
				if(i >= 29 && i < 34)
				{
					Vector2 vec = new Vector2();
				}
				#endregion CreatePosition

				// Create rectangle
				// Select tint
				// Select space type
			}
		}
	}
}
