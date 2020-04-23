﻿using System;
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
		/// <param name="sightWidth">Screen size</param>
		/// <param name="sightHeight">Screen size</param>
		/// <param name="notChaseWidth">Range that does not chase player</param>
		/// <param name="notChaseHeight">Range that does not chase player</param>
		public void SetSize(int x, int y, int sightWidth, int sightHeight, int notChaseWidth, int notChaseHeight)
		{
			X = x;
			Y = y;
			SightWidth = sightWidth;
			SightHeight = sightHeight;
			NotChaseWidth = notChaseWidth;
			NotChaseHeight = notChaseHeight;
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

			ChasePlayer(gameMaster.Player.GameTile);

			// Show screen as Screen location and sight
			for (int i = 0, x = X - SightWidth; i < SightWidth * 2 + 1; i++, x++)
			{
				for (int j = 0, y = Y - SightHeight; j < SightHeight * 2 + 1; j++, y++)
				{
					GameTile gameTile = gameMaster.GetGameTile(x, y);

					if (gameTile != null)
					{
						PrintData(gameTile, i, j);
					}
					else
					{
						PrintEmpty(i, j);
					}
				}
			}

			PrintPlayerLocation();
		}

		/// <summary>
		/// Chase player
		/// </summary>
		/// <param name="playerGameTile">player game tile</param>
		private void ChasePlayer(GameTile playerGameTile)
		{
			GameMaster gameMaster = GameMaster.GetInstance();

			// Screen should chase player on x-axis
			if (gameMaster.GetDistance(X, playerGameTile.X) > NotChaseWidth)
			{
				// Player is on the right of screen
				if (playerGameTile.X - X > 0)
				{
					X = playerGameTile.X - NotChaseWidth;
				}
				// Player is on the left of screen
				else
				{
					X = playerGameTile.X + NotChaseWidth;
				}

			}
			// Screen should chase player on y-axis
			if (gameMaster.GetDistance(Y, playerGameTile.Y) > NotChaseHeight)
			{
				// Player is on the bottom of screen
				if (playerGameTile.Y - Y > 0)
				{
					Y = playerGameTile.Y - NotChaseWidth;
				}
				// Player is on the top of screen
				else
				{
					Y = playerGameTile.Y + NotChaseWidth;
				}

			}
		}

		/// <summary>
		/// Print player location
		/// </summary>
		private void PrintPlayerLocation()
		{
			Character player = GameMaster.GetInstance().Player;

			Console.SetCursorPosition(0, SightHeight * 2 + 2);
			Console.WriteLine(string.Format("Location : {0}, {1}", player.GameTile.X.ToString("00"), player.GameTile.Y.ToString("00")));
		}

		/// <summary>
		/// Print sprite data.
		/// </summary>
		/// <param name="gameTile">Game tile</param>
		/// <param name="left">Print location x</param>
		/// <param name="top">Print location y</param>
		private void PrintData(GameTile gameTile, int left, int top)
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
		/// Print empty.
		/// </summary>
		/// <param name="left">Print location x</param>
		/// <param name="top">Print location y</param>
		private void PrintEmpty(int left, int top)
		{
			// TODO Move to const file.
			const char EMPTY = ' ';

			Console.SetCursorPosition(left, top);

			Console.Write(EMPTY);
		}
	}
}
