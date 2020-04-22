using System;
using System.Threading;
using Top2dGame.Client.Master;
using Top2dGame.InputMaster.GameController;

namespace Top2dGame.Client
{
	// TODO Need create Time class or Turn class
	// TODO Process negative location
	// TODO Set boundary
	// TODO Disable mouse click
	public class TestThread
	{
		private Thread Thread { get; set; }
		/// <summary>
		/// Process game Controller's input
		/// </summary>
		private GameController GameController { get; set; }

		public TestThread()
		{
			GameMaster gameMaster = GameMaster.GetInstance();

			gameMaster.GameStart();

			// TODO Use other way (ex: import from file)
			Screen.GetInstance().SetSize(0, 0, 20, 20);
			// TODO Use other way (ex: import from file)
			gameMaster.PlaceCharacter(gameMaster.Player, 2, 4);
			GameController = new GameController();
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
				Screen.GetInstance().Display();

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
