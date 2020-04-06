using System.Collections.Generic;
using Top2dGame.Model.Container;
using Top2dGame.Model.GameObject;

namespace Top2dGame.Client.Master
{
	// TODO Separate each function as other class if you can.

	/// <summary>
	/// Game master (singleton)
	/// </summary>
	public sealed class GameMaster
	{
		/// <summary>
		/// Instance (singleton)
		/// </summary>
		private static readonly GameMaster Instance = new GameMaster();
		/// <summary>
		/// Game tile list
		/// </summary>
		private IList<GameTile> GameMap { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private GameMaster() { }

		/// <summary>
		/// Get instance
		/// </summary>
		/// <returns>Instance</returns>
		public static GameMaster GetInstance()
		{
			return Instance;
		}

		/// <summary>
		/// Start the game
		/// </summary>
		public void GameStart()
		{
			// TODO Create Map info file
			int width = 20;
			int height = 20;

			CreateGameMap(width, height);
		}

		/// <summary>
		/// Create game map
		/// </summary>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		private void CreateGameMap(int horizontal, int vertical)
		{
			GameMap = new List<GameTile>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					GameMap.Add(new GameTile(x, y));
				}
			}

			CreaeSpace();
		}

		/// <summary>
		/// Get game tile
		/// </summary>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <returns>Game tile</returns>
		public GameTile GetGameTile(int x, int y)
		{
			if (GameMap == null)
			{
				// TODO Print text "No Game map!" in textLog section

				return null;
			}

			foreach (GameTile gameTile in GameMap)
			{
				if (gameTile.X == x && gameTile.Y == y)
				{
					return gameTile;
				}
			}

			return null;
		}

		/// <summary>
		/// Place character
		/// </summary>
		/// <param name="character">Character</param>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <returns>Placed game tile</returns>
		private void CreaeSpace()
		{
			// TODO Use other way. (Random, import from file)
			foreach (GameTile gameTile in GameMap)
			{
				gameTile.Space = new Space();
			}
		}

		/// <summary>
		/// Place character
		/// </summary>
		/// <param name="character">Character</param>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <returns>Placed game tile</returns>
		public GameTile PlaceCharacter(Character character, int x, int y)
		{
			GameTile gameTile = GetGameTile(x, y);

			if (gameTile == null)
			{
				// TODO Print text "Failed Init character" in textLog section
				
				return null;
			}

			if (gameTile.Space == null)
			{
				// TODO Print text "Character is must exist on space!" in textLog section

				return null;
			}

			// TODO Process Terrain case. (if terrain is wall, can't place when usually)

			if (gameTile.Character != null)
			{
				// TODO Print text "There is other character" in textLog section

				return null;
			}

			gameTile.Character = character;

			// Move from tile to other tile
			if (character.GameTile != null)
			{
				character.GameTile.Character = null;
			}

			character.GameTile = gameTile;

			return gameTile;
		}
	}
}
