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
		/*
			enum SpaceType - Indicators for path type.

			Resource - This path provides resources (users, memes, bandwitdh, or money).
			Card - This path triggers a card drawing.
			Bonus - This path triggers a bonus for the player.
			Stock - This path triggers a stock market mechanic.
			Random - This path triggers the stock 

			*/

        public enum SpaceType { Resource, Card, Bonus, Stock, Random, Empty };

		Path[] track;
		Texture2D image;

            // Constructor
		public Board()
		{
			track = new Path[35];

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

        public void AddPath(Path pth)
        {
            // Extend the array.
            // Add square.
        }
    }
}
