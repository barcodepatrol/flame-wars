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
		// Filenames for the textures
		// Necessary texture filenames
		public const string BACKGROUND_TEXTURE = ""; // For the backgroundTexture
		public const string FOREGROUND_TEXTURE = ""; // For the foregroundTexture

		// Font filenames
		public const string MAIN_FONT    = "MainFont";
		public const string DISPLAY_FONT = "DisplayFont";

		// Core texture filenames
		public const string BOARD_TEXTURE              = "board_final"; // For boardTexture
		public const string BOARD_TEXTURE_PLACEHOLDER  = "board_placeholder"; // For boardTexture
		public const string PLAYER_TOKEN_01 = "token_player1"; // For playerToken1
		public const string PLAYER_TOKEN_02 = "token_player2"; // For playerToken2
		public const string PLAYER_TOKEN_03 = "token_player3"; // For playerToken3
		public const string PLAYER_TOKEN_04 = "token_player4"; // For playerToken3
		public const string PLAYER_TEXTURE_PLACEHOLDER = "token_placeholder"; // For playerTexture1, playerTexture2, playerTexture3, playerTexture4

		// UI texture filenames
		public const string PLAYER_ICON_01 = "ui_player1"; // For playerIcon1
		public const string PLAYER_ICON_02 = "ui_player2"; // For playerIcon2
		public const string PLAYER_ICON_03 = "ui_player3"; // For playerIcon3
		public const string PLAYER_ICON_04 = "ui_player4"; // For playerIcon4
		public const string PLAYER_ICON_PLACEHOLDER = "token"; // For playerIcon1, playerIcon2, playerIcon3, playerIcon4

		// Button texture filenames
		public const string BUTTON_OK      = "OkButton";	  // For okButton
		public const string BUTTON_EXIT    = "ExitButton";    // For exitButton
		public const string BUTTON_HOW_TO  = "HowToButton";   // For howToButton
		public const string BUTTON_MENU    = "MenuButton";    // For menuButton
		public const string BUTTON_PLAY    = "PlayButton";    // For playButton
		public const string BUTTON_RESUME  = "ResumeButton";  // For resumeButton
		public const string BUTTON_RETURN  = "ReturnButton";  // For returnButton
		public const string BUTTON_CANCEL  = "CancelButton";  // For cancelButton
		public const string BUTTON_ROLL    = "RollButton";	  // For rollButton
		public const string BUTTON_PLAYER1 = "Player1Button"; // For player1Button
		public const string BUTTON_PLAYER2 = "Player2Button"; // For player2Button
		public const string BUTTON_PLAYER3 = "Player3Button"; // For player3Button
		public const string BUTTON_PLAYER4 = "Player4Button"; // For player4Button
		public const string BUTTON_CHOOSE2 = "choice_twoplayers"; // For chooseTwoPlayersButton
		public const string BUTTON_CHOOSE3 = "choice_threeplayers"; // For chooseThreePlayersButton
		public const string BUTTON_CHOOSE4 = "choice_fourplayers"; // For chooseFourPlayersButton


		// Path texture filenames
		public const string PATH_TEXTURE_01 = "path_texture_01"; // For pathTexture1
		public const string PATH_TEXTURE_02 = "path_texture_02"; // For pathTexture2
		public const string PATH_TEXTURE_03 = "path_texture_03"; // For pathTexture3
		public const string PATH_TEXTURE_04 = "path_texture_04"; // For pathTexture4
		public const string PATH_TEXTURE_PLACEHOLDER = "path_placeholder"; // For pathTexture1, pathTexture2, pathTexture3, pathTexture4
		public const int PATH_VARIATIONS = 4;

		//// Resource paths.
		public const string RESOURCE_01 = "path_resources_01"; 
		public const string RESOURCE_02 = "path_resources_02"; 
		public const string RESOURCE_03 = "path_resources_03"; 
		public const string RESOURCE_04 = "path_resources_04";

		//// Card paths.
		public const string CARD_01 = "path_card_01";
		public const string CARD_02 = "path_card_02";
		public const string CARD_03 = "path_card_03";
		public const string CARD_04 = "path_card_04";

		//// Bond Purchase paths.
		public const string BOND_01 = "path_buybond_01";
		public const string BOND_02 = "path_buybond_02";
		public const string BOND_03 = "path_buybond_03";
		public const string BOND_04 = "path_buybond_04";

		//// Bond return paths.
		public const string BOND_RETURN_01 = "path_returnbond_01";
		public const string BOND_RETURN_02 = "path_returnbond_02";
		public const string BOND_RETURN_03 = "path_returnbond_03";
		public const string BOND_RETURN_04 = "path_returnbond_04";

		//// Empty paths.
		public const string EMPTY_01 = "path_empty_01";
		public const string EMPTY_02 = "path_empty_02";
		public const string EMPTY_03 = "path_empty_03";
		public const string EMPTY_04 = "path_empty_04";

		//// Random paths.
		public const string RANDOM_01 = "path_random_01";
		public const string RANDOM_02 = "path_random_02";
		public const string RANDOM_03 = "path_random_03";
		public const string RANDOM_04 = "path_random_04";
		
		// Role texture filenames
		public const string FOLDER_CLOSED = "FolderClosed"; // For folderClosed
		public const string FOLDER_OPEN	  = "FolderOpen";	// For folderOpen
		public const string TOP_HAT		  = "tophat";		// for topHat
		public const string PLASTIC		  = "plastic";		// for plastic
		public const string NARCISSIST	  = "narcissist";	// for narcissist
		public const string DANKEST		  = "dankest";		// for dankest

		// How to instructions filename
		public const string HOW_TO_INSTRUCTIONS = "Default how to instructions"; // For placeholder how to image
		public const string SLIDE_TEST_01 = "slide_test01";
		public const string SLIDE_TEST_02 = "slide_test02";
		public const string SLIDE_TEST_03 = "slide_test03";
		public const string SLIDE_01 = "slide_01";
		public const string SLIDE_02 = "slide_02";
		public const string SLIDE_03 = "slide_03";
		public const string SLIDE_04 = "slide_04";
		public const string SLIDE_05 = "slide_05";
		public const string SLIDE_06 = "slide_06";
		public const string SLIDE_07 = "slide_07";
		public const string SLIDE_08 = "slide_08";
		public const string SLIDE_09 = "slide_09";
		public const string SLIDE_10 = "slide_10";
		public const string SLIDE_11 = "slide_11";
		public const string SLIDE_12 = "slide_12";
		public const string SLIDE_13 = "slide_13";
		public const string SLIDE_14 = "slide_14";
		public const string SLIDE_15 = "slide_15";
		public const string SLIDE_16 = "slide_16";
		public const string SLIDE_17 = "slide_17";

		// MessageBox texture
		public const string MESSAGE_BOX = "MessageBox"; // For messageBox
		public const string TARGET_BOX  = "TargetBox"; // for targetBox
		#endregion Filepaths

		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		#region Variables
		// Necessary textures for the game
		private static Texture2D backgroundTexture; // The background texture
		private static Texture2D foregroundTexture; // The foreground texture

		// Game Fonts
		private static SpriteFont mainFont;
		private static SpriteFont displayFont;

		// Core textures
		private static Texture2D boardTexture; // The actual board's texture
		private static Texture2D playerToken1; // Player 1's token texture
		private static Texture2D playerToken2; // Player 2's token texture
		private static Texture2D playerToken3; // Player 3's token texture
		private static Texture2D playerTexture4; // Player 4's token texture

		// UI textures
		private static Texture2D playerIcon1; // Player 1's icon texture
		private static Texture2D playerIcon2; // Player 2's icon texture
		private static Texture2D playerIcon3; // Player 3's icon texture
		private static Texture2D playerIcon4; // Player 4's icon texture

		// Button textures
		private static Texture2D okButton; // Ok button texture
		private static Texture2D exitButton; // Exit button texture
		private static Texture2D howToButton; // How-To button texture
		private static Texture2D menuButton; // Menu button texture
		private static Texture2D playButton; // Play button texture
		private static Texture2D resumeButton; // Resume button texture
		private static Texture2D returnButton; // Return button texture
		private static Texture2D cancelButton; // Cancel button texture
		private static Texture2D rollButton; // Roll button texture
		private static Texture2D player1Button; // Player1 button texture
		private static Texture2D player2Button; // Player2 button texture
		private static Texture2D player3Button; // Player3 button texture
		private static Texture2D player4Button; // Player4 button texture
		private static Texture2D chooseTwoPlayersButton; // Choose two players button texture.
		private static Texture2D chooseThreePlayersButton; // Choose three players button texture.
		private static Texture2D chooseFourPlayersButton; // Choose four players button texture.

		// Path textures
		private static Texture2D pathTexture1; // Path Texture type 1
		private static Texture2D pathTexture2; // Path Texture type 2
		private static Texture2D pathTexture3; // Path Texture type 3
		private static Texture2D pathTexture4; // Path Texture type 4
		private static Texture2D[] paths; // Path array.

		// Resource path textures.
		private static Texture2D pathResources1;
		private static Texture2D pathResources2;
		private static Texture2D pathResources3;
		private static Texture2D pathResources4;
		private static Texture2D[] resourcePaths;

		private static Texture2D pathCards1;
		private static Texture2D pathCards2;
		private static Texture2D pathCards3;
		private static Texture2D pathCards4;
		private static Texture2D[] cardPaths; // Path array.

		private static Texture2D pathBonds1;
		private static Texture2D pathBonds2;
		private static Texture2D pathBonds3;
		private static Texture2D pathBonds4;
		private static Texture2D[] bondPaths; // Path array.


		private static Texture2D pathBondReturns1;
		private static Texture2D pathBondReturns2;
		private static Texture2D pathBondReturns3;
		private static Texture2D pathBondReturns4;
		private static Texture2D[] bondReturnPaths; // Path array.

		private static Texture2D pathEmpty1;
		private static Texture2D pathEmpty2;
		private static Texture2D pathEmpty3;
		private static Texture2D pathEmpty4;
		private static Texture2D[] emptyPaths; // Path array.

		private static Texture2D pathRandom1;
		private static Texture2D pathRandom2;
		private static Texture2D pathRandom3;
		private static Texture2D pathRandom4;
		private static Texture2D[] randomPaths; // Path array.

		// Role textures
		private static Texture2D folderClosed; // Folder closed texture
		private static Texture2D folderOpen;   // Folder open texture
		private static Texture2D tophat;	   // Top hat texture
		private static Texture2D plastic;	   // Plastic texture
		private static Texture2D narcissist;   // Narcissist texture
		private static Texture2D dankest;	   // Dankest texture

		// How-to textures
		private static Texture2D howToInstructions; // How to texture
		private static Texture2D test01; // test slides
		private static Texture2D test02;
		private static Texture2D test03;
		private static Texture2D slide01; // final slides.
		private static Texture2D slide02;
		private static Texture2D slide03;
		private static Texture2D slide04;
		private static Texture2D slide05;
		private static Texture2D slide06;
		private static Texture2D slide07;
		private static Texture2D slide08;
		private static Texture2D slide09;
		private static Texture2D slide10;
		private static Texture2D slide11;
		private static Texture2D slide12;
		private static Texture2D slide13;
		private static Texture2D slide14;
		private static Texture2D slide15;
		private static Texture2D slide16;
		private static Texture2D slide17;
		private static Texture2D[] slides; // all slides in an array

		// Message Box texture
		private static Texture2D messageBox; // Message box texture
		private static Texture2D targetBox; // Target box texture
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
		public static SpriteFont MainFont { get {return mainFont; } set {mainFont = value; } }
		public static SpriteFont DisplayFont { get {return displayFont; } set {displayFont = value; } }

		// Core textures.
		public static Texture2D Board { get { return boardTexture; } set { boardTexture = value; } }
		public static Texture2D Player1 { get { return playerToken1; } set { playerToken1 = value; } }
		public static Texture2D Player2 { get { return playerToken2; } set { playerToken2 = value; } }
		public static Texture2D Player3 { get { return playerToken3; } set { playerToken3 = value; } }
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
		public static Texture2D Player1Button { get { return player1Button; } set { player1Button = value; } }
		public static Texture2D Player2Button { get { return player2Button; } set { player2Button = value; } }
		public static Texture2D Player3Button { get { return player3Button; } set { player3Button = value; } }
		public static Texture2D Player4Button { get { return player4Button; } set { player4Button = value; } }
		public static Texture2D Choose2PlayersButton { get { return chooseTwoPlayersButton; } set { chooseTwoPlayersButton = value; } }
		public static Texture2D Choose3PlayersButton { get { return chooseThreePlayersButton; } set { chooseThreePlayersButton = value; } }
		public static Texture2D Choose4PlayersButton { get { return chooseFourPlayersButton; } set { chooseFourPlayersButton = value; } }

		// Path textures.
		public static Texture2D PathTexture1 { get { return pathTexture1; } set { pathTexture1 = value; } }
		public static Texture2D PathTexture2 { get { return pathTexture2; } set { pathTexture2 = value; } }
		public static Texture2D PathTexture3 { get { return pathTexture3; } set { pathTexture3 = value; } }
		public static Texture2D PathTexture4 { get { return pathTexture4; } set { pathTexture4 = value; } }
		public static Texture2D[] Paths { get { return paths; } set { paths = value; } }

		// // Resources.
		public static Texture2D Resource1 { get { return pathResources1; } set { pathResources1 = value; } }
		public static Texture2D Resource2 { get { return pathResources2; } set { pathResources2 = value; } }
		public static Texture2D Resource3 { get { return pathResources3; } set { pathResources3 = value; } }
		public static Texture2D Resource4 { get { return pathResources4; } set { pathResources4 = value; } }
		public static Texture2D[] Resources { get { return resourcePaths; } set { resourcePaths = value; } }
		
		// // Cards.
		public static Texture2D Cards1 { get { return pathCards1; } set { pathCards1 = value; } }
		public static Texture2D Cards2 { get { return pathCards2; } set { pathCards2 = value; } }
		public static Texture2D Cards3 { get { return pathCards3; } set { pathCards3 = value; } }
		public static Texture2D Cards4 { get { return pathCards4; } set { pathCards4 = value; } }
		public static Texture2D[] Cards { get { return cardPaths; } set { cardPaths = value; } }

		// // Buy bonds.
		public static Texture2D Bonds1 { get { return pathBonds1; } set { pathBonds1 = value; } }
		public static Texture2D Bonds2 { get { return pathBonds2; } set { pathBonds2 = value; } }
		public static Texture2D Bonds3 { get { return pathBonds3; } set { pathBonds3 = value; } }
		public static Texture2D Bonds4 { get { return pathBonds4; } set { pathBonds4 = value; } }
		public static Texture2D[] Bonds { get { return bondPaths; } set { bondPaths = value; } }
		
		// // Reclaim bonds.
		public static Texture2D BondReclaims1 { get { return pathBondReturns1; } set { pathBondReturns1 = value; } }
		public static Texture2D BondReclaims2 { get { return pathBondReturns2; } set { pathBondReturns2 = value; } }
		public static Texture2D BondReclaims3 { get { return pathBondReturns3; } set { pathBondReturns3 = value; } }
		public static Texture2D BondReclaims4 { get { return pathBondReturns4; } set { pathBondReturns4 = value; } }
		public static Texture2D[] BondReclaims { get { return bondReturnPaths; } set { bondReturnPaths = value; } }

		// // Empty.
		public static Texture2D Empty1 { get { return pathEmpty1; } set { pathEmpty1 = value; } }
		public static Texture2D Empty2 { get { return pathEmpty2; } set { pathEmpty2 = value; } }
		public static Texture2D Empty3 { get { return pathEmpty3; } set { pathEmpty3 = value; } }
		public static Texture2D Empty4 { get { return pathEmpty4; } set { pathEmpty4 = value; } }
		public static Texture2D[] EmptyPaths { get { return emptyPaths; } set { emptyPaths = value; } }

		// // Random.
		public static Texture2D Random1 { get { return pathRandom1; } set { pathRandom1 = value; } }
		public static Texture2D Random2 { get { return pathRandom2; } set { pathRandom2 = value; } }
		public static Texture2D Random3 { get { return pathRandom3; } set { pathRandom3 = value; } }
		public static Texture2D Random4 { get { return pathRandom4; } set { pathRandom4 = value; } }
		public static Texture2D[] RandomPaths { get { return randomPaths; } set { randomPaths = value; } }
		
		// Role textures
		public static Texture2D FolderClosed { get { return folderClosed; } set { folderClosed = value; } }
		public static Texture2D FolderOpen { get { return folderOpen; } set { folderOpen = value; } }
		public static Texture2D TopHat { get { return tophat; } set { tophat = value; } }
		public static Texture2D Plastic { get { return plastic; } set { plastic = value; } }
		public static Texture2D Narcissist { get { return narcissist; } set { narcissist = value; } }
		public static Texture2D Dankest { get { return dankest; } set { dankest = value; } }

		// How-to textures.
		public static Texture2D HowToInstructions { get { return howToInstructions; } set { howToInstructions = value; } }
		public static Texture2D Test01 { get { return test01; } set { test01 = value; } }
		public static Texture2D Test02 { get { return test02; } set { test02 = value; } }
		public static Texture2D Test03 { get { return test03; } set { test03 = value; } }
		public static Texture2D Slide01 { get { return slide01; } set { slide01 = value; } }
		public static Texture2D Slide02 { get { return slide02; } set { slide02 = value; } }
		public static Texture2D Slide03 { get { return slide03; } set { slide03 = value; } }
		public static Texture2D Slide04 { get { return slide04; } set { slide04 = value; } }
		public static Texture2D Slide05 { get { return slide05; } set { slide05 = value; } }
		public static Texture2D Slide06 { get { return slide06; } set { slide06 = value; } }
		public static Texture2D Slide07 { get { return slide07; } set { slide07 = value; } }
		public static Texture2D Slide08 { get { return slide08; } set { slide08 = value; } }
		public static Texture2D Slide09 { get { return slide09; } set { slide09 = value; } }
		public static Texture2D Slide10 { get { return slide10; } set { slide10 = value; } }
		public static Texture2D Slide11 { get { return slide11; } set { slide11 = value; } }
		public static Texture2D Slide12 { get { return slide12; } set { slide12 = value; } }
		public static Texture2D Slide13 { get { return slide13; } set { slide13 = value; } }
		public static Texture2D Slide14 { get { return slide14; } set { slide14 = value; } }
		public static Texture2D Slide15 { get { return slide15; } set { slide15 = value; } }
		public static Texture2D Slide16 { get { return slide16; } set { slide16 = value; } }
		public static Texture2D Slide17 { get { return slide17; } set { slide17 = value; } }
		public static Texture2D[] Slides { get { return slides; } set { slides = value; } }

		// Message box textures.
		public static Texture2D MessageBox { get { return messageBox; } set { messageBox = value; } }
		public static Texture2D TargetBox { get { return targetBox; } set { targetBox = value; } }
		#endregion Properties

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

		#region Methods
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
				MainFont    = Content.Load<SpriteFont>(MAIN_FONT);
				DisplayFont = Content.Load<SpriteFont>(DISPLAY_FONT);

				// ===============================================================================================================
				// ================================================ TEMPORARY ====================================================
				// ===============================================================================================================

				// Core textures.
				Board   = Content.Load<Texture2D>(BOARD_TEXTURE);
				Player1 = Content.Load<Texture2D>(PLAYER_TOKEN_01);
				Player2 = Content.Load<Texture2D>(PLAYER_TOKEN_02);
				Player3 = Content.Load<Texture2D>(PLAYER_TOKEN_03);
				Player4 = Content.Load<Texture2D>(PLAYER_TOKEN_04);

				// UI textures.
				PlayerIcon1 = Content.Load<Texture2D>(PLAYER_ICON_01);
				PlayerIcon2 = Content.Load<Texture2D>(PLAYER_ICON_02);
				PlayerIcon3 = Content.Load<Texture2D>(PLAYER_ICON_03);
				PlayerIcon4 = Content.Load<Texture2D>(PLAYER_ICON_04);

				// Button textures.
				OkButton        = Content.Load<Texture2D>(BUTTON_OK);
				ExitButton      = Content.Load<Texture2D>(BUTTON_EXIT);
				HowToButton     = Content.Load<Texture2D>(BUTTON_HOW_TO);
				MenuButton      = Content.Load<Texture2D>(BUTTON_MENU);
				PlayButton      = Content.Load<Texture2D>(BUTTON_PLAY);
				ResumeButton    = Content.Load<Texture2D>(BUTTON_RESUME);
				ReturnButton    = Content.Load<Texture2D>(BUTTON_RETURN);
				CancelButton    = Content.Load<Texture2D>(BUTTON_CANCEL);
				RollButton      = Content.Load<Texture2D>(BUTTON_ROLL);
				Player1Button   = Content.Load<Texture2D>(BUTTON_PLAYER1);
				Player2Button   = Content.Load<Texture2D>(BUTTON_PLAYER2);
				Player3Button   = Content.Load<Texture2D>(BUTTON_PLAYER3);
				Player4Button   = Content.Load<Texture2D>(BUTTON_PLAYER4);
				Choose2PlayersButton = Content.Load<Texture2D>(BUTTON_CHOOSE2);
				Choose3PlayersButton = Content.Load<Texture2D>(BUTTON_CHOOSE3);
				Choose4PlayersButton = Content.Load<Texture2D>(BUTTON_CHOOSE4);

				// Path textures.
				PathTexture1 = Content.Load<Texture2D>(PATH_TEXTURE_01);
				PathTexture2 = Content.Load<Texture2D>(PATH_TEXTURE_02);
				PathTexture3 = Content.Load<Texture2D>(PATH_TEXTURE_03);
				PathTexture4 = Content.Load<Texture2D>(PATH_TEXTURE_04);
				Paths = new Texture2D[] { pathTexture1, pathTexture2, pathTexture3, pathTexture4 };

				// // Resources
				Resource1 = Content.Load<Texture2D>(RESOURCE_01);
				Resource2 = Content.Load<Texture2D>(RESOURCE_02);
				Resource3 = Content.Load<Texture2D>(RESOURCE_03);
				Resource4 = Content.Load<Texture2D>(RESOURCE_04);
				Resources = new Texture2D[] { Resource1, Resource2, Resource3, Resource4 };

				// // Cards
				Cards1 = Content.Load<Texture2D>(CARD_01);
				Cards2 = Content.Load<Texture2D>(CARD_01);
				Cards3 = Content.Load<Texture2D>(CARD_01);
				Cards4 = Content.Load<Texture2D>(CARD_01);
				Cards = new Texture2D[] { Cards1, Cards2, Cards3, Cards4 };

				// // Bonds
				Bonds1 = Content.Load<Texture2D>(BOND_01);
				Bonds2 = Content.Load<Texture2D>(BOND_02);
				Bonds3 = Content.Load<Texture2D>(BOND_03);
				Bonds4 = Content.Load<Texture2D>(BOND_04);
				Bonds = new Texture2D[] { Bonds1, Bonds2, Bonds3, Bonds4 };

				// // Bond returns
				BondReclaims1 = Content.Load<Texture2D>(BOND_RETURN_01);
				BondReclaims2 = Content.Load<Texture2D>(BOND_RETURN_02);
				BondReclaims3 = Content.Load<Texture2D>(BOND_RETURN_03);
				BondReclaims4 = Content.Load<Texture2D>(BOND_RETURN_04);
				BondReclaims = new Texture2D[] { BondReclaims1, BondReclaims2, BondReclaims3, BondReclaims4};

				// // Empty
				Empty1 = Content.Load<Texture2D>(EMPTY_01);
				Empty2 = Content.Load<Texture2D>(EMPTY_02);
				Empty3 = Content.Load<Texture2D>(EMPTY_03);
				Empty4 = Content.Load<Texture2D>(EMPTY_04);
				EmptyPaths = new Texture2D[] { Empty1, Empty2, Empty3, Empty4 };

				// // Random
				Random1 = Content.Load<Texture2D>(RANDOM_04);
				Random2 = Content.Load<Texture2D>(RANDOM_04);
				Random3 = Content.Load<Texture2D>(RANDOM_04);
				Random4 = Content.Load<Texture2D>(RANDOM_04);
				RandomPaths = new Texture2D[] { Random1, Random2, Random3, Random4 };
				
				// Role textures
				FolderClosed = Content.Load<Texture2D>(FOLDER_CLOSED);
				FolderOpen   = Content.Load<Texture2D>(FOLDER_OPEN);
				TopHat       = Content.Load<Texture2D>(TOP_HAT);
				Plastic      = Content.Load<Texture2D>(PLASTIC);
				Narcissist   = Content.Load<Texture2D>(NARCISSIST);
				Dankest      = Content.Load<Texture2D>(DANKEST);

				// how-to texture
				HowToInstructions = Content.Load<Texture2D>(HOW_TO_INSTRUCTIONS);
				Test01 = Content.Load<Texture2D>(SLIDE_TEST_01);
				Test02 = Content.Load<Texture2D>(SLIDE_TEST_02);
				Test03 = Content.Load<Texture2D>(SLIDE_TEST_03);
				Slide01 = Content.Load<Texture2D>(SLIDE_01);
				Slide02 = Content.Load<Texture2D>(SLIDE_02);
				Slide03 = Content.Load<Texture2D>(SLIDE_03);
				Slide04 = Content.Load<Texture2D>(SLIDE_04);
				Slide05 = Content.Load<Texture2D>(SLIDE_05);
				Slide06 = Content.Load<Texture2D>(SLIDE_06);
				Slide07 = Content.Load<Texture2D>(SLIDE_07);
				Slide08 = Content.Load<Texture2D>(SLIDE_08);
				Slide09 = Content.Load<Texture2D>(SLIDE_09);
				Slide10 = Content.Load<Texture2D>(SLIDE_10);
				Slide11 = Content.Load<Texture2D>(SLIDE_11);
				Slide12 = Content.Load<Texture2D>(SLIDE_12);
				Slide13 = Content.Load<Texture2D>(SLIDE_13);
				Slide14 = Content.Load<Texture2D>(SLIDE_14);
				Slide15 = Content.Load<Texture2D>(SLIDE_15);
				Slide16 = Content.Load<Texture2D>(SLIDE_16);
				Slide17 = Content.Load<Texture2D>(SLIDE_17);
				slides = new Texture2D[] { Slide01, Slide02, Slide03,
											Slide04, Slide05, Slide06,
											Slide07, Slide08, Slide09,
											Slide10, Slide11, Slide12,
											Slide13, Slide14, Slide15,
											Slide16, Slide17, };

				// Message Box texture
				MessageBox = Content.Load<Texture2D>(MESSAGE_BOX);
				TargetBox  = Content.Load<Texture2D>(TARGET_BOX);
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
			MainFont    = Content.Load<SpriteFont>(MAIN_FONT);
			DisplayFont = Content.Load<SpriteFont>(DISPLAY_FONT);

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
			OkButton        = Content.Load<Texture2D>(BUTTON_OK);
			ExitButton      = Content.Load<Texture2D>(BUTTON_EXIT);
			HowToButton     = Content.Load<Texture2D>(BUTTON_HOW_TO);
			MenuButton      = Content.Load<Texture2D>(BUTTON_MENU);
			PlayButton      = Content.Load<Texture2D>(BUTTON_PLAY);
			ResumeButton    = Content.Load<Texture2D>(BUTTON_RESUME);
			ReturnButton    = Content.Load<Texture2D>(BUTTON_RETURN);
			CancelButton    = Content.Load<Texture2D>(BUTTON_CANCEL);
			RollButton      = Content.Load<Texture2D>(BUTTON_ROLL);
			Player1Button   = Content.Load<Texture2D>(BUTTON_PLAYER1);
			Player2Button   = Content.Load<Texture2D>(BUTTON_PLAYER2);
			Player3Button   = Content.Load<Texture2D>(BUTTON_PLAYER3);
			Player4Button   = Content.Load<Texture2D>(BUTTON_PLAYER4);
			Choose2PlayersButton = Content.Load<Texture2D>(BUTTON_CHOOSE2);
			Choose3PlayersButton = Content.Load<Texture2D>(BUTTON_CHOOSE3);
			Choose4PlayersButton = Content.Load<Texture2D>(BUTTON_CHOOSE4);

			// Path textures.
			PathTexture1 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			PathTexture2 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			PathTexture3 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			PathTexture4 = Content.Load<Texture2D>(PATH_TEXTURE_PLACEHOLDER);
			Paths = new Texture2D[] { pathTexture1, pathTexture2, pathTexture3, pathTexture4 };

			// // Resources
			Resource1 = Content.Load<Texture2D>(RESOURCE_01);
			Resource2 = Content.Load<Texture2D>(RESOURCE_02);
			Resource3 = Content.Load<Texture2D>(RESOURCE_03);
			Resource4 = Content.Load<Texture2D>(RESOURCE_04);
			Resources = new Texture2D[] { Resource1, Resource2, Resource3, Resource4 };

			// // Cards
			Cards1 = Content.Load<Texture2D>(CARD_01);
			Cards2 = Content.Load<Texture2D>(CARD_01);
			Cards3 = Content.Load<Texture2D>(CARD_01);
			Cards4 = Content.Load<Texture2D>(CARD_01);
			Cards = new Texture2D[] { Cards1, Cards2, Cards3, Cards4 };

			// // Bonds
			Bonds1 = Content.Load<Texture2D>(BOND_01);
			Bonds2 = Content.Load<Texture2D>(BOND_02);
			Bonds3 = Content.Load<Texture2D>(BOND_03);
			Bonds4 = Content.Load<Texture2D>(BOND_04);
			Bonds = new Texture2D[] { Bonds1, Bonds2, Bonds3, Bonds4 };

			// // Bond returns
			BondReclaims1 = Content.Load<Texture2D>(BOND_RETURN_01);
			BondReclaims2 = Content.Load<Texture2D>(BOND_RETURN_02);
			BondReclaims3 = Content.Load<Texture2D>(BOND_RETURN_03);
			BondReclaims4 = Content.Load<Texture2D>(BOND_RETURN_04);
			BondReclaims = new Texture2D[] { BondReclaims1, BondReclaims2, BondReclaims3, BondReclaims4 };

			// // Empty
			Empty1 = Content.Load<Texture2D>(EMPTY_01);
			Empty2 = Content.Load<Texture2D>(EMPTY_02);
			Empty3 = Content.Load<Texture2D>(EMPTY_03);
			Empty4 = Content.Load<Texture2D>(EMPTY_04);
			EmptyPaths = new Texture2D[] { Empty1, Empty2, Empty3, Empty4 };

			// // Random
			Random1 = Content.Load<Texture2D>(RANDOM_04);
			Random2 = Content.Load<Texture2D>(RANDOM_04);
			Random3 = Content.Load<Texture2D>(RANDOM_04);
			Random4 = Content.Load<Texture2D>(RANDOM_04);
			RandomPaths = new Texture2D[] { Random1, Random2, Random3, Random4 };

			// Role textures
			FolderClosed = Content.Load<Texture2D>(FOLDER_CLOSED);
			FolderOpen   = Content.Load<Texture2D>(FOLDER_OPEN);
			TopHat       = Content.Load<Texture2D>(TOP_HAT);
			Plastic      = Content.Load<Texture2D>(PLASTIC);
			Narcissist   = Content.Load<Texture2D>(NARCISSIST);
			Dankest      = Content.Load<Texture2D>(DANKEST);

			// How-to texture
			HowToInstructions = Content.Load<Texture2D>(HOW_TO_INSTRUCTIONS);
			Test01 = Content.Load<Texture2D>(SLIDE_TEST_01);
			Test02 = Content.Load<Texture2D>(SLIDE_TEST_02);
			Test03 = Content.Load<Texture2D>(SLIDE_TEST_03);
			slides = new Texture2D[] { Test01, Test02, Test03 };

			// Message Box texture
			MessageBox = Content.Load<Texture2D>(MESSAGE_BOX);
			TargetBox  = Content.Load<Texture2D>(TARGET_BOX);
		}

		// Gets a variation of a resource path.
		public static Texture2D GetResourcePathTexture()
		{
			return Resources[GameManager.RandomGen.Next(0, Resources.Length)];
		}

		// Gets a variation of a card path.
		public static Texture2D GetCardPathTexture()
		{
			return Cards[GameManager.RandomGen.Next(0, Cards.Length)];
		}

		// Gets a variation of a bond path.
		public static Texture2D GetBondPathTexture()
		{
			return Bonds[GameManager.RandomGen.Next(0, Bonds.Length)];
		}

		// Gets a variation of a bond return path.
		public static Texture2D GetBondReturnPathTexture()
		{
			return BondReclaims[GameManager.RandomGen.Next(0, BondReclaims.Length)];
		}

		// Gets a variation of a empty path.
		public static Texture2D GetEmptyPathTexture()
		{
			return EmptyPaths[GameManager.RandomGen.Next(0, EmptyPaths.Length)];
		}

		// Gets a variation of a random path.
		public static Texture2D GetRandomPathTexture()
		{
			return RandomPaths[GameManager.RandomGen.Next(0, RandomPaths.Length)];
		}






		#endregion Methods

	}
}