#define DEBUG
// #undef DEBUG

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
		// ======================= Instance Variables =================================
		// ============================================================================

		// Graphics variables.
		private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
		private SpriteFont mainFont;

		// Keyboard input states.
		private KeyboardState previousKeyboardState;
		private KeyboardState currentKeyboardState;

		// Mouse input states.
		private MouseState previousMouseState;
		private MouseState currentMouseState;
		
		// Vector for passing along position information.
        private Vector2 vec;

		// Game and State Classes
		private World world;
		private Menu menuState;
		private HowTo howToState;
		private Pause pauseState;

		// Measurements and boolean variables.
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;
		private bool debug;

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

        public Game1()
        {
#if DEBUG
			debug = true;
#else
			debug = false;
#endif
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

			// Initialize GameManager
			GameManager.Init(SCREEN_WIDTH, SCREEN_HEIGHT);

			// Keyboard and mouse initialization
			currentKeyboardState  = Keyboard.GetState();
			currentMouseState = Mouse.GetState();

			// Initialize Management Classes.
			ArtManager.Initialize(this.Content, debug);
			
			// Create Game Objects
			world = new World(4);
			menuState  = new Menu();
			howToState = new HowTo();
			pauseState = new Pause();

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
			// Load artwork into the manager.
			ArtManager.Load();

			// Load World Content
			world.LoadContent();
		
			// Load fonts
			mainFont = Content.Load<SpriteFont>("BROWNIEregular_14");

			// Load Menu Content
			menuState.LoadContent();

			// Load HowTo Content
			howToState.LoadContent();

			// Load Pause Content
			pauseState.LoadContent();
			
			// Initialize world. (Must take place after content is loaded.)
			world.Initialize();
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
			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

			// Get mouse states
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			// Switch statements is used to determine our current game state
			switch (StateManager.gameState)
			{
				case StateManager.GameState.Menu:
					// Update the menu object
					menuState.Update(currentMouseState.X, currentMouseState.Y);

					// If the lmb was just released, call menuState.released
					if (Released())
					{
						menuState.Released();
					}

					// Call the hover method to determine if mouse is hovering
					menuState.Hover();

					// If the lmb is being pressed, call menuState.pressed
					if (Pressed())
					{
						menuState.Pressed();
					}

					break;

				case StateManager.GameState.HowTo:
					// Update the howto object
					howToState.Update(currentMouseState.X, currentMouseState.Y);

					// If the lmb was just released, call howToState.released
					if (Released())
					{
						howToState.Released();
					}

					// Call the hover method to determine if mouse is hovering
					howToState.Hover();

					// If the lmb is being pressed, call howToState.pressed
					if (Pressed())
					{
						howToState.Pressed();
					}

					break;

				case StateManager.GameState.Pause:
					// Update the pause object
					pauseState.Update(currentMouseState.X, currentMouseState.Y);

					// If the lmb was just released, call pauseState.released
					if (Released())
					{
						pauseState.Released();
					}

					// Call the hover method to determine if mouse is hovering
					pauseState.Hover();

					// If the lmb is being pressed, call pauseState.pressed
					if (Pressed())
					{
						pauseState.Pressed();
					}

					break;

				case StateManager.GameState.Game:
					// Update the world object.
					world.Update(gameTime, currentMouseState.X, currentMouseState.Y);

					// If the lmb was just released, call world.released
					if (Released())
					{
						world.Released();
					}

					// Call the hover method to determine if mouse is hovering
					world.Hover();

					// If the lmb is being pressed, call world.pressed
					if (Pressed())
					{
						world.Pressed();
					}

					// Game is paused; break out of state and head to different one.
					if (currentKeyboardState.IsKeyDown(Keys.P))
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
					
					menuState.Draw(spriteBatch);
					break;

				case StateManager.GameState.HowTo:

					howToState.Draw(spriteBatch);
					break;

				case StateManager.GameState.Pause:

					pauseState.Draw(spriteBatch);
					break;

				case StateManager.GameState.Game:

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
			if (previousKeyboardState.IsKeyDown(k) && !currentKeyboardState.IsKeyDown(k))
			{
				return true;
			}
			else return false;
		}

		// This method determines if the left mouse button was just released
		private bool Released()
		{
			if (previousMouseState.LeftButton == ButtonState.Pressed && 
				currentMouseState.LeftButton == ButtonState.Released)
			{
				return true;
			}
			else return false;
		}

		// This method determines if the left mouse button is being pressed
		private bool Pressed()
		{
			if (currentMouseState.LeftButton == ButtonState.Pressed)
			{
				return true;
			}
			else return false;
		}
    }
}
