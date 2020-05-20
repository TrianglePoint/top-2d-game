using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Player;
using Top2dGame.Client.GameObjects.Tile;
using Top2dGame.Client.Scripts.Base;
using Top2dGame.Client.Scripts.Character;

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
			GameObjects = new List<GameObject>();
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
			InitPlayer();

			AddGameObject(Player);
			MapMaster.GetInstance().SetCurrentMap("map1");
		}

		/// <summary>
		/// Get game tile
		/// </summary>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <returns>Game tile</returns>
		public TileGameObject GetGameTile(int x, int y)
		{
			MapMaster mapMaster = MapMaster.GetInstance();

			if (mapMaster.GetCurrentMap() == null)
			{
				// TODO Print text "No Game map!" in textLog section

				return null;
			}

			foreach (TileGameObject gameTile in mapMaster.GetCurrentMap())
			{
				if (gameTile.X == x && gameTile.Y == y)
				{
					return gameTile;
				}
			}

			return null;
		}

		/// <summary>
		/// Get game tile
		/// </summary>
		/// <param name="x">Location x</param>
		/// <param name="y">Location y</param>
		/// <param name="mapName">Map name</param>
		/// <returns>Game tile</returns>
		public TileGameObject GetGameTile(int x, int y, string mapName)
		{
			MapMaster mapMaster = MapMaster.GetInstance();

			if (mapMaster.GetMap(mapName) == null)
			{
				// TODO Print text "No Game map!" in textLog section

				return null;
			}

			foreach (TileGameObject gameTile in mapMaster.GetMap(mapName))
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
		public TileGameObject PlaceCharacter(CharacterGameObject character, int x, int y, string mapName = "")
		{
			bool canPlace = true;
			TileGameObject gameTile;

			if (mapName == "")
			{
				gameTile = GetGameTile(x, y);
			}
			else
			{
				gameTile = GetGameTile(x, y, mapName);
			}

			if (gameTile == null)
			{
				// TODO Get text from const file
				LogMaster.GetInstance().WriteLog("There is nothing.");
				canPlace = false;
			}
			else if (gameTile.Space == null)
			{
				// TODO Get text from const file
				LogMaster.GetInstance().WriteLog("Character is must exist on space!");
				canPlace = false;
			}
			// TODO Process Terrain case. (if terrain is wall, can't place when usually)
			else if (gameTile.Terrain != null)
			{
				if (gameTile.Terrain is WallGameObject)
				{
					LogMaster.GetInstance().WriteLog("There is wall.");
					canPlace = false;
				}
				else if (gameTile.Terrain is StairGameObject stair)
				{
					LogMaster.GetInstance().WriteLog("Move to : " + stair.ToGameMapName);
					canPlace = false;
					// Move through stair
					PlaceCharacter(character, stair.ToX, stair.ToY, stair.ToGameMapName);
				}
				else if (gameTile.Terrain is ExitGameObject)
				{
					// Player has escaped!
					if (character is PlayerGameObject)
					{
						GameClear();
					}
				}
			}
			else if (gameTile.Character != null)
			{
				// TODO Get text from const file
				LogMaster.GetInstance().WriteLog("There is other character");
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

				character.X = x;
				character.Y = y;

				// Move to other game map
				if (mapName != "")
				{
					MapMaster.GetInstance().SetCurrentMap(mapName);
				}
			}

			return gameTile;
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
		/// Init player (TODO this method is temp. load player info from other file)
		/// </summary>
		private void InitPlayer()
		{
			CharacterStatusScript playerStatus = Player.GetScript(typeof(CharacterStatusScript)) as CharacterStatusScript;
			int healthPoint = 10;
			int satiation = 5;

			playerStatus.HealthPoint = healthPoint;
			playerStatus.MaxHealthPoint = healthPoint;
			playerStatus.Satiation = satiation;
			playerStatus.MaxSatiation = satiation;
		}
	}
}
