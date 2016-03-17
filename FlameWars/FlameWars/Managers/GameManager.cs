namespace FlameWars
{
    internal static class GameManager
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		// These two ints store the height and width of the board
		static int windowWidth, windowHeight;

		//* Properties *//
		// Stores the window width
		public static int winW
		{
			get { return windowWidth; }
			set { windowWidth = value; }
		}
		// Stores the window height
		public static int winH
		{
			get { return windowHeight; }
			set { windowHeight = value; }
		}

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

        // This method initializes the class
		public static void Init(int w, int h)
		{
			windowWidth  = w;
			windowHeight = h;
		}
    }
}