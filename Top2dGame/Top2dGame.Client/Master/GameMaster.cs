using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Player;
using Top2dGame.Client.GameObjects.Tile;
using Top2dGame.Client.Scripts.Base;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Model.Const;

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
		/// Game objects
		/// </summary>
		private IList<GameObject> GameObjects { get; set; }

		/// <summary>
		/// Player information
		/// </summary>
		public PlayerGameObject Player { get; set; }

		/// <summary>
		/// Is game clear
		/// </summary>
		public bool IsGameClear { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private GameMaster()
		{
			Player = new PlayerGameObject();
			IsGameClear = false;
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
		/// Add specified game object to game master
		/// </summary>
		/// <param name="gameObject">Game object to add</param>
		public void AddGameObject(GameObject gameObject)
		{
			GameObjects.Add(gameObject);
		}

		/// <summary>
		/// Remove specified game object from game master
		/// </summary>
		/// <param name="gameObject">Game object to remove</param>
		public void RemoveGameObject(GameObject gameObject)
		{
			GameObjects.Remove(gameObject);
		}

		/// <summary>
		/// Start the game
		/// </summary>
		public void GameStart()
		{
			GameObjects = MapMaster.GetInstance().GetMap("map1");
			InitPlayer();
			AddGameObject(Player);
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

			if (GameObjects == null)
			{
				// TODO Print text "No Game map!" in textLog section

				return gameObjects;
			}

			foreach (GameObject gameObject in GameObjects)
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
		/// <returns>Placed game tile</returns>
		public void PlaceCharacter(CharacterGameObject character, int x, int y, string mapName = "")
		{
			bool canPlace = true;
			IList<GameObject> gameObjects;

			if (mapName == "")
			{
				gameObjects = GetGameObjects(x, y);
			}
			else
			{
				gameObjects = GetGameObjects(x, y, mapName);
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

				if (FindGameObjectAsTag(gameObjects, TagConst.CHARACTER) != null)
				{
					// TODO Get text from const file
					LogMaster.GetInstance().WriteLog("There is other character");
					canPlace = false;
				}
				else if (terrainGameObject != null)
				{
					if (terrainGameObject is WallGameObject)
					{
						LogMaster.GetInstance().WriteLog("There is wall.");
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
				else if (FindGameObjectAsTag(gameObjects, TagConst.SPACE) == null)
				{
					// TODO Get text from const file
					LogMaster.GetInstance().WriteLog("Character is must exist on space!");
					canPlace = false;
				}
			}			

			if (canPlace)
			{
				character.X = x;
				character.Y = y;

				// Move to other game map
				if (mapName != "")
				{
					RemoveGameObject(Player);
					GameObjects = MapMaster.GetInstance().GetMap(mapName);
					AddGameObject(Player);
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
			// TODO Is it fine when remove game object?
			for (int i = 0; i < GameObjects.Count; i++)
			{
				foreach (GameScript script in GameObjects[i].Scripts)
				{
					script.Update();
				}
			}
		}

		/// <summary>
		/// Next turn
		/// </summary>
		public void NextTurn()
		{
			// TODO Is it fine when remove game object?
			for (int i = 0; i < GameObjects.Count; i++)
			{
				foreach (GameScript script in GameObjects[i].Scripts)
				{
					script.UpdateEveryTurn();
				}
			}
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
		}
	}
}
