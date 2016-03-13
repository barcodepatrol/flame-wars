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
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Instance Variables
		//* Instance variables *//
		private Vector2 pos; // Position. X and Y Co-ordinates.
		private Rectangle boundaries; // Bounds. X and Y are arbitrary. Width and Height.
		private Color tint; // DrawColor. Not everything will be drawn in white.
		private Board.SpaceType space; // The "type" of square the path will be.
		#endregion

		#region Properties
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
		{
			get { return this.boundaries; }
			set { this.boundaries = value; }
		}

		// Stores the X position.
		public int X
		{
			get { return (int) this.pos.X; }
			set { this.pos.X = value; }
		}

		// Stores the Y position.
		public int Y
		{
			get { return (int)this.pos.Y; }
			set { this.pos.Y = value; }
		}

		// Gets the current draw color for the path object.
		public Color DrawColor {
			get { return this.tint; }
			set { this.tint = value; }
		}
		#endregion

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		#region Constructors
		// Constructor
		public Path()
		{

		}
		#endregion

		#region Service Methods
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


		// Card Trigger
		/*
			Play a sound effect.
			Display a message to the user.
			Use the GameManager's static class to trigger the card drawing mechanism.
			Query player as to whether or not they will pay the "processing fee."
			Free but Random / Pay but Decline Options Pile
		*/

		// Resource Trigger
		/*
			Play a sound effect.
			Display a message to the user.
		*/


		// Bonus Trigger
		/*
			Play a sound effect.
			Display a message to the user.
		*/


		// Stock Trigger
		/*
			Play a sound effect.
			Display a message to the user.
		*/

		// Random Trigger
		/*
			This will randomly choose among different triggers to call a different one. 
			Picks from: Card, Resource, Bonus, Stock.
		*/

		// Empty
		/*
			Play a sound effect.
			Display a message to the user.
		*/

		#endregion
	}
}