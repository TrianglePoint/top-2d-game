using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Character;
using Top2dGame.Client.GameObjects.Item;
using Top2dGame.Client.GameObjects.Player;
using Top2dGame.Client.GameObjects.Terrain;
using Top2dGame.Client.Scripts.Base;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.Master
{
	// TODO Separate each function as other class if you can.

	/// <summary>
	/// Game master
	/// </summary>
	public sealed class GameMaster
	{
		/// <summary>
		/// Instance
		/// </summary>
		private static readonly GameMaster Instance = new GameMaster();

		/// <summary>
		/// Player information
		/// </summary>
		public PlayerGameObject Player { get; set; }

		/// <summary>
		/// Is game clear
		/// </summary>
		public bool IsGameClear { get; set; }

		/// <summary>
		/// Is game over
		/// </summary>
		public bool IsGameOver { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private GameMaster()
		{
			IsGameClear = false;
			IsGameOver = false;
		}

		/// <summary>
		/// Get instance
		/// </summary>
		/// <returns>Instance</returns>
		public static GameMaster GetInstance()
		{
			return Instance;
		}

		/// <summary>
		/// Add specified game object to specified map
		/// </summary>
		/// <param name="gameObject">Game object to add</param>
		/// <param name="mapName">Target map name</param>
		public void AddGameObject(GameObject gameObject, string mapName)
		{
			MapMaster.GetInstance().GetMap(mapName).Add(gameObject);
		}

		/// <summary>
		/// Remove specified game object from specified map
		/// </summary>
		/// <param name="gameObject">Game object to remove</param>
		/// <param name="mapName">Target map name</param>
		public void RemoveGameObject(GameObject gameObject, string mapName)
		{
			MapMaster.GetInstance().GetMap(mapName).Remove(gameObject);
		}

		/// <summary>
		/// Start the game
		/// </summary>
		public void GameStart()
		{
			string mapName = "map1";

			MapMaster.GetInstance().SetCurrentMap(mapName);
			Player = new PlayerGameObject
			{
				MapName = mapName,
				Name = "Player"
			};
			InitPlayer();
			Player.Create();
		}

		/// <summary>
		/// Get game object list
		/// </summary>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <returns>Game object list</returns>
		public IList<GameObject> GetGameObjects(int x, int y)
		{
			IList<GameObject> gameObjects = new List<GameObject>();

			if (MapMaster.GetInstance().GetCurrentMap() == null)
			{
				// TODO Print text "No Game map!" in textLog section

				return gameObjects;
			}

			foreach (GameObject gameObject in MapMaster.GetInstance().GetCurrentMap())
			{
				if (gameObject.X == x && gameObject.Y == y)
				{
					gameObjects.Add(gameObject);
				}
			}

			return gameObjects;
		}

		/// <summary>
		/// Get game object list
		/// </summary>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <param name="mapName">Map name</param>
		/// <returns>Game object list</returns>
		public IList<GameObject> GetGameObjects(int x, int y, string mapName)
		{
			IList<GameObject> gameObjects = new List<GameObject>();
			MapMaster mapMaster = MapMaster.GetInstance();

			if (mapMaster.GetMap(mapName) == null)
			{
				// TODO Print text "No Game map!" in textLog section
				return gameObjects;
			}

			foreach (GameObject gameObject in mapMaster.GetMap(mapName))
			{
				if (gameObject.X == x && gameObject.Y == y)
				{
					gameObjects.Add(gameObject);
				}
			}

			return gameObjects;
		}

		/// <summary>
		/// Place character
		/// </summary>
		/// <param name="character">Character</param>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <param name="toMapName">Map name to move</param>
		/// <returns>Placed game tile</returns>
		public void PlaceCharacter(CharacterGameObject character, int x, int y, string toMapName = "")
		{
			bool canPlace = true;
			IList<GameObject> gameObjects;

			if (toMapName == "")
			{
				gameObjects = GetGameObjects(x, y, character.MapName);
			}
			else
			{
				gameObjects = GetGameObjects(x, y, toMapName);
			}

			if (gameObjects.Count == 0)
			{
				// TODO Get text from const file
				LogMaster.GetInstance().WriteLog("There is nothing.");
				canPlace = false;
			}
			else
			{
				GameObject terrainGameObject = FindGameObjectAsTag(gameObjects, TagConst.TERRAIN);
				GameObject otherGameObject = FindGameObjectAsTag(gameObjects, TagConst.CHARACTER);
				GameObject itemGameObject = FindGameObjectAsTag(gameObjects, TagConst.ITEM);
				
				if (otherGameObject != null)
				{
					// Attack event
					CharacterStatusScript characterStatus = character.GetScript(typeof(CharacterStatusScript)) as CharacterStatusScript;					
					CharacterStatusScript otherStatus = otherGameObject.GetScript(typeof(CharacterStatusScript)) as CharacterStatusScript;

					LogMaster.GetInstance().WriteLog(string.Format("{0} attack {1} damage to {2}.", character.Name, characterStatus.AttackPoint, otherGameObject.Name));
					otherStatus.TakeDamage(characterStatus.AttackPoint);
					canPlace = false;
				}
				else if (itemGameObject != null)
				{
					// Use item
					ItemGameObject item = itemGameObject as ItemGameObject;
					CharacterStatusScript characterStatus = character.GetScript(typeof(CharacterStatusScript)) as CharacterStatusScript;

					LogMaster.GetInstance().WriteLog(string.Format("{0} used {1}. : Health({2}), Satisfaction({3})", character.Name, item.Name, item.HealthEffect, item.SatisfactionEffect));
					characterStatus.UseItem(item.HealthEffect, item.SatisfactionEffect);
					itemGameObject.Destroy();
				}
				else if (terrainGameObject != null)
				{
					if (terrainGameObject is WallGameObject)
					{
						LogMaster.GetInstance().WriteLog(string.Format("There is {0}.", terrainGameObject.Name));
						canPlace = false;
					}
					else if (terrainGameObject is StairGameObject stair)
					{
						LogMaster.GetInstance().WriteLog("Move to : " + stair.ToGameMapName);
						canPlace = false;
						// Move through stair
						PlaceCharacter(character, stair.ToX, stair.ToY, stair.ToGameMapName);
					}
					else if (terrainGameObject is ExitGameObject)
					{
						// Player has escaped!
						if (character is PlayerGameObject)
						{
							GameClear();
						}
					}
				}
			}			

			if (canPlace)
			{
				character.X = x;
				character.Y = y;

				// Move to other game map
				if (toMapName != "")
				{
					RemoveGameObject(character, character.MapName);
					if (character == Player)
					{
						MapMaster.GetInstance().SetCurrentMap(toMapName);
					}
					character.MapName = toMapName;
					AddGameObject(character, character.MapName);
				}
			}
		}

		/// <summary>
		/// Process game clear
		/// </summary>
		private void GameClear()
		{
			IsGameClear = true;
		}

		/// <summary>
		/// Process game over
		/// </summary>
		private void GameOver()
		{
			IsGameOver = true;
		}

		/// <summary>
		/// Get distance
		/// </summary>
		/// <param name="a">Location a</param>
		/// <param name="b">Location b</param>
		/// <returns>Distance</returns>
		public int GetDistance(int a, int b)
		{
			return System.Math.Abs(a - b);
		}

		/// <summary>
		/// Process gameobject update
		/// </summary>
		public void ProcessUpdate()
		{
			MapMaster mapMaster = MapMaster.GetInstance();

			// TODO Is it fine when remove game object?
			foreach (string mapName in mapMaster.GetMapNameList())
			{
				IList<GameObject> map = mapMaster.GetMap(mapName);

				for (int i = 0; i < map.Count; i++)
				{
					foreach (GameScript script in map[i].Scripts)
					{
						script.Update();
					}
				}
			}
		}

		/// <summary>
		/// Next turn
		/// </summary>
		public void NextTurn()
		{
			MapMaster mapMaster = MapMaster.GetInstance();

			// TODO Is it fine when remove game object?
			foreach (string mapName in mapMaster.GetMapNameList())
			{
				IList<GameObject> map = mapMaster.GetMap(mapName);

				for (int i = 0; i < map.Count; i++)
				{
					foreach (GameScript script in map[i].Scripts)
					{
						script.UpdateEveryTurn();
					}
				}
			}

			CheckGameOver();
		}

		/// <summary>
		/// Find game object as tag
		/// </summary>
		/// <param name="gameObjects">Game objects to find target</param>
		/// <param name="tag">Tag</param>
		/// <returns>Found game object</returns>
		public GameObject FindGameObjectAsTag(IList<GameObject> gameObjects, int tag)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject.HasTag(tag))
				{
					return gameObject;
				}
			}

			return null;
		}

		/// <summary>
		/// Init player (TODO this method is temp. load player info from other file)
		/// </summary>
		private void InitPlayer()
		{
			CharacterStatusScript playerStatus = Player.GetScript(typeof(CharacterStatusScript)) as CharacterStatusScript;
			int healthPoint = 10;
			int satiation = 50;

			playerStatus.HealthPoint = healthPoint;
			playerStatus.MaxHealthPoint = healthPoint;
			playerStatus.Satiation = satiation;
			playerStatus.MaxSatiation = satiation;
			playerStatus.AttackPoint = 1;
		}

		/// <summary>
		/// Check game over
		/// </summary>
		private void CheckGameOver()
		{
			if (!Player.IsAlive)
			{
				GameOver();
			}
		}
	}
}
