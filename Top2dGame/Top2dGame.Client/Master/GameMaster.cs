using System.Collections.Generic;
using Top2dGame.Model.Container;
using Top2dGame.Model.GameObjects;
using Top2dGame.Model.GameObjects.Terrains;

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
			GameMap = MapMaster.GetInstance().GetGameMap("map1s");
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
		public GameTile PlaceCharacter(Character character, int x, int y)
		{
			bool canPlace = true;
			GameTile gameTile = GetGameTile(x, y);

			if (gameTile == null)
			{
				// TODO Print text "Failed Init character" in textLog section

				canPlace = false;
			}
			else if (gameTile.Space == null)
			{
				// TODO Print text "Character is must exist on space!" in textLog section

				canPlace = false;
			}
			// TODO Process Terrain case. (if terrain is wall, can't place when usually)
			else if (gameTile.Terrain != null)
			{
				if (gameTile.Terrain is Wall)
				{
					canPlace = false;
				}
				else if (gameTile.Terrain is Stair stair)
				{
					canPlace = false;
					// Move through stair
					PlaceCharacter(character, stair.ToX, stair.ToY);
				}
			}
			else if (gameTile.Character != null)
			{
				// TODO Print text "There is other character" in textLog section

				canPlace = false;
			}

			if (canPlace)
			{
				// Place character
				gameTile.Character = character;

				// Move from tile to other tile
				if (character.GameTile != null)
				{
					// Before location info
					character.GameTile.Character = null;
				}
				// After location info
				character.GameTile = gameTile;
			}

			return gameTile;
		}
	}
}
