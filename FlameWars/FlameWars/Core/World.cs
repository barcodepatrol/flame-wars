﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
	class World
	{
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables
		public static bool DISPLAY_WIN_STATUS = false;

		enum PlayerState { PlayerOne, PlayerTwo, PlayerThree, PlayerFour };
		private int numberOfPlayers = 0;
		//private int totalBandwidth = 100;
		private int turnCount = 1;

		const int PLAYER_UI_WIDTH  = 180;
		const int PLAYER_UI_HEIGHT = 350;
		const int BUTTON_HEIGHT    = 50;
		const int BUTTON_WIDTH     = 75;
		const int PLAYER_X         = 15;
		const int PLAYER_Y         = 15;
		const int ANIM_SPEED       = 5;
		const int FRAME_PER_SECOND = 30;
	
		readonly Vector2 TOP_LEFT_POSITION = new Vector2(PLAYER_X, PLAYER_Y);
		readonly Vector2 TOP_RIGHT_POSITION = new Vector2(GameManager.Width - PLAYER_UI_WIDTH - (PLAYER_X / 2), PLAYER_Y);
		readonly Vector2 BOTTOM_LEFT_POSITION = new Vector2(PLAYER_X, GameManager.Height - PLAYER_UI_HEIGHT - PLAYER_Y - BUTTON_HEIGHT);
		readonly Vector2 BOTTOM_RIGHT_POSIITION = new Vector2(GameManager.Width - PLAYER_UI_WIDTH - (PLAYER_X / 2), GameManager.Height - PLAYER_UI_HEIGHT - PLAYER_Y - BUTTON_HEIGHT);
		
		SoundManager sm; // TODO
        OptionsManager om; // TODO

		Color[] buttonColors;
		Texture2D buttonTexture; // Roll button texture.
		Player player1;
		Player player2;
		Player player3;
		Player player4;
		List<Player> players;
		PlayerState playerState;
		Board board;
		Player currentPlayer;

		// state management variables.
		int mX, mY; // Mouse state variables.
		int windowWidth, windowHeight; // Height and width of screen.

		#endregion Variables

		#region Properties
		//* Properties *//

		// Stores the first player object
		public Player PlayerOne
		{
			get { return this.player1; }
			set { this.player1 = value; }
		}

		// Stores the first player object
		public Player PlayerTwo
		{
			get { return this.player2; }
			set { this.player2 = value; }
		}

		// Stores the first player object
		public Player PlayerThree
		{
			get { return this.player3; }
			set { this.player3 = value; }
		}

		// Stores the first player object
		public Player PlayerFour
		{
			get { return this.player4; }
			set { this.player4 = value; }
		}		

		// stores the amount of badwidth available on the board
		/*public int TotalBandwidth
		{
			get { return this.totalBandwidth; }
			set { this.totalBandwidth = value; }
		}*/

		// stores count of turns since game has begun
		public int TurnCount
		{
			get { return this.turnCount; }
		}		
		#endregion Properties

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Constructor

        // Constructor
		public World()
		{
			// Initialize the managers
			sm = new SoundManager(0,0);
			om = new OptionsManager();
		}

		// Passes texture to board object
		public void Initialize(int players)
		{
			World.DISPLAY_WIN_STATUS = false;
			Texture2D[] resources = ArtManager.Resources;
			Texture2D[] cards = ArtManager.Cards;
			Texture2D[] bonds = ArtManager.Bonds;
			Texture2D[] bondreturns = ArtManager.BondReclaims;
			Texture2D[] empty = ArtManager.EmptyPaths;
			Texture2D[] random = ArtManager.RandomPaths;

			Texture2D boardImage   = ArtManager.Board;

			// Initialize the board
			board = new Board(resources, cards, bonds, bondreturns, empty, random, boardImage);

			InitializePlayers(players);
			InitializeRoles();
			LoadContent();
			InitializePlayerTokens();

			windowWidth  = GameManager.Width;
			windowHeight = GameManager.Height;
			
			buttonColors = new Color[]{ Color.DarkGray, Color.White }; // 0 for inactive, 1 for active.
			buttonTexture = ArtManager.RollButton;

			MakeButtons();
		}

		// This method initializes the players
		public void InitializePlayers(int players)
		{
			#region CreatePlayers

			// Initialize the players
			// There will always be at least two players
			this.players = new List<Player>();
			player1      = new Player(TOP_LEFT_POSITION);
			player2      = new Player(TOP_RIGHT_POSITION);
			this.players.Add(player1); 
			this.players.Add(player2);

			// Four players
			if (players > 3)
			{
				player3 = new Player(BOTTOM_LEFT_POSITION);
				player4 = new Player(BOTTOM_RIGHT_POSIITION);
				this.players.Add(player3); 
				this.players.Add(player4);
			}
			// Three Players
			else if (players > 2)
			{
				player3 = new Player(BOTTOM_LEFT_POSITION);
				this.players.Add(player3);
			}
			#endregion CreatePlayers

			// Set currentPlayer and playerState to first player
			currentPlayer = this.players[0];
			GameManager.CurrentPlayer = currentPlayer;
			playerState = PlayerState.PlayerOne;
			currentPlayer.Start();
		}

		// This method will initialize the player token positions
		public void InitializePlayerTokens()
		{
			// Iterate through all players
			for (int index = 0; index < this.players.Count; index++)
			{
				// Set the draw position for the player token.
				Path playerLocation = board.GetPath(this.players[index].BoardPosition);
				Rectangle pathLocation = new Rectangle(playerLocation.Bounds.X, 
													   playerLocation.Bounds.Y, 
													   playerLocation.Bounds.Width, 
													   playerLocation.Bounds.Height);

				// Find the origin of the player.
				float tokenScale = 0.25f; // This will be scale of the token.
				float divisor = 1f / tokenScale; // This is the divisor.

				// Find the centered corners of the path.
				int playerHeight = this.players[index].Token.Height;
				int playerWidth = this.players[index].Token.Width;

				playerHeight = (int)(tokenScale * playerHeight);
				playerWidth = (int)(tokenScale * playerWidth);

				int playerQuarterOffsetX = (int)(playerWidth / divisor);
				int playerQuarterOffsetY = (int)(playerHeight / divisor);
				int playerOffsetX = (playerWidth / 2);
				int playerOffsetY = (playerHeight / 2);

				int margin = (int)(75 * tokenScale);

				Vector2 topLeft = new Vector2(pathLocation.X - playerQuarterOffsetX, 
											  pathLocation.Y - playerQuarterOffsetY);
				Vector2 playerPosition = new Vector2(0, 0);

				switch (index)
				{
					case 0:
						// Player One gets TOP LEFT.
						playerPosition = new Vector2(topLeft.X + margin, 
													 topLeft.Y + margin);
						break;
					case 1:
						// Player Two gets TOP RIGHT.
						playerPosition = new Vector2(topLeft.X + (pathLocation.Width - playerOffsetX) - margin, 
													 topLeft.Y + margin);
						break;
					case 2:
						// Player Three gets BOTTOM LEFT.
						playerPosition = new Vector2(topLeft.X + margin, 
													 topLeft.Y + (pathLocation.Height - playerOffsetY) - margin);
						break;
					case 3:
						// Player Four gets BOTTOM RIGHT.
						playerPosition = new Vector2(topLeft.X + (pathLocation.Width - playerOffsetX) - margin, 
													 topLeft.Y + (pathLocation.Height - playerOffsetY) - margin);
						break;
				}

				// Set each player's token's position
				this.players[index].TokenPosition = new Rectangle((int)(playerPosition.X), 
																  (int)(playerPosition.Y), 
																       (int)(GameManager.ScreenScale*playerWidth), 
																	   (int)(GameManager.ScreenScale*playerHeight));
			}
		}

		// This method will initialize and set the roles for each player
		public void InitializeRoles()
		{
			// Iterate through total players
			for (int i = 0; i < GameManager.NumberOfPlayers; i++)
			{
				// Set role based off of int
				switch (GameManager.PlayerRoles[i])
				{
					// Top Hat
					case 0: players[i].PlayerRole = Player.Role.TopHat; break;
					// Plastic
					case 1: players[i].PlayerRole = Player.Role.Plastic; break;
					// Narcissist
					case 2: players[i].PlayerRole = Player.Role.Narcissist; break;
					// Dankest
					case 3: players[i].PlayerRole = Player.Role.Dankest; break;
				}
			}
		}

		// Ask each player to make their own button.
		public void MakeButtons()
		{
			foreach (Player player in players)
			{
				// Create a button of a certain width and height.
				// Add it to the player's UI.
				player.MakeButton(buttonTexture, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}

		// Ask each player to determine the value of the button.
		public void Hover()
		{
			currentPlayer.Hover(mX, mY);
		}

		public void Pressed()
		{
			currentPlayer.Pressed(mX, mY);
		}

		public void Released()
		{
			currentPlayer.Released(mX, mY);
		}

		// Loads texture content
		public void LoadContent()
		{
			// Load player texture data
			if (player1 != null) { player1.Token = ArtManager.Player1; player1.Icon = ArtManager.PlayerIcon1; }
			if (player2 != null) { player2.Token = ArtManager.Player2; player2.Icon = ArtManager.PlayerIcon2; }
			if (player3 != null) { player3.Token = ArtManager.Player3; player3.Icon = ArtManager.PlayerIcon3; }
			if (player4 != null) { player4.Token = ArtManager.Player4; player4.Icon = ArtManager.PlayerIcon4; }
		}

		public void Update(GameTime gameTime, int mx, int my)
		{
			Update(gameTime);
			Update(mx, my);
		}

		public void Update(GameTime gameTime)
		{
			// Iterate through all players
			for (int index = 0; index < players.Count; index++)
			{
				// Update currentPlayer
				currentPlayer.Update(gameTime);

				// If the player has just rolled, start animating
				if (currentPlayer.IsRolling())
					AnimatePlayer();

				// If the player is animating, update their animation
				if (currentPlayer.IsAnimated())
					UpdateAnimation();

				// If the player has ended their turn, switch players
				if (GameManager.EndTurn)
				{
					// Check to see if we just targeted a player
					if (Target.isActive)
					{
						// If so, turn off target and change player's stats
						Target.Deactivate();
						int playerTarget = Target.PlayerTarget;
						players[playerTarget].CardEffect(Message.CurrentCard);

						// Subtract cost of card
						currentPlayer.Money -= Message.CurrentCard.Cost;
					}
					// Check to see if we bought a card
					else if (Message.CurrentCard != null && Message.Bought)
					{
						// Change the current player's values
						currentPlayer.CardEffect(Message.CurrentCard);
					}
					// Check to see if we just bought a bond
					else if (Message.CurrentBond != null && Message.Bought)
					{
						// Change the current player's values
						currentPlayer.BuyBond(Message.CurrentBond);
					}

					// Player ends their turn
					GameManager.EndTurn = false;
					currentPlayer.End();
					currentPlayer.GenerateUsers();
					currentPlayer.GenerateMoney();
					currentPlayer.UpdateBonds();

					// Check to see if a player just won
					if (!currentPlayer.CheckWinStatus(turnCount))
						SwitchPlayers(gameTime);
					else
					{
						// Give information to GameManager for EndGame state.
						DISPLAY_WIN_STATUS = true;
						GameManager.EndGame = true;
						GameManager.WinningPlayer = currentPlayer;
						GameManager.WinningPlayerRole = currentPlayer.GetRoleAsString();
						GameManager.WinningPlayerResources = currentPlayer.GetResources();

						// Complete win message:
						string win_message = "Player " + ((int)playerState + 1) + " has won!";
						win_message += "\n" + GameManager.WinInformation;

						// Activate the message box.
						Message.Activate();
						Message.CreateMessage(win_message + "\nReturn to menu.");
					}
				}
			}
		}

		// Passes in a few variables to save for update functions
		public void Update(int mx, int my)
		{
			this.mX = mx;
			this.mY = my;
		}

		public void Draw(SpriteBatch spriteBatch)
		{

			if (!DISPLAY_WIN_STATUS)
			{
				// Draw the board (if not displaying win status).
				board.Draw(spriteBatch);

				// Draw the player UI and Token
				foreach (Player p in players)
				{
					// Draw the player token
					p.DrawToken(spriteBatch);

					// Draw the player UI.
					p.DrawUI(spriteBatch);

					//* DEVELOPERS NOTE *//
					/*
						Originally, p.DrawUI() was in a switch structure
						that desiginated each call to a draw depending on its corner.

						By passing the Vector2 on construction to each player,
						we gain quicker access to the position values, 
						and can draw buttons for each player.				
					*/
				}

				// draw turn counter
				spriteBatch.DrawString(ArtManager.MainFont, "Current Turn: " + turnCount, new Vector2(GameManager.Width / 2 - 70, 50), Color.Black);
			}
			else
			{
				// Should not draw anything during win status from the World class.
			}
		}

		public void SwitchPlayers(GameTime gameTime)
		{
			// Convert current playerstate to int and increment by one
			int pState = (int)playerState;
			pState++;

			// Changes player 5 (doesn't exist) back to player 1
			if (pState == GameManager.NumberOfPlayers)
			{
				pState = 0;
				turnCount++;
				GameManager.CurrentTurnNumber = turnCount;
			}

			// Reset and recast playerState
			playerState = (PlayerState)pState;
			currentPlayer = players[(int)playerState];

			// Set GameManager to have a reference to the current player
			GameManager.CurrentPlayer = currentPlayer;

			// Activate player
			currentPlayer.Start();
		}

		// This method starts the animation by giving the player the
		// required info about the board
		public void AnimatePlayer()
		{
			// Set the player to animating state
			currentPlayer.StartAnimation();
		}

		// This method is called in World.Update
		// The point of this method is to call the player animation update
		// This method will also give the player the new position to go to
		// once the player has moved to the first square, they are told to
		// go to the second square, until they have moved the roll amount
		public void UpdateAnimation()
		{
			// Check if we have reached the final position
			if (currentPlayer.BoardPosition == currentPlayer.FinalPosition)
			{ 
				// End animation, trigger the path object
				currentPlayer.EndAnimation();
				board.GetPath(currentPlayer.FinalPosition).Trigger();
			}
		}
    }
}
