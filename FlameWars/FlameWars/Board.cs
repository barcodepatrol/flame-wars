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
		int boardHeight;// height of board
		int boardWidth;// width of board
		Color[] tints = new Color[3];
		Vector2 boardBounds = new Vector2();
		int vertLength = 7;// number of vertical squares
		int horiLength = 12;// number of horizontal squares

            // Constructor
		public Board()
		{
			track = new Path[34];

		}

            // Method
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
			// create random object
			Random rng = new Random();

			for (int i = 0; i < track.Length; i++)
			{
				// create position
				// if intervals set to handle each side of board
				if(i >=0 && i < 12)
				{
					Vector2 vec = new Vector2(i * SQUARE_WIDTH, (boardHeight / vertLength));// equations being worked out
				}
				if(i >= 12 && i < 18)
				{
					Vector2 vec = new Vector2((boardWidth / horiLength), i * SQUARE_HEIGHT);// see above comment
				}
				if(i >= 18 && i < 29)
				{
					Vector2 vec = new Vector2();
				}
				if(i >= 29 && i < 34)
				{
					Vector2 vec = new Vector2();
				}
				// create rectangle
				// select tint
				// select space type
			}
		}
    }
}
