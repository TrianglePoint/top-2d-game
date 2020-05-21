using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
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
		public Dictionary<string, IList<GameObject>> GameMaps { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private MapMaster()
		{
			GameMaps = new Dictionary<string, IList<GameObject>>
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
		public IList<GameObject> GetMap(string mapName)
		{
			return GameMaps[mapName];
		}

		// TODO import from file
		/// <summary>
		/// Create game map
		/// </summary>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		/// <returns>Game map</returns>
		private IList<GameObject> CreateGameMap1(int horizontal, int vertical)
		{
			IList<GameObject> gameMap = new List<GameObject>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					if ((x == 2 && y == 4))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 2 && y == 3))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 0 && y == 0))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
						gameMap.Add(new StairGameObject
						{
							X = x,
							Y = y,
							ToGameMapName = "map2",
							ToX = 2,
							ToY = 4
						});
					}
					else if ((x == 1 && y == 2))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
						gameMap.Add(new WallGameObject
						{
							X = x,
							Y = y
						});
					}
					else if (new System.Random().Next(10) != 0)
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
					}
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
		private IList<GameObject> CreateGameMap2(int horizontal, int vertical)
		{
			IList<GameObject> gameMap = new List<GameObject>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					if ((x == 2 && y == 4))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 6 && y == 6))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
						gameMap.Add(new StairGameObject
						{
							X = x,
							Y = y,
							ToGameMapName = "map1",
							ToX = 2,
							ToY = 4
						});
					}
					else if ((x == 2 && y == 1))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
						gameMap.Add(new WallGameObject
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 5 && y == 5))
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
						gameMap.Add(new ExitGameObject
						{
							X = x,
							Y = y
						});
					}
					else if (new System.Random().Next(10) != 0)
					{
						gameMap.Add(new SpaceGameObject
						{
							X = x,
							Y = y
						});
					}
				}
			}

			return gameMap;
		}
	}
}
