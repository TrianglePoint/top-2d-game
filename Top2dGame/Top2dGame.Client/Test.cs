using System;
using System.Collections.Generic;
using System.Threading;
using Top2dGame.InputMaster.GameController;
using Top2dGame.Model;
using Top2dGame.Model.Existence;

namespace Top2dGame.Client
{
	// TODO Need create Time class or Turn class
	// TODO Process negative location
	// TODO Show map on screen. ex) screen: (0, 0) -> map (2, 0)
	// TODO Set boundary
	// TODO Disable mouse click
	public class TestThread
	{
		private Screen Screen { get; set; }
		private Thread Thread { get; set; }
		/// <summary>
		/// Player information
		/// </summary>
		private Character Character { get; set; }
		/// <summary>
		/// Process game Controller's input
		/// </summary>
		private GameController GameController { get; set; }

		public TestThread()
		{
			IList<Existence> spirteList = new List<Existence>();

			Screen = new Screen(GetGameMap(), spirteList);
			Character = new Character();
			spirteList.Add(Character);

			GameController = new GameController(Character);
			Thread = new Thread(new ThreadStart(ThreadProc));

			Console.CursorVisible = false;
			
		}

		private void ThreadProc()
		{
			int interval = FrameMaster.GetInverval(60);

			GameController.StartReceiveInput();

			while (true)
			{
				Screen.Display();
				Console.SetCursorPosition(22, 22);
				Console.WriteLine(string.Format("Location : {0}, {1}", Character.X, Character.Y));

				Thread.Sleep(interval);
			}
		}

		public void StartThread()
		{
			Thread.Start();
		}

		/// <summary>
		/// Get game map (temp)
		/// </summary>
		/// <returns>Game map</returns>
		private IList<Existence> GetGameMap()
		{
			// TODO Create game map class (sprite info storage)
			IList<Existence> gameMap = new List<Existence>();
			int w = 10;
			int h = 10;

			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					gameMap.Add(new Space(x, y));
				}
			}

			return gameMap;
		}
	}
}
