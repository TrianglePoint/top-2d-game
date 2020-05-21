using System;
using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Player;
using Top2dGame.Client.GameObjects.Tile;
using Top2dGame.Client.Master;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Model.Const;

namespace Top2dGame.Client
{
	/// <summary>
	/// Screen
	/// </summary>
	public sealed class Screen
	{
		/// <summary>
		/// Instance
		/// </summary>
		private static readonly Screen Instance = new Screen();

		/// <summary>
		/// Border width
		/// </summary>
		private int BorderW { get; set; }
		/// <summary>
		/// Border height
		/// </summary>
		private int BorderH { get; set; }
		/// <summary>
		/// Width
		/// </summary>
		private int Width { get; set; }
		/// <summary>
		/// Height
		/// </summary>
		private int Height { get; set; }
		/// <summary>
		/// Location X
		/// </summary>
		private int X { get; set; }
		/// <summary>
		/// Location Y
		/// </summary>
		private int Y { get; set; }
		/// <summary>
		/// Sight width from location X
		/// </summary>
		private int SightWidth { get; set; }
		/// <summary>
		/// Sight height from location Y
		/// </summary>
		private int SightHeight { get; set; }
		/// <summary>
		/// Range that does not chase player
		/// </summary>
		private int NotChaseWidth { get; set; }
		/// <summary>
		/// Range that does not chase player
		/// </summary>
		private int NotChaseHeight { get; set; }

		private IList<IList<string>> BeforeScreenInfo { get; set; }

		// TODO Move to const file.
		const string EMPTY = " ";

		/// <summary>
		/// Constructor
		/// </summary>
		private Screen() { }

		/// <summary>
		/// Get instance
		/// </summary>
		/// <returns>Instance</returns>
		public static Screen GetInstance()
		{
			return Instance;
		}

		/// <summary>
		/// Create frame
		/// </summary>
		/// <param name="borderW">Screen border width</param>
		/// <param name="borderH">Screen border height</param>
		/// <param name="width">Screen width</param>
		/// <param name="height">Screen height</param>
		/// <param name="x">Screen location</param>
		/// <param name="y">Screen location</param>
		/// <param name="sightWidth">Screen size</param>
		/// <param name="sightHeight">Screen size</param>
		/// <param name="notChaseWidth">Range that does not chase player</param>
		/// <param name="notChaseHeight">Range that does not chase player</param>
		public void CreateFrame(int borderW, int borderH, int width, int height, int x, int y, int sightWidth, int sightHeight, int notChaseWidth, int notChaseHeight)
		{
			BorderW = borderW;
			BorderH = borderH;
			Width = width;
			Height = height;
			X = x;
			Y = y;
			SightWidth = sightWidth;
			SightHeight = sightHeight;
			NotChaseWidth = notChaseWidth;
			NotChaseHeight = notChaseHeight;

			BeforeScreenInfo = CreateScreenInfo(Width, Height);

			DrawBorder(BorderW, BorderH, Width, Height);
		}

		/// <summary>
		/// Draw border
		/// </summary>
		/// <param name="borderW"></param>
		/// <param name="borderH"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		private void DrawBorder(int borderW, int borderH, int width, int height)
		{
			string border = "|";
			int screenStartX = borderW;
			int screenStartY = borderH;
			int screenEndX = borderW + width - 1;
			int screenEndY = borderH + height - 1;

			for (int j = 0; j < borderH * 2 + height; j++)
			{
				for (int i = 0; i < borderW * 2 + width; i++)
				{
					if ((i < screenStartX || i > screenEndX) ||
						(j < screenStartY || j > screenEndY))
					{
						Console.SetCursorPosition(i, j);
						Console.Write(border);
					}
				}
			}
		}

		/// <summary>
		/// Display game
		/// </summary>
		public void Display()
		{
			IList<IList<string>> currentScreenInfo;
			GameMaster gameMaster = GameMaster.GetInstance();

			if (gameMaster.IsGameClear)
			{
				// TODO Add game clear screen
				Console.Clear();
				Console.WriteLine("Game clear!");
				Environment.Exit(0);

				return;
			}

			ChasePlayer(gameMaster.Player);

			currentScreenInfo = CreateScreenInfo(Width, Height);
			SetGameObjects(gameMaster, TagConst.SPACE, currentScreenInfo);
			SetGameObjects(gameMaster, TagConst.TERRAIN, currentScreenInfo);
			SetGameObjects(gameMaster, TagConst.CHARACTER, currentScreenInfo);

			SetPlayerInfo(currentScreenInfo);
			SetPlayerLocation(currentScreenInfo);
			SetLog(currentScreenInfo);

			DrawScreen(BeforeScreenInfo, currentScreenInfo);

			// Overwrite before info with current info
			BeforeScreenInfo = currentScreenInfo;
		}

		/// <summary>
		/// Create screen info to EMPTY
		/// </summary>
		/// <param name="width">Screen width</param>
		/// <param name="height">Screen height</param>
		/// <returns>Screen info</returns>
		private IList<IList<string>> CreateScreenInfo(int width, int height)
		{
			IList<IList<string>> screenInfo = new List<IList<string>>();

			for (int h = 0; h < height; h++)
			{
				IList<string> horizontal = new List<string>();

				for (int w = 0; w < width; w++)
				{
					horizontal.Add(EMPTY);
				}

				screenInfo.Add(horizontal);
			}

			return screenInfo;
		}

		/// <summary>
		/// Chase player
		/// </summary>
		/// <param name="player">player game tile</param>
		private void ChasePlayer(PlayerGameObject player)
		{
			GameMaster gameMaster = GameMaster.GetInstance();

			// Screen should chase player on x-axis
			if (gameMaster.GetDistance(X, player.X) > NotChaseWidth)
			{
				// Player is on the right of screen
				if (player.X - X > 0)
				{
					X = player.X - NotChaseWidth;
				}
				// Player is on the left of screen
				else
				{
					X = player.X + NotChaseWidth;
				}

			}
			// Screen should chase player on y-axis
			if (gameMaster.GetDistance(Y, player.Y) > NotChaseHeight)
			{
				// Player is on the bottom of screen
				if (player.Y - Y > 0)
				{
					Y = player.Y - NotChaseWidth;
				}
				// Player is on the top of screen
				else
				{
					Y = player.Y + NotChaseWidth;
				}

			}
		}

		/// <summary>
		/// Set screen info
		/// </summary>
		/// <param name="screenInfo">Screen info</param>
		/// <param name="left">Cursor position : left</param>
		/// <param name="top">Cursor position : top</param>
		/// <param name="sprite">Data drawn on screen</param>
		private void SetScreenInfo(IList<IList<string>> screenInfo, int left, int top, string sprite)
		{
			foreach (char data in sprite)
			{
				if (top < screenInfo.Count && left < screenInfo[top].Count)
				{
					screenInfo[top][left++] = data.ToString();
				}
			}
		}

		/// <summary>
		/// Set game objects into current screen info
		/// </summary>
		/// <param name="gameMaster">Game master</param>
		/// <param name="tag">Game object tag</param>
		/// <param name="currentScreenInfo">Current screen info</param>
		private void SetGameObjects(GameMaster gameMaster, int tag, IList<IList<string>> currentScreenInfo)
		{
			// Show screen as Screen location and sight
			for (int i = 0, x = X - SightWidth; i < SightWidth * 2 + 1; i++, x++)
			{
				for (int j = 0, y = Y - SightHeight; j < SightHeight * 2 + 1; j++, y++)
				{
					IList<GameObject> gameObjects = gameMaster.GetGameObjects(x, y);

					if (gameObjects.Count != 0)
					{
						GameObject gameObject = gameMaster.FindGameObjectAsTag(gameObjects, tag);
						if (gameObject != null)
						{
							int top = j;
							foreach (string sprite in gameObject.Sprite)
							{
								SetScreenInfo(currentScreenInfo, i, top++, sprite);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Set log into current screen info
		/// </summary>
		/// <param name="currentScreenInfo">Current screen info</param>
		private void SetPlayerInfo(IList<IList<string>> currentScreenInfo)
		{
			PlayerGameObject player = GameMaster.GetInstance().Player;
			CharacterStatusScript statusScript = player.GetScript(typeof(CharacterStatusScript)) as CharacterStatusScript;

			// TODO Get cursorPosition from other.
			SetScreenInfo(currentScreenInfo, SightWidth * 2 + 2, 1, "HP");
			// TODO Get cursorPosition from other.
			SetScreenInfo(currentScreenInfo, SightWidth * 2 + 2, 2, statusScript.HealthPoint.ToString("00"));
			// TODO Get cursorPosition from other.
			SetScreenInfo(currentScreenInfo, SightWidth * 2 + 2, 4, "Satiation");
			// TODO Get cursorPosition from other.
			SetScreenInfo(currentScreenInfo, SightWidth * 2 + 2, 5, statusScript.Satiation.ToString("00"));
		}

		/// <summary>
		/// Set player location into current screen info
		/// </summary>
		/// <param name="currentScreenInfo">Current screen info</param>
		private void SetPlayerLocation(IList<IList<string>> currentScreenInfo)
		{
			GameObject player = GameMaster.GetInstance().Player;

			string text = string.Format("Location : {0}, {1}", player.X.ToString("00"), player.Y.ToString("00"));

			// TODO Get cursorPosition from other.
			SetScreenInfo(currentScreenInfo, 1, SightHeight * 2 + 2, text);
		}

		/// <summary>
		/// Set log into current screen info
		/// </summary>
		/// <param name="currentScreenInfo">Current screen info</param>
		private void SetLog(IList<IList<string>> currentScreenInfo)
		{
			const int MAX_SHOW = 5;

			LogMaster logMaster = LogMaster.GetInstance();

			for (int i = 0; i < MAX_SHOW; i++)
			{
				// TODO Get cursorPosition from other.
				SetScreenInfo(currentScreenInfo, 0, SightHeight * 2 + 4 + i, logMaster.GetLogFromLatest(i));
			}
		}

		/// <summary>
		/// Draw screen
		/// </summary>
		/// <param name="beforeScreenInfo">Before screen info</param>
		/// <param name="currentScreenInfo">Current scrren info</param>
		private void DrawScreen(IList<IList<string>> beforeScreenInfo, IList<IList<string>> currentScreenInfo)
		{
			for (int height = 0; height < currentScreenInfo.Count; height++)
			{
				for (int width = 0; width < currentScreenInfo[height].Count; width++)
				{
					// If different current and before screen info
					if (!currentScreenInfo[height][width].Equals(beforeScreenInfo[height][width]))
					{
						Console.SetCursorPosition(width + BorderW, height + BorderH);
						Console.Write(currentScreenInfo[height][width]);
					}
				}
			}
		}
	}
}
