using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Space;

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
				{ map1Name, FillRandomSpaceToMap(ResourceMaster.GetInstance().LoadMap(map1Name), map1Name, 20, 20)},
				{ map2Name, FillRandomSpaceToMap(ResourceMaster.GetInstance().LoadMap(map2Name), map2Name, 10, 15)}
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

		/// <summary>
		/// Fill space game object to map as random
		/// </summary>
		/// <param name="map">Map</param>
		/// <param name="mapName">Map name</param>
		/// <param name="horizontal">Map size</param>
		/// <param name="vertical">Map size</param>
		/// <returns>Map</returns>
		private IList<GameObject> FillRandomSpaceToMap(IList<GameObject> map, string mapName, int horizontal, int vertical)
		{
			for (int x = 0; x < horizontal; x++)
			{
				for (int y = 0; y < vertical; y++)
				{
					if (new System.Random().Next(10) != 0)
					{
						map.Add(new SpaceGameObject()
						{
							MapName = mapName,
							// TODO Create string data to const class
							Name = "Space",
							X = x,
							Y = y
						});
					}
				}
			}

			return map;
		}
	}
}
