﻿using System;
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

		#region Variables

		private Vector2 position; // Position. X and Y Co-ordinates.
		private Vector2 center; // Provides a center point for players to be drawn upon.
		private Rectangle boundaries; // Bounds. X and Y are arbitrary. Width and Height.
		private Color tint; // DrawColor. Not everything will be drawn in white.
		private Board.SpaceType space; // The "type" of square the path will be.
		private int textureID; // The ID for the type of texture the path will receive. Zero-based!
		private int pathIndex; // The actual index for the path.

		// Used for random path triggers
		private Random rnd;

		#endregion

		#region Properties
		// Stores the "Type" of square the path object is.
		public Board.SpaceType Space
		{
			get { return this.space; }
			set { this.space = value; }
		}

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

		// Gets the current texture ID.
		public int TextureID
		{
			get { return this.textureID; }
			set { this.textureID = value; }
		}

		// Gets the index of the current path.
		public int ID
		{
			get { return this.pathIndex; }
			set { this.pathIndex = value; }
		}

		#endregion

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		#region Constructors

		public Path(Vector2 pos, Rectangle bounds, int id, Color tint, Board.SpaceType type, int index)
		{
			TextureID = id;
			DrawColor = tint;
			Position  = pos;
			Bounds    = bounds;
			Space     = type;
			ID        = index;
			Center    = GameManager.GetElementCenterPoint(bounds.X, bounds.Y, bounds.Width, bounds.Height);
			rnd       = new Random();
		}

		#endregion

		#region Service Methods

		/*
			The Trigger() method is called by a Path whenever a player lands on it.
			Whenever trigger is called, based on the path type, a certain method will be called.
		*/
		public void Trigger()
		{
			switch(space) {
				case Board.SpaceType.Card:
					CardTrigger();
					break;
				case Board.SpaceType.PremiumCard:
					PremiumCardTrigger();
					break;
				case Board.SpaceType.Resource:
					ResourceTrigger();
					break;
				case Board.SpaceType.BondReturn:
					BondReturnTrigger();
					break;
				case Board.SpaceType.BondBuying:
					BondBuyingTrigger();
					break;
				case Board.SpaceType.Random:
					RandomTrigger();
					break;
				case Board.SpaceType.Empty:
					EmptyTrigger();
					break;
			}
		}

		// Determines if this is the path requested based on parameter index.
		public bool IsPath(int index)
		{
			return (ID == index);
		}
				
		// Card Trigger
		/*
			Play a sound effect.
			Display a message to the user.
			Use the GameManager's static class to trigger the card drawing mechanism.
			Query player as to whether or not they will pay the "processing fee."
			Free but Random / Pay but Decline Options Pile
		*/
		public void CardTrigger()
		{
			// Play a sound effect

			// Create card message by passing in next card from deck
			Message.Activate();
			Message.CreateMessage(GameManager.GetCard());
		}

		public void PremiumCardTrigger()
		{
			// Create card message by passing in next premium card from deck
			Message.Activate();
			Message.CreateMessage(GameManager.GetPremiumCard());
		}

		// Resource Trigger
		/*
			Play a sound effect.
			Display a message to the user.
		*/
		public void ResourceTrigger()
		{
			int rsc = GameManager.RandomGen.Next(4);// determine which resource is changed

			switch(rsc)
			{
				case 0: GameManager.CurrentPlayer.Money += 100;
					Message.Activate();
					Message.CreateMessage("You have received $100");
					break;
				case 1: GameManager.CurrentPlayer.Users += 20;
					Message.Activate();
					Message.CreateMessage("You have received 20 users");
					break;
				case 2: GameManager.CurrentPlayer.Memes += 5;
					Message.Activate();
					Message.CreateMessage("You have received 5 memes");
					break;
				case 3: GameManager.CurrentPlayer.Bandwidth += 5;
					Message.Activate();
					Message.CreateMessage("You have received 5 bandwidth");
					break;
			}
		}

		// BondReturn Trigger
		/*
			Play a sound effect.
			Display a message to the user.
		*/
		public void BondReturnTrigger()
		{
			// Check if player has bonds
			if (GameManager.CurrentPlayer.Bonds.Count > 0)
			{
				int totalRevenue = 0;
				// Add the value of each bond
				foreach (Bond b in GameManager.CurrentPlayer.Bonds)
				{
					GameManager.CurrentPlayer.Money += b.GenerateRevenue();
					totalRevenue += b.GenerateRevenue();
				}

				Message.Activate();
				Message.CreateMessage(GameManager.CurrentPlayer.Bonds.Count + " bond(s) cashed in.\n $" + totalRevenue + " received.");

				// Clear bonds
				GameManager.CurrentPlayer.Bonds.Clear();
			}
			else
			{
				Message.Activate();
				Message.CreateMessage("No Bonds Returned");
			}
		}

		// BondBuying Trigger
		/*
			Play a sound effect.
			Display a message to the user.
		*/
		public void BondBuyingTrigger()
		{
			// Create a bond
			Bond bond = new Bond();

			// Ask player if they want to buy Bond
			Message.Activate();
			Message.CreateMessage(bond);
		}

		// Random Trigger
		/*
			This will randomly choose among different triggers to call a different one. 
			Picks from: Card, Resource, Bonus, Stock.
		*/
		public void RandomTrigger()
		{
			// Select a random space type
			int select = rnd.Next(0, 5);

			// Trigger based off of spacetype
			switch ((Board.SpaceType)select)
			{
				case Board.SpaceType.Card:
					CardTrigger();
					break;
				case Board.SpaceType.Resource:
					ResourceTrigger();
					break;
				case Board.SpaceType.BondReturn:
					BondReturnTrigger();
					break;
				case Board.SpaceType.BondBuying:
					BondBuyingTrigger();
					break;
				default:
					EmptyTrigger();
					break;
			}
		}

		// Empty
		/*
			Play a sound effect.
			Display a message to the user.
		*/
		public void EmptyTrigger()
		{
			// Play a sound effect

			// Create message
			Message.Activate();
			Message.CreateMessage("NOTHING HAPPENS!");
		}

		#endregion
	}
}