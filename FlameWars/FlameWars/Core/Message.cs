using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars.Core
{
	class Message
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		// Used for drawing
		private Vector2 position; // Position. X and Y Co-ordinates.
		private Vector2 center; // Provides a center point for players to be drawn upon.
		private Rectangle boundaries; // Bounds. X and Y are arbitrary. Width and Height.
		private Color tint; // DrawColor. Not everything will be drawn in white.

		// Used for content
		private string message; // Contains the message to display
		private bool cancel = false;  // Determines if a cancel button is displayed

		#endregion

		#region Properties

		// Stores the X, Y position of the path object display, as float vectors.
		public Vector2 Position
		{
			get { return this.position; }
			set { this.position = value; }
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
			get { return (int) this.position.X; }
			set { this.position.X = value; }
		}

		// Stores the Y position.
		public int Y
		{
			get { return (int)this.position.Y; }
			set { this.position.Y = value; }
		}

		// Stores the center vector.
		public Vector2 Center
		{
			get { return this.center; }
			set { this.center = value; }
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

		// Constructor
		public Message(string message)
		{
			this.message = message;
		}

		// Constructor - Includes a change to the default cancel value
		public Message(string message, bool cancel)
		{
			this.message = message;
			this.cancel = cancel;
		}


	}
}
