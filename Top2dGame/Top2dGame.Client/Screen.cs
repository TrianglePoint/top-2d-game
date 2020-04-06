using System;
using Top2dGame.Client.Master;
using Top2dGame.Model.Container;

namespace Top2dGame.Client
{
	public class Screen
	{
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
		/// <param name="x">Screen location</param>
		/// <param name="y">Screen location</param>
		/// <param name="width">Screen size</param>
		/// <param name="height">Screen size</param>
		public Screen(int x, int y, int width, int height)
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
	}
}
