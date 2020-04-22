using System.Collections.Generic;
using Top2dGame.Model.Container;
using Top2dGame.Model.GameObjects;
using Top2dGame.Model.GameObjects.Terrains;

namespace Top2dGame.Client.Master
{
	/// <summary>
	/// Map master
	/// </summary>
	public sealed class MapMaster
	{
		/// <summary>
		/// Instance
		/// </summary>
		private static readonly MapMaster Instance = new MapMaster();

		/// <summary>
		/// Game maps
		/// </summary>
		public Dictionary<string, IList<GameTile>> GameMaps { get; set; }
		
		/// <summary>
		/// Game tile list
		/// </summary>
		private IList<GameTile> CurrentGameMap { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private MapMaster()
		{
			GameMaps = new Dictionary<string, IList<GameTile>>
			{
				// TODO this is temp process. add load map from file.
				{ "map1", CreateGameMap1(20, 20) },
				{ "map2", CreateGameMap2(10, 15) }
			};
		}

		/// <summary>
		/// Get instance
		/// </summary>
		/// <returns>Instance</returns>
		public static MapMaster GetInstance()
		{
			return Instance;
		}

		/// <summary>
		/// Get game map
		/// </summary>
		/// <param name="mapName">Map name</param>
		/// <returns>Current game map</returns>
		public IList<GameTile> GetMap(string mapName)
		{
			return GameMaps[mapName];
		}

		/// <summary>
		/// Set current game map
		/// </summary>
		/// <param name="mapName">Map name</param>
		public void SetCurrentMap(string mapName)
		{
			CurrentGameMap = GameMaps[mapName];
		}

		/// <summary>
		/// Get current game map
		/// </summary>
		/// <returns>Current game map</returns>
		public IList<GameTile> GetCurrentMap()
		{
			return CurrentGameMap;
		}

		// TODO import from file
		/// <summary>
		/// Create game map
		/// </summary>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		/// <returns>Game map</returns>
		private IList<GameTile> CreateGameMap1(int horizontal, int vertical)
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
					else if ((x == 1 && y == 2) || num != 0)
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
					gameTile.Terrain = new Stair("map2", 2, 4);
				}
				else if (gameTile.X == 1 && gameTile.Y == 2)
				{
					gameTile.Terrain = new Wall();
				}
			}

			return gameMap;
		}

		// TODO import from file
		/// <summary>
		/// Create game map
		/// </summary>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		/// <returns>Game map</returns>
		private IList<GameTile> CreateGameMap2(int horizontal, int vertical)
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
					else if ((x == 6 && y == 6) || num != 0)
					{
						gameMap.Add(new GameTile(x, y));
					}
					else if ((x == 2 && y == 1) || num != 0)
					{
						gameMap.Add(new GameTile(x, y));
					}
					else if ((x == 5 && y == 5) || num != 0)
					{
						gameMap.Add(new GameTile(x, y));
					}
				}
			}

			CreaeSpace(gameMap);

			foreach (GameTile gameTile in gameMap)
			{
				if (gameTile.X == 6 && gameTile.Y == 6)
				{
					gameTile.Terrain = new Stair("map1", 2, 4);
				}
				else if (gameTile.X == 2 && gameTile.Y == 1)
				{
					gameTile.Terrain = new Wall();
				}
				else if (gameTile.X == 5 && gameTile.Y == 5)
				{
					gameTile.Terrain = new Exit();
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
