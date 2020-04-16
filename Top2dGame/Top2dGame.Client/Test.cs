using System;
using System.Threading;
using Top2dGame.Client.Master;
using Top2dGame.InputMaster.GameController;
using Top2dGame.Model.GameObjects;

namespace Top2dGame.Client
{
	// TODO Need create Time class or Turn class
	// TODO Process negative location
	// TODO Set boundary
	// TODO Disable mouse click
	public class TestThread
	{
		private Screen Screen { get; set; }
		private Thread Thread { get; set; }
		/// <summary>
		/// Player information
		/// </summary>
		private Character Player { get; set; }
		/// <summary>
		/// Process game Controller's input
		/// </summary>
		private GameController GameController { get; set; }

		public TestThread()
		{
			GameMaster gameMaster = GameMaster.GetInstance();

			gameMaster.GameStart();

			// TODO Use other way (ex: import from file)
			Screen = new Screen(0, 0, 20, 20);
			Player = new Character();
			// TODO Use other way (ex: import from file)
			gameMaster.PlaceCharacter(Player, 2, 4);
			GameController = new GameController(Player);
			Thread = new Thread(new ThreadStart(ThreadProc));

			Console.CursorVisible = false;			
		}

		/// <summary>
		/// Thread proc
		/// </summary>
		private void ThreadProc()
		{
			int interval = FrameMaster.GetInverval(60);

			GameController.StartReceiveInput();

			while (true)
			{
				Screen.Display();

				// TODO Move to screen class
				Console.SetCursorPosition(22, 22);
				Console.WriteLine(string.Format("Location : {0}, {1}", Player.GameTile.X.ToString("00"), Player.GameTile.Y.ToString("00")));

				Thread.Sleep(interval);
			}
		}

		/// <summary>
		/// Start thread
		/// </summary>
		public void StartThread()
		{
			Thread.Start();
		}
	}
}
