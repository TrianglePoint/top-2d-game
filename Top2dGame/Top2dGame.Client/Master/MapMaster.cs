using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Enemy;
using Top2dGame.Client.GameObjects.Space;
using Top2dGame.Client.GameObjects.Terrain;
using Top2dGame.Client.Scripts.Character;

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
		/// Current map name
		/// </summary>
		public string CurrentMapName { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private MapMaster()
		{
			// TODO this is temp process. add load map from file.
			string map1Name = "map1";
			string map2Name = "map2";

			GameMaps = new Dictionary<string, IList<GameObject>>
			{
				// TODO this is temp process. add load map from file.
				{ map1Name, CreateGameMap1(map1Name, 20, 20) },
				{ map2Name, CreateGameMap2(map2Name, 10, 15) }
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
		/// <returns>Game map</returns>
		public IList<GameObject> GetMap(string mapName)
		{
			return GameMaps[mapName];
		}

		/// <summary>
		/// Get current game map
		/// </summary>
		/// <returns>Current game map</returns>
		public IList<GameObject> GetCurrentMap()
		{
			return GameMaps[CurrentMapName];
		}

		/// <summary>
		/// Set current game map
		/// </summary>
		/// <param name="mapName">Map name</param>
		public void SetCurrentMap(string mapName)
		{
			CurrentMapName = mapName;
		}

		/// <summary>
		/// Get map name list
		/// </summary>
		/// <returns>Map name list</returns>
		public IList<string> GetMapNameList()
		{
			return new List<string>(GameMaps.Keys);
		}

		// TODO import from file
		/// <summary>
		/// Create game map
		/// </summary>
		/// <param name="mapName">Map 1 name</param>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		/// <returns>Game map</returns>
		private IList<GameObject> CreateGameMap1(string mapName, int horizontal, int vertical)
		{
			IList<GameObject> gameMap = new List<GameObject>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					if ((x == 2 && y == 4))
					{
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 2 && y == 3))
					{
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 0 && y == 0))
					{
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
						gameMap.Add(new StairGameObject(mapName)
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
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
						gameMap.Add(new WallGameObject(mapName)
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 3 && y == 3))
					{
						ScarecrowGameObject scarecrow = new ScarecrowGameObject(mapName)
						{
							X = x,
							Y = y
						};
						CharacterStatusScript scarecrowStatus = scarecrow.GetScript(typeof(CharacterStatusScript)) as CharacterStatusScript;
						int healthPoint = 3;
						int satiation = 999;

						scarecrowStatus.HealthPoint = healthPoint;
						scarecrowStatus.MaxHealthPoint = healthPoint;
						scarecrowStatus.Satiation = satiation;
						scarecrowStatus.MaxSatiation = satiation;
						scarecrowStatus.AttackPoint = 0;

						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
						
						gameMap.Add(scarecrow);
					}
					else if (new System.Random().Next(10) != 0)
					{
						gameMap.Add(new SpaceGameObject(mapName)
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
		/// <param name="mapName">Map name</param>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		/// <returns>Game map</returns>
		private IList<GameObject> CreateGameMap2(string mapName, int horizontal, int vertical)
		{
			IList<GameObject> gameMap = new List<GameObject>();
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					if ((x == 2 && y == 4))
					{
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 6 && y == 6))
					{
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
						gameMap.Add(new StairGameObject(mapName)
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
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
						gameMap.Add(new WallGameObject(mapName)
						{
							X = x,
							Y = y
						});
					}
					else if ((x == 5 && y == 5))
					{
						gameMap.Add(new SpaceGameObject(mapName)
						{
							X = x,
							Y = y
						});
						gameMap.Add(new ExitGameObject(mapName)
						{
							X = x,
							Y = y
						});
					}
					else if (new System.Random().Next(10) != 0)
					{
						gameMap.Add(new SpaceGameObject(mapName)
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
