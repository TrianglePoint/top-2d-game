using System;
using System.Collections.Generic;
using Top2dGame.Model.Existence;

namespace Top2dGame.Client
{
	public class Screen
	{
		private int SizeX { get; set; }
		private int SizeY { get; set; }
		public IList<Existence> GameMap { get; set; }
		public IList<Existence> SpriteList { get; set; }

		public Screen(IList<Existence> gameMap, IList<Existence> spriteList) : this(20, 20, gameMap, spriteList) { }

		public Screen(int sizeX, int sizeY, IList<Existence> gameMap, IList<Existence> spriteList)
		{
			SizeX = sizeX;
			SizeY = sizeY;
			GameMap = gameMap;
			SpriteList = spriteList;
		}

		public void Display()
		{
			PrintData(GameMap);
			PrintData(SpriteList);
		}

		public void PrintData(IList<Existence> existences)
		{
			foreach (Existence existence in existences)
			{
				Console.SetCursorPosition(existence.X, existence.Y);
				Console.Write(existence.Sprite);
			}
		}
	}
}
