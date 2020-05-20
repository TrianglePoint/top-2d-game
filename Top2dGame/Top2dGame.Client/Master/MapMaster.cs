using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Tile;

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
		public Dictionary<string, IList<TileGameObject>> GameMaps { get; set; }
		
		/// <summary>
		/// Game tile list
		/// </summary>
		private IList<TileGameObject> CurrentGameMap { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private MapMaster()
		{
			GameMaps = new Dictionary<string, IList<TileGameObject>>
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
		public IList<TileGameObject> GetMap(string mapName)
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
		public IList<TileGameObject> GetCurrentMap()
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
		private IList<TileGameObject> CreateGameMap1(int horizontal, int vertical)
		{
			IList<TileGameObject> gameMap = new List<TileGameObject>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					int num = new System.Random().Next(10);
					if ((x == 2 && y == 4) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 2 && y == 3) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 0 && y == 0) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 1 && y == 2) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
				}
			}

			CreaeSpace(gameMap);

			foreach (TileGameObject gameTile in gameMap)
			{
				if (gameTile.X == 0 && gameTile.Y == 0)
				{
					gameTile.Terrain = new StairGameObject
					{
						ToGameMapName = "map2",
						ToX = 2, 
						ToY = 4
					};
				}
				else if (gameTile.X == 1 && gameTile.Y == 2)
				{
					gameTile.Terrain = new WallGameObject();
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
		private IList<TileGameObject> CreateGameMap2(int horizontal, int vertical)
		{
			IList<TileGameObject> gameMap = new List<TileGameObject>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					int num = new System.Random().Next(10);
					if ((x == 2 && y == 4) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 6 && y == 6) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 2 && y == 1) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 5 && y == 5) || num != 0)
					{
						gameMap.Add(new TileGameObject
						{
							X = x,
							Y = y
						});
					}
				}
			}

			CreaeSpace(gameMap);

			foreach (TileGameObject gameTile in gameMap)
			{
				if (gameTile.X == 6 && gameTile.Y == 6)
				{
					gameTile.Terrain = new StairGameObject
					{
						ToGameMapName = "map1",
						ToX = 2,
						ToY = 4
					};
				}
				else if (gameTile.X == 2 && gameTile.Y == 1)
				{
					gameTile.Terrain = new WallGameObject();
				}
				else if (gameTile.X == 5 && gameTile.Y == 5)
				{
					gameTile.Terrain = new ExitGameObject();
				}
			}

			return gameMap;
		}

		/// <summary>
		/// Create space.
		/// </summary>
		/// <param name="gameMap">Game map</param>
		private void CreaeSpace(IList<TileGameObject> gameMap)
		{
			// TODO Use other way. (Random, import from file)
			foreach (TileGameObject gameTile in gameMap)
			{
				gameTile.Space = new SpaceGameObject();
			}
		}
	}
}
