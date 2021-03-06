﻿namespace FlameWars
{
    internal static class StateManager
    {
		// ============================================================================
		// ================================ Variables =================================
		// ============================================================================

		public enum GameState
		{
			Start,
			Role,
			Menu,
			HowTo,
			Pause,
			Game,
			GameOver,
			Exit,
			Reset
		}
		public static GameState gameState = GameState.Menu;
		public static GameState lastState;

		// ============================================================================
		// ================================= Methods ==================================
		// ============================================================================
    }
}