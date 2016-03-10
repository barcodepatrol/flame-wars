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
			//* Instance variables *//
		private Vector2 pos; // Position. X and Y Co-ordinates.
		private Rectangle boundaries; // Bounds. X and Y are arbitrary. Width and Height.
		private Color tint; // DrawColor. Not everything will be drawn in white.
		private Board.SpaceType space; // The "type" of square the path will be.

			//* Properties *//
		// Stores the "Type" of square the path object is.
		public Board.SpaceType Space
		{
			get { return this.space; }
			set { this.space = value; }
		}

		// Stores the X, Y position of the path object display, as float vectors.
		public Vector2 Position
		{
			get { return this.pos; }
			set { this.pos = value; }
		}

		// Stores the Width and Height of the path object.
		public Rectangle Bounds
		{ get; set; }

		public int X
		{
			get { return (int) this.pos.X; }
			set { this.Position.X = new Vector2(value, Position.Y); }
		}

		public int Y
		{ get; set; }

		// Gets the current draw color for the path object.
		public Color DrawColor {
			get;
			set;
		}

		// Constructor
		public Path()
		{

		}

		// Service Methods

		/*
			The Trigger() method is called by a Path whenever a player lands on it.
			Whenever trigger is called, based on the path type, a certain method will be called.
		*/
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