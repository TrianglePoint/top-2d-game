using System;
using Top2dGame.Client.Master;
using Top2dGame.Model.Container;
using Top2dGame.Model.GameObjects;

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
		/// Location X
		/// </summary>
		private int X { get; set; }
		/// <summary>
		/// Location Y
		/// </summary>
		private int Y { get; set; }
		/// <summary>
		/// Screen width
		/// </summary>
		private int Width { get; set; }
		/// <summary>
		/// Screen height
		/// </summary>
		private int Height { get; set; }

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
		/// Set size
		/// </summary>
		/// <param name="x">Screen location</param>
		/// <param name="y">Screen location</param>
		/// <param name="width">Screen size</param>
		/// <param name="height">Screen size</param>
		public void SetSize(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Display game
		/// </summary>
		public void Display()
		{
			GameMaster gameMaster = GameMaster.GetInstance();

			if (gameMaster.IsGameClear)
			{
				// TODO Add game clear screen
				Console.Clear();
				Console.WriteLine("Game clear!");
				Environment.Exit(0);

				return;
			}

			for (int i = 0; i < Width; i++)
			{
				for (int j = 0; j < Height; j++)
				{
					GameTile gameTile = gameMaster.GetGameTile(X + i, Y + j);

					if (gameTile != null)
					{
						PrintData(gameTile, i, j);
					}
				}
			}

			PrintPlayerLocation();
		}

		/// <summary>
		/// Print player location
		/// </summary>
		private void PrintPlayerLocation()
		{
			Character player = GameMaster.GetInstance().Player;

			Console.SetCursorPosition(22, 22);
			Console.WriteLine(string.Format("Location : {0}, {1}", player.GameTile.X.ToString("00"), player.GameTile.Y.ToString("00")));
		}

		/// <summary>
		/// Print sprite data.
		/// </summary>
		/// <param name="gameTile">Game tile</param>
		/// <param name="left">Print location x</param>
		/// <param name="top">Print location y</param>
		public void PrintData(GameTile gameTile, int left, int top)
		{
			Console.SetCursorPosition(left, top);
			
			if (gameTile.Character != null)
			{
				Console.Write(gameTile.Character.Sprite);
			}
			else if (gameTile.Terrain != null)
			{
				Console.Write(gameTile.Terrain.Sprite);
			}
			else if (gameTile.Space != null)
			{
				Console.Write(gameTile.Space.Sprite);
			}
		}

		/// <summary>
		/// Clear screen
		/// </summary>
		public void ClearScreen()
		{
			Console.Clear();
		}
	}
}
