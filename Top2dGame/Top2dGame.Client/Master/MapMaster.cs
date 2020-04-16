using System.Collections.Generic;
using Top2dGame.Model.Container;
using Top2dGame.Model.GameObjects;
using Top2dGame.Model.GameObjects.Terrains;

namespace Top2dGame.Client.Master
{
	/// <summary>
	/// Map master (singleton)
	/// </summary>
	public sealed class MapMaster
	{
		/// <summary>
		/// Instance (singleton)
		/// </summary>
		private static readonly MapMaster Instance = new MapMaster();

		/// <summary>
		/// Constructor
		/// </summary>
		private MapMaster() { }

		/// <summary>
		/// Get instance
		/// </summary>
		/// <returns>Instance</returns>
		public static MapMaster GetInstance()
		{
			return Instance;
		}

		public IList<GameTile> GetGameMap(string mapName)
		{
			// TODO this is temp process. add load map from file.
			if (mapName == "map1")
			{
				return CreateGameMap(20, 20);
			}
			else
			{
				return CreateGameMap(10, 15);
			}
		}

		// TODO import from file
		/// <summary>
		/// Create game map
		/// </summary>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		/// <returns>Game map</returns>
		private IList<GameTile> CreateGameMap(int horizontal, int vertical)
		{
			IList<GameTile> gameMap = new List<GameTile>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					int num = new System.Random().Next(10);
					if ((x == 2 && y == 4) || num != 0)
					{
						gameMap.Add(new GameTile(x, y));
					}
					else if ((x == 0 && y == 0) || num != 0)
					{
						gameMap.Add(new GameTile(x, y));
					}
					else if ((x == 2 && y == 1) || num != 0)
					{
						gameMap.Add(new GameTile(x, y));
					}
				}
			}

			CreaeSpace(gameMap);

			foreach (GameTile gameTile in gameMap)
			{
				if (gameTile.X == 0 && gameTile.Y == 0)
				{
					gameTile.Terrain = new Stair(gameMap, 2, 4);
				}
				else if (gameTile.X == 2 && gameTile.Y == 1)
				{
					gameTile.Terrain = new Wall();
				}
			}

			return gameMap;
		}

		/// <summary>
		/// Create space.
		/// </summary>
		/// <param name="gameMap">Game map</param>
		private void CreaeSpace(IList<GameTile> gameMap)
		{
			// TODO Use other way. (Random, import from file)
			foreach (GameTile gameTile in gameMap)
			{
				gameTile.Space = new Space();
			}
		}
	}
}
