using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	public class Path
	{
		// Instance variables
		private Vector2 pos;
		private Rectangle boundaries;
		private Color tint;
		private Board.SpaceType space;

		// Properties
		public Board.SpaceType Space { get { this.space} }
		public Vector2 Position { get { } set { } }
		public Rectangle Bounds { get { } set { } }
		public int X { get { } set { } }
		public int Y { get { } set { } }
		public Color Tint { get { } set { } }

		// Constructor
		Path() { }

		// Method
		public void Trigger()
		{
			switch(space) {
				case Board.SpaceType.Card:
					break;
				case Board.SpaceType.Resource:
					break;
				case Board.SpaceType.Bonus:
					break;
				case Board.SpaceType.Stock:
					break;
				case Board.SpaceType.Random:
					break;
				case Board.SpaceType.Empty:
					break;
			}
		}
	}
}