﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	class Player
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables

		// Enumerator.
		public enum Role
		{
			TopHat,
			Plastic,
			Narcissist,
			Dankest
		}
		public static List<Role> roles = new List<Role>();

		// Enumerator.
		public enum AnimationState { Idle, Roll, Animate };
		public enum Direction { North, South, East, West };

		// Textures.
		private Texture2D rollButton; // UI RollButton texture for the player.
		private Color[] rollButtonColors; // RollButton color for the player.
		private Rectangle rollButtonBounds; // Bounds for the rollButton.
		private Texture2D iconTexture;  // UI Icon for the player.
		private Rectangle iconBounds;   // Display position for the player's UI icon.
		private Texture2D tokenTexture; // Actual texture for the player's token.
		private Rectangle tokenBounds;  // Display position for the player's token.
		private AnimationState animationState; // The current animation state.
		private Direction currentDirection; // The current direction we are moving in.
		private const double MOVEMENT_AMOUNT = 1; // The amount we move by when we animate.

		// Vectors and Rectangles
		private Rectangle tokenPosition = new Rectangle(0,0,0,0);
		private Vector2 uiPosition = new Vector2(0,0);

		private Role role; // The role of the player.
		private int boardPosition       = 0;   // The position the player has on the board.
		private int nextPosition        = 0;   // The position the player must move to.
		private int finalPosition		= 0;   // The position the player will finish moving at.
		private double movedAmount      = 0;   // The amount we have moved so far during animation.
		private int money               = 0;   // The capital a given player has.
		private int users               = 0;   // The number of users the player has.
		private int memes               = 0;   // The number of memes the player can use.
		private int bandwidth           = 0;   // The bandwidth amount the player owns.
		private int bandwidthPercentage = 0; // The percentage of bandwidth the player can utilize.
		private int malice              = 0;   // The malice amount the player has accrued.
		private int charity             = 0;   // The charity amount the player has accrued.
		private int baseRate            = 1;  // base rate at which users are accrued
		private bool buttonActive   = false; // Is the roll button currently interactable?
		private bool buttonPressed  = false;
		private bool buttonReleased = false;
		private bool buttonHover    = false;
		private bool currentPlayer  = false;
		private int[] DrawYPositions; // The Y positions for UI icons.

		

		#endregion Variables

		#region Properties

		// Stores the 2D texture for the player's icon
		public Texture2D Icon
		{
			get { return this.iconTexture; }
			set { this.iconTexture = value; }
		}

		// Stores the Vector2 for the player's UI.
		public Vector2 UIPosition
		{
			get { return this.uiPosition; }
			set { this.uiPosition = value; }
		}

		// Stores the 2D texture for the player's board piece
		public Texture2D Token
		{
			get { return this.tokenTexture; }
			set { this.tokenTexture = value; }
		}

		// Stores the Vector2 for the player's token.
		public Rectangle TokenPosition
		{
			get { return this.tokenPosition; }
			set { this.tokenPosition = value; }
		}

		// Stores the rectangle of the player's position on the screen
		public Rectangle Bounds
		{
			get { return this.tokenBounds; }
			set { this.tokenBounds = value; }
		}

		// Stores the int value that evaluates to board position
		public Role PlayerRole
		{
			get { return this.role; }
			set { this.role = value; }
		}

		// Stores the int value that evaluates to board position
		public int BoardPosition
		{
			get { return this.boardPosition; }
			set { this.boardPosition = value; }
		}

		// Stores the int value that evaluates to the position the player must move to.
		public int NextPosition
		{
			get { return this.nextPosition; }
			set { this.nextPosition = value; }
		}

		// Stores the index of the final position
		public int FinalPosition
		{
			get { return finalPosition; }
			set { finalPosition = value; }
		}

		// Stores the int value for the player's money
		public int Money
		{
			get { return this.money; }
			set { this.money = value; }
		}

		// Stores the int value for the player's users
		public int Users
		{
			get { return this.users; }
			set
			{
				if (this.users - value < 0) this.users = 0;
				else this.users = value;
			}
		}

		// Stores the int value for the player's memes
		public int Memes
		{
			get { return this.memes; }
			set { this.memes = value; }
		}

		// Stores the int value for the player's bandwidth amount
		public int Bandwidth
		{
			get { return this.bandwidth; }
			set { this.bandwidth = value; }
		}

		// Stores the int value for the player's bandwidth percentage
		public int BandwidthPercentage
		{
			get { return this.bandwidthPercentage; }
			set { this.bandwidthPercentage = value; }
		}

		// Stores the int value for the player's malice
		public int Malice
		{
			get { return this.malice; }
			set { this.malice = value; }
		}

		// Stores the int value for the player's charity
		public int Charity
		{
			get { return this.charity; }
			set { this.charity = value; }
		}

		// Stores the bool value for the player's button active property.
		public bool IsButtonActive
		{
			get { return this.buttonActive; }
			set { this.buttonActive = value; }
		}
		
		// Stores the active color
		public Color ActiveColor
		{
			get { return this.rollButtonColors[1]; }
			set { this.rollButtonColors[1] = value; }
		}

		// Stores the inactive color
		public Color InactiveColor
		{
			get { return this.rollButtonColors[0]; }
			set { this.rollButtonColors[0] = value; }
		}

		public Color PressedColor
		{
			get { return this.rollButtonColors[2]; }
			set { this.rollButtonColors[2] = value; }
		}

		public bool IsCurrentPlayer
		{
			get { return this.currentPlayer; }
			set { this.currentPlayer = value; }
		}
		
		// Animationstate is the name of the enum and thus cannot be used as the name of the property
		public AnimationState AnimState 
		{
			get { return animationState; }
			set { animationState = value; }
		}

		public Direction CurrentDirection
		{
			get { return currentDirection; }
			set { currentDirection = value; }
		}
		#endregion

		#region Constants

		// Endgame conditions.
		private const int TURN_LIMIT = 10;
		private const int WEALTH_LIMIT = 100000;
		private const int USER_LIMIT = 100000;
		private const int MEME_LIMIT = 100000;

		#endregion

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor
		public Player(Vector2 ui, int currentPathIndex)
		{
			Initialize(currentPathIndex,ui);
		}

		public Player(Vector2 ui)
		{
			Initialize(0,ui);
		}

		// Initialize the Player.
		public void Initialize(int currentPathIndex, Vector2 ui)
		{
			//initialize player
			UIPosition = ui;
			tokenBounds = new Rectangle();

			// Set the initial path index to zero.
			BoardPosition = 0;
			rollButtonColors = new Color[3];

			ActiveColor   = Color.White;
			InactiveColor = Color.DarkGray;
			PressedColor  = Color.Gray;

			role = roles[GameManager.random.Next(roles.Count)];
			roles.Remove(role);
		}
		
		// Determines how many users the player gets
		public void GenerateUsers(int totBandwidth)
		{
			// Memes increase the amount of users you get each round
			// Bandwidth determines the upper and lower bounds of how many users you gain/lose
			bandwidthPercentage = (int)(bandwidth / (totBandwidth + .01));
			int userCap = bandwidth * 100;

			// Memes increase your users by an exponential addition
			int memeAddicts = (memes / 20) ^ 2;

			users += baseRate + (memeAddicts + bandwidthPercentage * ((userCap - users)));
		}

		// Update function.
		public void Update(GameTime gameTime)
		{
			// Decide the active state of the roll button.
			// Roll button is active when:
			// It is the player's turn.
			// The player hasn't yet rolled.
			// The player is required to 

			// Three states: idleState, rollState, animationState
			switch (animationState)
			{
				case AnimationState.Idle:
					Idle(gameTime);
					break;
				case AnimationState.Roll:
					Roll(gameTime);
					break;
				case AnimationState.Animate:
					UpdateAnimation();
					break;
			}
		}

		public void Idle(GameTime gameTime)
		{
			// Should do nothing.

			// May change in the future.

		}

		public void Roll(GameTime gameTime)
		{
			// FOR TESTING PURPOSES ONLY
			//Message.Activate();
			//Message.CreateMessage(GameManager.GetCard());

			//AnimState = AnimationState.Idle;

			// Set nextPosition to be one square ahead
			// Set finalPosition to be 
			NextPosition = BoardPosition + 1;
			finalPosition = BoardPosition + Dice.Roll(1);

			// Wrap final position
			if (finalPosition > 33)
				finalPosition -= 34;
		}

		// This method merely updates the player's position (animation)
		public void UpdateAnimation()
		{
			// Move based off of current direction
			switch (CurrentDirection)
			{
				case Direction.North:
					TokenPosition = new Rectangle(TokenPosition.X, 
												  (int)(TokenPosition.Y-MOVEMENT_AMOUNT), 
												  TokenPosition.Width, 
												  TokenPosition.Height);
					break;
				case Direction.South:
					TokenPosition = new Rectangle(TokenPosition.X, 
												  (int)(TokenPosition.Y+MOVEMENT_AMOUNT), 
												  TokenPosition.Width, 
												  TokenPosition.Height);
					break;
				case Direction.East:
					TokenPosition = new Rectangle((int)(TokenPosition.X+MOVEMENT_AMOUNT), 
												  TokenPosition.Y, 
												  TokenPosition.Width, 
												  TokenPosition.Height);
					break;
				case Direction.West:
					TokenPosition = new Rectangle((int)(TokenPosition.X-MOVEMENT_AMOUNT), 
												  TokenPosition.Y, 
												  TokenPosition.Width, 
												  TokenPosition.Height);
					break;
			}

			// Increment movedAmount
			movedAmount += MOVEMENT_AMOUNT;

			// Update movedAmount and check if our position has changed
			if (movedAmount >= 100)
			{
				movedAmount = 0;
				BoardPosition = NextPosition;
				NextPosition++;
				if (NextPosition == 34)
					NextPosition = 0;
				UpdateDirection();
			}
		}

		// This method changes the direction for the player
		public void UpdateDirection()
		{
			// East
			if (NextPosition > 0 && NextPosition < 12)
			{
				CurrentDirection = Direction.East;
			}
			// North
			else if (NextPosition >= 12 && NextPosition <= 17)
			{
				CurrentDirection = Direction.North;
			}
			// West
			else if (NextPosition > 17 && NextPosition < 29)
			{
				CurrentDirection = Direction.West;
			}
			// South
			else if ((NextPosition >= 29 && NextPosition <= 33) || NextPosition == 0)
			{
				CurrentDirection = Direction.South;
			}
		}

		// Trigger to get the Player into the Is rolling state. 
		public void StartRolling()
		{
			animationState = AnimationState.Roll;
		}

		public void StartAnimation()
		{
			animationState = AnimationState.Animate;
			UpdateDirection();
		}

		public void EndAnimation()
		{
			animationState = AnimationState.Idle;
		}

		// Initializes the players turn
		public void Start()
		{
			animationState = AnimationState.Idle;
			IsButtonActive = true;
			IsCurrentPlayer = true;
		}

		// Ends the player's turn
		public void End()
		{
			animationState = AnimationState.Idle;
			IsButtonActive = false;
			IsCurrentPlayer = false;
		}

		// Check to see if the player is currently Idle.
		public bool IsIdle()
		{
			return (animationState == AnimationState.Idle);
		}

		// Check to see if the player is currently Rolling.
		public bool IsRolling()
		{
			return (animationState == AnimationState.Roll);
		}

		// Check to see if the player is currently being Animated.
		public bool IsAnimated()
		{
			return (animationState == AnimationState.Animate);
		}

		// Check to see if the button is currently active.
		public bool IsRollButtonActive()
		{
			if (IsIdle())
			{
				return IsButtonActive;
			}
			else
			{
				return false;
			}
		}

		// Create the roll button in the Player class.
		public void MakeButton(Texture2D buttonTexture, int buttonWidth, int buttonHeight)
		{
			CalculateDrawYPositions();
			int xOrigin = (int)(UIPosition.X + buttonWidth / 3);
			int yOrigin = DrawYPositions[DrawYPositions.Length - 1];

			rollButton = buttonTexture;
			rollButtonBounds = new Rectangle(xOrigin, yOrigin, buttonWidth, buttonHeight);
		}

		// Get the button's current color.
		public Color GetButtonColor()
		{
			if (!IsRollButtonActive())
			{
				return InactiveColor;
			}
			else
			{
				if (buttonHover)
				{
					if (buttonPressed)
					{
						return PressedColor;
					}
					else if (buttonReleased)
					{
						return ActiveColor;
					}
					else
					{
						return InactiveColor;
					}
				}
				else
				{
					return ActiveColor;
				}
			}
		}

		// Get the button's current bounds.
		public Rectangle GetButtonBounds()
		{
			// The input parameter is the difference between the current
			// drawY value in the spriteBatch and the Icon height used to
			// offset things. We handle the removal of Icon.Height
			// and replace it with our own buttonHeight instead.
			int yPosition = DrawYPositions[DrawYPositions.Length - 1];
			int xOrigin = rollButtonBounds.X;
			int width = rollButtonBounds.Width;
			int height = rollButtonBounds.Height;

			return new Rectangle(xOrigin, yPosition, width, height);
		}

		// Get the button's hover state.
		public void Hover(int mX, int mY)
		{
			// Check to see if in roll state.
			if (!IsIdle())
			{
				buttonHover = false;
				buttonPressed = false;
				buttonReleased = false;
			}
			else
			{
				// Get the dimensions.
				Rectangle buttonBounds = GetButtonBounds();

				// If the mouse x and mouse y values are within the rectangle
				if (buttonBounds.X <= mX && mX <= buttonBounds.X + buttonBounds.Width &&
					buttonBounds.Y <= mY && mY <= buttonBounds.Y + buttonBounds.Height)
				{
					buttonHover = true;
				}
				// Otherwise, reset the color
				else
				{
					buttonHover = false;
				}
			}
		}

		// Get the button's pressed state.
		public void Pressed(int mX, int mY)
		{
			// Check to see if in roll state.
			if (!IsIdle())
			{
				buttonHover = false;
				buttonPressed = false;
				buttonReleased = false;
			}
			else
			{
				// Get the dimensions.
				Rectangle buttonBounds = GetButtonBounds();

				// If the mouse x and mouse y values are within the rectangle
				if (buttonBounds.X <= mX && mX <= buttonBounds.X + buttonBounds.Width &&
					buttonBounds.Y <= mY && mY <= buttonBounds.Y + buttonBounds.Height)
				{
					buttonPressed = true;
				}
				// Otherwise, reset the color
				else
				{
					buttonPressed = false;
					buttonHover = false;
				}
			}
		}

		// Get the button's released state.
		public void Released(int mX, int mY)
		{
			// Check to see if in roll state.
			if (!IsIdle())
			{
				buttonHover = false;
				buttonPressed = false;
				buttonReleased = false;
			}
			else
			{
				// Get the dimensions.
				Rectangle buttonBounds = GetButtonBounds();

				// If the mouse x and mouse y values are within the rectangle
				// If the button has already been pressed
				if (buttonBounds.X <= mX && mX <= buttonBounds.X + buttonBounds.Width &&
					buttonBounds.Y <= mY && mY <= buttonBounds.Y + buttonBounds.Height &&
					GetButtonColor() == PressedColor)
				{
					buttonHover = true;
					buttonPressed = false;
					buttonReleased = true;
					if (Message.isActive == false) StartRolling();
				}
				// Otherwise, reset the color
				else
				{
					buttonReleased = false;
					buttonHover = false;
				}
			}
		}

		// Draws the player token on the board
		public void DrawToken(SpriteBatch sb)
		{
			if (IsCurrentPlayer)
			{
				sb.Draw(tokenTexture, tokenPosition, Color.White);
			}
			else
			{
				sb.Draw(tokenTexture, tokenPosition, Color.DarkGray);
			}
		}

		// Draws the UI portion for the player
		// Parameter: The initial x and y position to start from
		public void DrawUI(SpriteBatch sb)
		{
			// Get the int values for ix, iy.
			int ix = (int)UIPosition.X;
			int iy = (int)UIPosition.Y;
			
			// Draw the icon
			sb.Draw(iconTexture, new Rectangle(ix, iy, Icon.Width, Icon.Height), Color.White);

			// Draw the stats below
			Object[] objectsToDraw = new object[] { money, users, memes, bandwidth, Malice, Charity, rollButton }; // Place all drawn elements here.

			for (int index = 0; index < objectsToDraw.Length; index++)
			{
				// Check to see if it is a Texture2D. If it is, then it is the button.
				if (objectsToDraw[index] is Texture2D)
				{
					sb.Draw((Texture2D)objectsToDraw[index], GetButtonBounds(), GetButtonColor());
				}
				else
				// If it is not a Texture2D, then it is an integer value.
				{
					string stringToPrint = "";
					int value = (int) objectsToDraw[index];

					switch (index)
					{
						case 0:
							// Money.
							stringToPrint = "Money: ";
							break;
						case 1:
							// Users.
							stringToPrint = "Users: ";
							break;
						case 2:
							// Memes.
							stringToPrint = "Memes: ";
							break;
						case 3:
							// Bandwidth.
							stringToPrint = "Bandwidth: ";
							break;
						case 4:
							// Malice.
							stringToPrint = "Malice: ";
							break;
						case 5:
							// Charity.
							stringToPrint = "Charity: ";
							break;
					}

					sb.DrawString(ArtManager.StellarLightFont, stringToPrint + value, new Vector2(ix, DrawYPositions[index]), Color.Black);
				}
			}

			/*
				sb.DrawString(ArtManager.BrownieFont, "Money: " + money, new Vector2(ix, iy+Icon.Height+10), Color.Black);
			
				sb.DrawString(ArtManager.BrownieFont, "Users: " + users, new Vector2(ix, iy+Icon.Height+30), Color.Black);

				sb.DrawString(ArtManager.BrownieFont, "Memes: " + memes, new Vector2(ix, iy+Icon.Height+50), Color.Black);

				sb.DrawString(ArtManager.BrownieFont, "Bandwidth: " + bandwidthAmount, new Vector2(ix, iy+Icon.Height+70), Color.Black);

				sb.DrawString(ArtManager.BrownieFont, "Malice: " + Malice, new Vector2(ix, iy+Icon.Height+90), Color.Black);

				sb.DrawString(ArtManager.BrownieFont, "Charity: " + Charity, new Vector2(ix, iy+Icon.Height+110), Color.Black);
			*/
		}

		// Calculuate DrawY positions
		public void CalculateDrawYPositions()
		{
			// Get the int values for ix, iy.
			int ix = (int)UIPosition.X;
			int iy = (int)UIPosition.Y;

			int drawY = iy;
			int baseIncrement = 10; // 10 px base increment.
			int amountToIncrement = 20; // 20 px increment.

			// Calculate the values necessary based on the number in the pool.
			int placeholderMoney = 0;
			int placeholderUsers = 1;
			int placeholderMemes = 2;
			int placeholderBandwidthAmount = 3;
			int placeholderMalice = 4;
			int placeholderCharity = 5;
			int placeholderRollButtonTexture = 6;
		
			// Using integers as placeholders allows us to calculate position values
			// Without actually having data within them. Eg. for the roll button texture,
			// We use an arbitrary value of "6" in the int array, with no regards,
			// To whether or not we've initialized the button texture as of this moment. 

			int[] objectsToDraw = new int[] { placeholderMoney,
				placeholderUsers, placeholderMemes,
				placeholderBandwidthAmount, placeholderMalice,
				placeholderCharity, placeholderRollButtonTexture }; // Place all drawn elements here.

			DrawYPositions = new int[objectsToDraw.Length]; // The draw y position will correlate to the UI elements in the hierarchy below the Icon element.

			// Loops through the object array, adding a drawY value for each element.
			// The last value in the array will always be the rollButton's drawY.
			for (int index = 0; index < objectsToDraw.Length; index++)
			{
				drawY = iy + Icon.Height + baseIncrement + (amountToIncrement * index);
				DrawYPositions[index] = drawY;
			}
		}

		// This method changes a player's attributes based off of some card
		public void CardEffect(Card c)
		{
			switch (c.Attribute)
			{
				case "Money":
					money += c.Amount;
					break;
				case "Users":
					users += c.Amount;
					break;
				case "Memes":
					memes += c.Amount;
					break;
				case "Bandwidth":
					bandwidth += c.Amount;
					break;
			}
		}

		// This method applies morality to a user
		public void ApplyMorality(Card c)
		{
			if (c.Malice  != 0) malice  -= c.Malice;
			if (c.Charity != 0) charity += c.Charity;
		}

		public bool CheckWinStatus(int turnCount)
		{
			switch (role)
			{
				case Role.Dankest: if(memes >= MEME_LIMIT) { return true; }
					break;
				case Role.Narcissist: if(turnCount >= TURN_LIMIT) { return true; }
					break;
				case Role.Plastic: if(users >= USER_LIMIT) { return true; }
					break;
				case Role.TopHat: if(money >= WEALTH_LIMIT) { return true; }
					break;
			}
			return false;
		}
	}
}
