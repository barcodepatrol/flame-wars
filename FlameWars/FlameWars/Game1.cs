using Microsoft.Xna.Framework;
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
		MouseState oldMState;
		MouseState newMState;
		SpriteFont mainFont;
        Texture2D board;
		Texture2D background;
        Vector2 vec;

		// Game Classes
		World world;
		Menu menu;
		HowTo howto;
		Pause pause;

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
            graphics.IsFullScreen              = false;	// Make this true for the real game, false for testing
            graphics.ApplyChanges();
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
			newMState = Mouse.GetState();
			
			// Create Game Objects
			world = new World(4);
			menu  = new Menu(SCREEN_WIDTH, SCREEN_HEIGHT);
			howto = new HowTo(SCREEN_WIDTH, SCREEN_HEIGHT);
			pause = new Pause(SCREEN_WIDTH, SCREEN_HEIGHT);

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
            //board = Content.Load<Texture2D>("Board");

			// Load fonts
			mainFont = Content.Load<SpriteFont>("BROWNIEregular_14");

			// Load Menu Content
			menu.LoadContent(Content.Load<Texture2D>("PlayButton.png"), 
							 Content.Load<Texture2D>("HowToButton.png"), 
							 Content.Load<Texture2D>("ExitButton.png"));

			// Load HowTo Content
			howto.LoadContent(Content.Load<Texture2D>("ReturnButton.png"), 
							  Content.Load<Texture2D>("ExitButton.png"));

			// Load Pause Content
			pause.LoadContent(Content.Load<Texture2D>("ResumeButton.png"),
							  Content.Load<Texture2D>("HowToButton.png"), 
							  Content.Load<Texture2D>("MenuButton.png"), 
							  Content.Load<Texture2D>("ExitButton.png"));

			// Load path textures
			board = Content.Load<Texture2D>("path_texture_01");

			// load background texture
			background = Content.Load<Texture2D>("board_final");

			world.InitializeBoard(board, background);
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
			oldMState = newMState;
			newMState = Mouse.GetState();

			// Switch statements is used to determine our current game state
			switch (StateManager.gameState)
			{
				case StateManager.GameState.Menu:
					// Update the menu object
					menu.Update(newMState.X, newMState.Y);
					
					// If the lmb was just released, call menu.released
					// Call the hover method to determine if mouse is hovering
					// If the lmb is being pressed, call menu.pressed
					if (Released()) menu.Released();
					menu.Hover();
					if (Pressed())  menu.Pressed();
					break;

				case StateManager.GameState.HowTo:
					// Update the howto object
					howto.Update(newMState.X, newMState.Y);
					
					// If the lmb was just released, call menu.released
					// Call the hover method to determine if mouse is hovering
					// If the lmb is being pressed, call menu.pressed
					if (Released()) howto.Released();
					howto.Hover();
					if (Pressed())  howto.Pressed();
					break;

				case StateManager.GameState.Pause:
					// Update the pause object
					pause.Update(newMState.X, newMState.Y);
					
					// If the lmb was just released, call menu.released
					// Call the hover method to determine if mouse is hovering
					// If the lmb is being pressed, call menu.pressed
					if (Released()) pause.Released();
					pause.Hover();
					if (Pressed())  pause.Pressed();
					break;

				case StateManager.GameState.Game:

					// Do stuff with World class

					// Game is paused
					if (newKState.IsKeyDown(Keys.P))
					{
						StateManager.gameState = StateManager.GameState.Pause;
						break;
					}
					break;

				case StateManager.GameState.Exit:
					Exit();
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

            //Vector2 scrVec = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            //Vector2 origin = new Vector2(board.Width / 2, board.Height / 2);

            //Rectangle rec = new Rectangle(0,0,board.Width,board.Height);
            //spriteBatch.Draw(board, scrVec, null, Color.White, 0, origin, 1f, SpriteEffects.None, 0);

			// Switch statements is used to determine our current game state
			switch (StateManager.gameState)
			{
				case StateManager.GameState.Menu:
					
					menu.Draw(spriteBatch);
					break;

				case StateManager.GameState.HowTo:

					howto.Draw(spriteBatch);
					break;

				case StateManager.GameState.Pause:

					pause.Draw(spriteBatch);
					break;

				case StateManager.GameState.Game:
					// Do stuff with World class
					world.Draw(spriteBatch);
					break;

				case StateManager.GameState.Exit:
					Exit();
					break;
			}

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
			if (oldMState.LeftButton == ButtonState.Pressed && 
				newMState.LeftButton == ButtonState.Released)
			{
				return true;
			}
			else return false;
		}

		// This method determines if the left mouse button is being pressed
		private bool Pressed()
		{
			if (newMState.LeftButton == ButtonState.Pressed)
			{
				return true;
			}
			else return false;
		}
    }
}
