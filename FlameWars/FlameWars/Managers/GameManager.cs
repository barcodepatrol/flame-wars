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
			windowCenter = GetElementCenter(w, h);
		}

		// Service method.
		// Call these for quick calculations in other methods.

		// GetElementCenter() returns a center Vector2 value,
		// with no regards to the elements' (X,Y) position.
		// This treats it as if it is in the origin of (0,0).
		// In other words this method is asking for the element's origin.
		public static Vector2 GetElementCenter(int w, int h)
		{
			Vector2 centerOfElement = new Vector2(w / 2, h / 2);
			return centerOfElement;
		}

		// GetElementCenterPoint() returns a center Vector2 value,
		// with regards to the elements' (X,Y) position.
		// The returned value can be used, say, to draw to a particular point.
		public static Vector2 GetElementCenterPoint(int x, int y, int w, int h)
		{
			Vector2 position = new Vector2(x, y); // Gets a vector of the element's current position.
			Vector2 origin = GetElementCenter(w, h); // Gets a vector with no regards to the current position.
			Vector2 center = position - origin; // The center vector will be a vector addition of the position + a negative origin.
			return center;
		}
    }
}