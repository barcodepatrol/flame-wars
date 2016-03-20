using Microsoft.Xna.Framework;
namespace FlameWars
{
    internal static class GameManager
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		// These two ints store the height and width of the board
		private static int windowWidth, windowHeight;

		//* Properties *//
		// Stores the window width
		public static int Width
		{
			get { return windowWidth; }
			set { windowWidth = value; }
		}

		// Stores the window height
		public static int Height
		{
			get { return windowHeight; }
			set { windowHeight = value; }
		}

		// These Vectors store important positions in the board.

		// Stores the window's center.
		private static Vector2 windowCenter;

		//* Properties *//
		// Stores the window center position.
		public static Vector2 Center
		{
			get { return windowCenter; }
			set { windowCenter = value; }
		}

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================

        // This method initializes the class
		public static void Init(int w, int h)
		{
			windowWidth  = w;
			windowHeight = h;
			windowCenter = new Vector2(w / 2, h / 2);
		}
    }
}