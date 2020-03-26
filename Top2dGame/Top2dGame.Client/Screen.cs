using System;
using System.Collections.Generic;
using System.Text;

namespace Top2dGame.Client
{
	public class Screen
	{
		private int SizeX { get; set; }
		private int SizeY { get; set; }

		public Screen() : this(20, 20) { }

		public Screen(int sizeX, int sizeY)
		{
			SizeX = sizeX;
			SizeY = sizeY;
		}
	}
}
