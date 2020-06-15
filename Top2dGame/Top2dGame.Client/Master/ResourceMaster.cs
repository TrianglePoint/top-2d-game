using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Top2dGame.Client.Resource;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Character;
using Top2dGame.Client.GameObjects.Item;
using Top2dGame.Client.GameObjects.Space;
using Top2dGame.Client.GameObjects.Terrain;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Client.Scripts.Base;
using Top2dGame.Client.GameObjects.Enemy;

namespace Top2dGame.Client.Master
{
	public sealed class ResourceMaster
	{
		/// <summary>
		/// Instance
		/// </summary>
		private static readonly ResourceMaster Instance = new ResourceMaster();

		// TODO Move to const class
		/// <summary>
		/// Resource folder path
		/// </summary>
		private const string RESOURCE_PATH = @"Resource\";

		// TODO Move to const class
		/// <summary>
		/// Map folder path from Resource folder
		/// </summary>
		private string MapPath { get => @"Map\"; }

		// TODO Move to const class
		/// <summary>
		/// Json file extension
		/// </summary>
		private string JsonExtension { get => ".json"; }

		/// <summary>
		/// Get instance
		/// </summary>
		/// <returns>Instance</returns>
		public static ResourceMaster GetInstance()
		{
			return Instance;
		}

		/// <summary>
		/// Get json file as string
		/// </summary>
		/// <param name="path">file path</param>
		/// <returns>json string</returns>
		private string GetJsonFileAsString(string path)
		{
			try
			{   
				// Open the json file using a stream reader.
				using (StreamReader sr = new StreamReader(RESOURCE_PATH + path))
				{
					// Read the stream to a string, and return json string.
					return sr.ReadToEnd();
				}
			}
			catch (IOException)
			{
				LogMaster.GetInstance().WriteLog("The file could not be read: " + path);

				return string.Empty;
			}
		}

		/// <summary>
		/// Load map
		/// </summary>
		/// <param name="mapName">Map name</param>
		/// <returns>Map</returns>
		public IList<GameObject> LoadMap(string mapName)
		{
			IList<GameObject> map = new List<GameObject>();
			string json = GetJsonFileAsString(MapPath + mapName + JsonExtension);

			IList<GameObjectResourceContainer> resources = JsonConvert.DeserializeObject<IList<GameObjectResourceContainer>>(json);

			foreach (GameObjectResourceContainer gameObjectResource in resources)
			{
				string gameObjectData = JsonConvert.SerializeObject(gameObjectResource.Data);
				GameObject gameObject = null;

				// TODO Check case if type is empty.
				// TODO Create string data to const class
				if ("Space".Equals(gameObjectResource.Type))
				{
					gameObject = JsonConvert.DeserializeObject<SpaceGameObject>(gameObjectData);
				}
				else if ("Character".Equals(gameObjectResource.Type))
				{
					gameObject = JsonConvert.DeserializeObject<CharacterGameObject>(gameObjectData);
				}
				else if ("Enemy".Equals(gameObjectResource.Type))
				{
					gameObject = JsonConvert.DeserializeObject<EnemyGameObject>(gameObjectData);
				}
				else if ("Stair".Equals(gameObjectResource.Type))
				{
					gameObject = JsonConvert.DeserializeObject<StairGameObject>(gameObjectData);
				}
				else if ("Wall".Equals(gameObjectResource.Type))
				{
					gameObject = JsonConvert.DeserializeObject<WallGameObject>(gameObjectData);
				}
				else if ("Exit".Equals(gameObjectResource.Type))
				{
					gameObject = JsonConvert.DeserializeObject<ExitGameObject>(gameObjectData);
				}
				else if ("Item".Equals(gameObjectResource.Type))
				{
					gameObject = JsonConvert.DeserializeObject<ItemGameObject>(gameObjectData);
				}

				// When valid data
				if (gameObject != null)
				{
					if (string.IsNullOrEmpty(gameObject.Name))
					{
						// Default name
						gameObject.Name = gameObjectResource.Type;
					}
					gameObject.MapName = mapName;
					map.Add(gameObject);

					// Set additional script
					if (gameObjectResource.Scripts != null)
					{
						foreach (ResourceContainer scriptResource in gameObjectResource.Scripts)
						{
							string scriptData = JsonConvert.SerializeObject(scriptResource.Data);
							GameScript script = null;

							// TODO Check case if type is empty.
							// TODO Create string data to const class
							if ("CharacterStatus".Equals(scriptResource.Type))
							{
								script = JsonConvert.DeserializeObject<CharacterStatusScript>(scriptData);
							}
							else if ("CharacterDetectScript".Equals(scriptResource.Type))
							{
								script = JsonConvert.DeserializeObject<CharacterDetectScript>(scriptData);
							}

							// When valid data
							if (script != null)
							{
								script.GameObject = gameObject;
								gameObject.Scripts.Add(script);
							}
						}
					}
				}
			}

			return map;
		}
	}
}
