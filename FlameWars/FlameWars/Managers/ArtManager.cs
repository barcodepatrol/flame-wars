using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlameWars
{
    internal static class ArtManager
    {

		// ============================================================================
		// ================================ Constants =================================
		// ============================================================================

		#region Filepaths
		// Filenames for the textures.
		// Necessary texture filenames.
		public const string BACKGROUND_TEXTURE = ""; // For the backgroundTexture.
		public const string FOREGROUND_TEXTURE = ""; // For the foregroundTexture.

		// Font filenames
		public const string STELLAR_BOLD_FONT = "Stellar-Bold_24";
		public const string STELLAR_LIGHT_FONT = "Stellar-Light_14";

		// Core texture filenames.
		public const string BOARD_TEXTURE              = "board_final"; // For boardTexture.
		public const string BOARD_TEXTURE_PLACEHOLDER  = "board_placeholder"; // For boardTexture.
		public const string PLAYER_TEXTURE_01          = "player_texture_01"; // For playerTexture1.
		public const string PLAYER_TEXTURE_02          = "player_texture_02"; // For playerTexture2.
		public const string PLAYER_TEXTURE_03          = "player_texture_03"; // For playerTexture3.
		public const string PLAYER_TEXTURE_04          = "player_texture_04"; // For playerTexture4.
		public const string PLAYER_TEXTURE_PLACEHOLDER = "token_placeholder"; // For playerTexture1, playerTexture2, playerTexture3, playerTexture4.

		// UI texture filenames.
		public const string PLAYER_ICON_01 = "player_icon_01"; // For playerIcon1.
		public const string PLAYER_ICON_02 = "player_icon_02"; // For playerIcon2.
		public const string PLAYER_ICON_03 = "player_icon_03"; // For playerIcon3.
		public const string PLAYER_ICON_04 = "player_icon_04"; // For playerIcon4.
		public const string PLAYER_ICON_PLACEHOLDER = "token"; // For playerIcon1, playerIcon2, playerIcon3, playerIcon4.

		// Button texture filenames.
		public const string BUTTON_OK     = "OkButton"; // For okButton.
		public const string BUTTON_EXIT   = "ExitButton"; // For exitButton.
		public const string BUTTON_HOW_TO = "HowToButton"; // For howToButton.
		public const string BUTTON_MENU   = "MenuButton"; // For menuButton.
		public const string BUTTON_PLAY   = "PlayButton"; // For playButton.
		public const string BUTTON_RESUME = "ResumeButton"; // For resumeButton.
		public const string BUTTON_RETURN = "ReturnButton"; // For returnButton.
		public const string BUTTON_CANCEL = "CancelButton"; // For cancelButton.
		public const string BUTTON_ROLL   = "RollButton"; // For rollButton.

		// Path texture filenames.
		public const string PATH_TEXTURE_01 = "path_texture_01"; // For pathTexture1;
		public const string PATH_TEXTURE_02 = "path_texture_02"; // For pathTexture2;
		public const string PATH_TEXTURE_03 = "path_texture_03"; // For pathTexture3;
		public const string PATH_TEXTURE_04 = "path_texture_04"; // For pathTexture4;
		public const string PATH_TEXTURE_PLACEHOLDER = "path_placeholder"; // For pathTexture1, pathTexture2, pathTexture3, pathTexture4;

		// How to instructions filename.
		public const string HOW_TO_INSTRUCTIONS = "Default how to instructions"; // For placeholder how to image.

		// MessageBox texture.
		public const string MESSAGE_BOX = "MessageBox"; // For messageBox.
		#endregion Filepaths

		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables
		// Necessary textures for the game.
		private static Texture2D backgroundTexture; // The background texture.
		private static Texture2D foregroundTexture; // The foreground texture.

		// Game Fonts
		private static SpriteFont stellarBoldFont;
		private static SpriteFont stellarLightFont;

		// Core textures.
		private static Texture2D boardTexture; // The actual board's texture.
		private static Texture2D playerTexture1; // Player 1's token texture.
		private static Texture2D playerTexture2; // Player 2's token texture.
		private static Texture2D playerTexture3; // Player 3's token texture.
		private static Texture2D playerTexture4; // Player 4's token texture.

		// UI textures.
		private static Texture2D playerIcon1; // Player 1's icon texture.
		private static Texture2D playerIcon2; // Player 2's icon texture.
		private static Texture2D playerIcon3; // Player 3's icon texture.
		private static Texture2D playerIcon4; // Player 4's icon texture.

		// Button textures.
		private static Texture2D okButton; // Ok button texture.
		private static Texture2D exitButton; // Exit button texture.
		private static Texture2D howToButton; // How-To button texture.
		private static Texture2D menuButton; // Menu button texture.
		private static Texture2D playButton; // Play button texture.
		private static Texture2D resumeButton; // Resume button texture.
		private static Texture2D returnButton; // Return button texture.
		private static Texture2D cancelButton; // Cancel button texture.
		private static Texture2D rollButton; // Roll button texture.

		// Path textures.
		private static Texture2D pathTexture1; // Path Texture type 1.
		private static Texture2D pathTexture2; // Path Texture type 2.
		private static Texture2D pathTexture3; // Path Texture type 3.
		private static Texture2D pathTexture4; // Path Texture type 4.
		private static Texture2D[] paths; // Path array.

		// how-to texture
		private static Texture2D howToInstructions; // How to texture

		// Message Box texture
		private static Texture2D messageBox; // Message box texture
		#endregion Variables

		// ============================================================================
		// =============================== Properties =================================
		// ============================================================================

		#region Properties
		// Misc. properities.
		public static ContentManager Content { get; set; }
		public static bool Debug { get; set; }

		// Necessary textures for the game.
		public static Texture2D Background { get { return backgroundTexture; } set { backgroundTexture = value; } }
		public static Texture2D Foreground { get { return foregroundTexture; } set { foregroundTexture = value; } }

		// Game Fonts
		public static SpriteFont StellarBoldFont { get {return stellarBoldFont; } set {stellarBoldFont = value; } }
		public static SpriteFont StellarLightFont { get {return stellarLightFont; } set {stellarLightFont = value; } }

		// Core textures.
		public static Texture2D Board { get { return boardTexture; } set { boardTexture = value; } }
		public static Texture2D Player1 { get { return playerTexture1; } set { playerTexture1 = value; } }
		public static Texture2D Player2 { get { return playerTexture2; } set { playerTexture2 = value; } }
		public static Texture2D Player3 { get { return playerTexture3; } set { playerTexture3 = value; } }
		public static Texture2D Player4 { get { return playerTexture4; } set { playerTexture4 = value; } }

		// UI textures.
		public static Texture2D PlayerIcon1 { get { return playerIcon1; } set { playerIcon1 = value; } }
		public static Texture2D PlayerIcon2 { get { return playerIcon2; } set { playerIcon2 = value; } }
		public static Texture2D PlayerIcon3 { get { return playerIcon3; } set { playerIcon3 = value; } }
		public static Texture2D PlayerIcon4 { get { return playerIcon4; } set { playerIcon4 = value; } }

		// Button textures.
		public static Texture2D OkButton { get { return okButton; } set { okButton = value; } }
		public static Texture2D ExitButton { get { return exitButton; } set { exitButton = value; } }
		public static Texture2D HowToButton { get { return howToButton; } set { howToButton = value; } }
		public static Texture2D MenuButton { get { return menuButton; } set { menuButton = value; } }
		public static Texture2D PlayButton { get { return playButton; } set { playButton = value; } }
		public static Texture2D ResumeButton { get { return resumeButton; } set { resumeButton = value; } }
		public static Texture2D ReturnButton { get { return returnButton; } set { returnButton = value; } }
		public static Texture2D CancelButton { get { return cancelButton; } set { cancelButton = value; } }
		public static Texture2D RollButton { get { return rollButton; } set { rollButton = value; } }

		// Path textures.
		public static Texture2D PathTexture1 { get { return pathTexture1; } set { pathTexture1 = value; } }
		public static Texture2D PathTexture2 { get { return pathTexture2; } set { pathTexture2 = value; } }
		public static Texture2D PathTexture3 { get { return pathTexture3; } set { pathTexture3 = value; } }
		public static Texture2D PathTexture4 { get { return pathTexture4; } set { pathTexture4 = value; } }
		public static Texture2D[] Paths { get { return paths; } set { paths = value; } }

		// how-to texture
		public static Texture2D HowToInstructions { get { return howToInstructions; } set { howToInstructions = value; } }

		// Message box texture
		public static Texture2D MessageBox { get { return messageBox; } set { messageBox = value; } }
		#endregion Properties

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		// Initialize a series of these Texture2D items.
		public static void Initialize(ContentManager cm, bool debug)
		{
			Content = cm;
			Debug = debug;
		}

		public static void Initialize(ContentManager cm)
		{
			Content = cm;
			Debug = false;
		}

		public static void Load()
		{
			if (!Debug)
			{
				// Add the items to be loaded here. Comment out non-existent artwork.

				// Necessary textures.
				// Background = Content.Load<Texture2D>(BACKGROUND_TEXTURE);
				// Foreground = Content.Load<Texture2D>(FOREGROUND_TEXTURE);

				// Game fonts
				StellarBoldFont = Content.Load<SpriteFont>(STELLAR_BOLD_FONT);
				StellarLightFont = Content.Load<SpriteFont>(STELLAR_LIGHT_FONT);

				// Core textures.
				Board   = Content.Load<Texture2D>(BOARD_TEXTURE);
				Player1 = Content.Load<Texture2D>(PLAYER_TEXTURE_01);
				Player2 = Content.Load<Texture2D>(PLAYER_TEXTURE_02);
				Player3 = Content.Load<Texture2D>(PLAYER_TEXTURE_03);
				Player4 = Content.Load<Texture2D>(PLAYER_TEXTURE_04);

				// UI textures.
				PlayerIcon1 = Content.Load<Texture2D>(PLAYER_ICON_01);
				PlayerIcon2 = Content.Load<Texture2D>(PLAYER_ICON_02);
				PlayerIcon3 = Content.Load<Texture2D>(PLAYER_ICON_03);
				PlayerIcon4 = Content.Load<Texture2D>(PLAYER_ICON_04);

				// Button textures.
				OkButton     = Content.Load<Texture2D>(BUTTON_OK);
				ExitButton   = Content.Load<Texture2D>(BUTTON_EXIT);
				HowToButton  = Content.Load<Texture2D>(BUTTON_HOW_TO);
				MenuButton   = Content.Load<Texture2D>(BUTTON_MENU);
				PlayButton   = Content.Load<Texture2D>(BUTTON_PLAY);
				ResumeButton = Content.Load<Texture2D>(BUTTON_RESUME);
				ReturnButton = Content.Load<Texture2D>(BUTTON_RETURN);
				CancelButton = Content.Load<Texture2D>(BUTTON_CANCEL);
				RollButton   = Content.Load<Texture2D>(BUTTON_ROLL);

				// Path textures.
				PathTexture1 = Content.Load<Texture2D>(PATH_TEXTURE_01);
				PathTexture2 = Content.Load<Texture2D>(PATH_TEXTURE_02);
				PathTexture3 = Content.Load<Texture2D>(PATH_TEXTURE_03);
				PathTexture4 = Content.Load<Texture2D>(PATH_TEXTURE_04);
				Paths = new Texture2D[] { pathTexture1, pathTexture2, pathTexture3, pathTexture4 };

				// how-to texture
				HowToInstructions = Content.Load<Texture2D>(HOW_TO_INSTRUCTIONS);

				// Message Box texture
				MessageBox = Content.Load<Texture2D>(MESSAGE_BOX);
			}
			else
			{
				LoadDebugMode();
			}			
		}

		public static void LoadDebugMode()
		{
			// Add the items to be loaded here. Comment out non-existent artwork.

			// Necessary textures.
			// Background = Content.Load<Texture2D>(BACKGROUND_TEXTURE);
			// Foreground = Content.Load<Texture2D>(FOREGROUND_TEXTURE);

			// Game fonts
			StellarBoldFont = Content.Load<SpriteFont>(STELLAR_BOLD_FONT);
			StellarLightFont = Content.Load<SpriteFont>(STELLAR_LIGHT_FONT);

			// Core textures.
			Board   = Content.Load<Texture2D>(BOARD_TEXTURE_PLACEHOLDER);
			Player1 = Content.Load<Texture2D>(PLAYER_TEXTURE_PLACEHOLDER);
			Player2 = Content.Load<Texture2D>(PLAYER_TEXTURE_PLACEHOLDER);
			Player3 = Content.Load<Texture2D>(PLAYER_TEXTURE_PLACEHOLDER);
			Player4 = Content.Load<Texture2D>(PLAYER_TEXTURE_PLACEHOLDER);

			// UI textures.
			PlayerIcon1 = Content.Load<Texture2D>(PLAYER_ICON_PLACEHOLDER);
			PlayerIcon2 = Content.Load<Texture2D>(PLAYER_ICON_PLACEHOLDER);
			PlayerIcon3 = Content.Load<Texture2D>(PLAYER_ICON_PLACEHOLDER);
			PlayerIcon4 = Content.Load<Texture2D>(PLAYER_ICON_PLACEHOLDER);

			// Button textures.
			OkButton     = Content.Load<Texture2D>(BUTTON_OK);
			ExitButton   = Content.Load<Texture2D>(BUTTON_EXIT);
			HowToButton  = Content.Load<Texture2D>(BUTTON_HOW_TO);
			MenuButton   = Content.Load<Texture2D>(BUTTON_MENU);
			PlayButton   = Content.Load<Texture2D>(BUTTON_PLAY);
			ResumeButton = Content.Load<Texture2D>(BUTTON_RESUME);
			ReturnButton = Content.Load<Texture2D>(BUTTON_RETURN);
			CancelButton = Content.Load<Texture2D>(BUTTON_CANCEL);
			RollButton   = Content.Load<Texture2D>(BUTTON_ROLL);

			// Path textures.
			PathTexture1 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			PathTexture2 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			PathTexture3 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			PathTexture4 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			Paths = new Texture2D[] { pathTexture1, pathTexture2, pathTexture3, pathTexture4 };

			// How-to texture
			HowToInstructions = Content.Load<Texture2D>(HOW_TO_INSTRUCTIONS);

			// Message Box texture
			MessageBox = Content.Load<Texture2D>(MESSAGE_BOX);
		}
				
	}
}