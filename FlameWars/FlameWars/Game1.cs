﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		KeyboardState oldKState;
		KeyboardState newKState;
		MouseState oldmState;
		MouseState newmState;
        Texture2D board;
        Vector2 vec;

		// Game Classes
		World world;
		Menu menu;

        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            SCREEN_WIDTH  = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            SCREEN_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //graphics.PreferredBackBufferWidth = 1400;  // set this value to the desired width of your window
            //graphics.PreferredBackBufferHeight = 900;   // set this value to the desired height of your window
            //graphics.ApplyChanges();
            graphics.PreferredBackBufferWidth  = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen              = true;
            graphics.ApplyChanges();
			
			// Create Game Objects
			world = new World(4);
			menu  = new Menu(SCREEN_WIDTH, SCREEN_HEIGHT);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            vec = new Vector2(0, 0);

			// Window Initialization
            this.Window.Position = new Point(0, 0);
            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += new System.EventHandler<System.EventArgs>(Window_ClientSizeChanged);

			// Keyboard and mouse initialization
			newKState  = Keyboard.GetState();
			newmState = Mouse.GetState();

            base.Initialize();
        }

        void Window_ClientSizeChanged(object sender, System.EventArgs e)
        {
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            board = Content.Load<Texture2D>("Board");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

			// Get keyboard states
			oldKState = newKState;
			newKState = Keyboard.GetState();

			// Get mouse states
			oldmState = newmState;
			newmState = Mouse.GetState();

			// Switch statements is used to determine our current game state
			switch (StateManager.gameState)
			{
				case StateManager.GameState.Menu:
					
					menu.Update(newmState.X, newmState.Y);
					menu.Hover();
					if (Released()) menu.Press();
					break;

				case StateManager.GameState.HowTo:
					break;
				case StateManager.GameState.Pause:
					break;
				case StateManager.GameState.Game:
					// Do stuff with World class
					break;
			}

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            Vector2 scrVec = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            Vector2 origin = new Vector2(board.Width / 2, board.Height / 2);

            //Rectangle rec = new Rectangle(0,0,board.Width,board.Height);
            spriteBatch.Draw(board, scrVec, null, Color.White, 0, origin, 1f, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }

		// This method determines if a key was just released
		private bool Released(Keys k)
		{
			if (oldKState.IsKeyDown(k) && !newKState.IsKeyDown(k))
			{
				return true;
			}
			else return false;
		}

		// This method determines if the left mouse button was just released
		private bool Released()
		{
			if (oldmState.LeftButton == ButtonState.Pressed && 
				newmState.LeftButton == ButtonState.Released)
			{
				return true;
			}
			else return false;
		}
    }
}
