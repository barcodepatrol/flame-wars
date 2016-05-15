#define DEBUG
#undef DEBUG

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

		#region Constants
		private readonly Color BACKGROUND_COLOR = new Color(215, 212, 203); // Changes the background color. This is a brownish-ash gray.
		#endregion

		#region Variables
		// Graphics variables.
		private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

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
		private Start startState;
		private RoleSelector roleState;
		private Menu menuState;
		private HowTo howToState;
		private Pause pauseState;

		// Measurements and boolean variables.
        private int SCREEN_WIDTH;
        private int SCREEN_HEIGHT;
		private bool debug;
		#endregion Variables

		#region Properties
		
		public GraphicsDevice Graphics
		{
			get {return this.graphics.GraphicsDevice; }
		}
		
		#endregion Properties

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

			//graphics.IsFullScreen              = !debug; // Make this true for the real game, false for testing
			
			if (!graphics.IsFullScreen)
			{
				Window.IsBorderless = false;
				SCREEN_WIDTH = 1440;
				SCREEN_HEIGHT = 880;
			}
			else
			{
				Window.IsBorderless = true;
				SCREEN_WIDTH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				SCREEN_HEIGHT = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}

			graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
			graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;

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
			GameManager.Init(graphics, SCREEN_WIDTH, SCREEN_HEIGHT);

			// Keyboard and mouse initialization
			currentKeyboardState  = Keyboard.GetState();
			currentMouseState = Mouse.GetState();

			// Initialize Management Classes.
			ArtManager.Initialize(Content, debug);
			
			// Create Game Objects
			world      = new World();
			startState = new Start();
			roleState  = new RoleSelector();
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

			// Load Start Content
			startState.LoadContent();

			// Load Role content
			roleState.LoadContent();

			// Load Menu Content
			menuState.LoadContent();

			// Load HowTo Content
			howToState.LoadContent();

			// Load Pause Content
			pauseState.LoadContent();
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

			// Check to see if Message Box is active
			if (Message.isActive)
			{
				// Update MessageBox's position
				Message.Update(currentMouseState.X, currentMouseState.Y);

				// If the lmb was just released, call Message.released
				if (Released())
				{
					Message.Released();
				}

				// Call the hover method to determine if mouse is hovering
				Message.Hover();

				// If the lmb is being pressed, call Message.pressed
				if (Pressed())
				{
					Message.Pressed();
				}
			}

			// Check to see if Target Box is active
			if (Target.isActive)
			{
				// Update TargetBox's position
				Target.Update(currentMouseState.X, currentMouseState.Y);

				// If the lmb was just released, call Target.released
				if (Released())
				{
					Target.Released();
				}

				// Call the hover method to determine if mouse is hovering
				Target.Hover();

				// If the lmb is being pressed, call Target.pressed
				if (Pressed())
				{
					Target.Pressed();
				}
			}

			// Switch statements is used to determine our current game state
			switch (StateManager.gameState)
			{
				case StateManager.GameState.Start:
					// Update the start object
					startState.Update(currentMouseState.X, currentMouseState.Y);

					// If the lmb was just released, call startState.released
					if (Released())
					{

						startState.Released();

						// NOW we set the amount of players
						world.Initialize(GameManager.NumberOfPlayers);
						roleState.FirstInit();
						roleState.Initialize();
					}

					// Call the hover method to determine if mouse is hovering
					startState.Hover();

					// If the lmb is being pressed, call startState.pressed
					if (Pressed())
					{
						startState.Pressed();
					}

					break;

				case StateManager.GameState.Role:
					// Update the start object
					roleState.Update(currentMouseState.X, currentMouseState.Y);

					// If the lmb was just released, call startState.released
					if (Released())
					{
						roleState.Released();
					}

					// Call the hover method to determine if mouse is hovering
					roleState.Hover();

					// If the lmb is being pressed, call startState.pressed
					if (Pressed())
					{
						roleState.Pressed();
					}

					break;

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

						// If a message box is active, deactivate it and tell pause one exists
						if (Message.isActive)
						{ 
							Message.isActive = false;
							pauseState.MessageExists = true;
						}

						break;
					}
					break;

				case StateManager.GameState.Exit:
					Exit();
					break;

				// resets he game
				case StateManager.GameState.Reset:
					// reinitializes game1 and creates new version of world and managers which initialize remaining classes
					Initialize();
					// game is no longer at end
					GameManager.EndGame = false;
					// bring game back to menu
					StateManager.gameState = StateManager.GameState.Menu;
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
            GraphicsDevice.Clear(BACKGROUND_COLOR);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Vector2 scrVec = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            //Vector2 origin = new Vector2(board.Width / 2, board.Height / 2);

            //Rectangle rec = new Rectangle(0,0,board.Width,board.Height);
            //spriteBatch.Draw(board, scrVec, null, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
			
			

			// Switch statements is used to determine our current game state
			switch (StateManager.gameState)
			{
				case StateManager.GameState.Start:

					startState.Draw(spriteBatch);
					break;

				case StateManager.GameState.Role:

					roleState.Draw(spriteBatch);
					break;

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
			
			// Check to see if Message Box is active
			if (Message.isActive)
			{
				Message.Draw(spriteBatch);
			}

			// Check to see if Target Box is active
			if (Target.isActive)
			{
				Target.Draw(spriteBatch);
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
