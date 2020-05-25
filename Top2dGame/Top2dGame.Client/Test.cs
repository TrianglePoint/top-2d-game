using System;
using System.Threading;
using Top2dGame.Client.Master;

namespace Top2dGame.Client
{
	// TODO Need create Time class or Turn class
	// TODO Set boundary
	// TODO Disable mouse click
	public class TestThread
	{
		private Thread Thread { get; set; }

		public TestThread()
		{
			GameMaster gameMaster = GameMaster.GetInstance();

			gameMaster.GameStart();

			// TODO Use other way (ex: import from file)
			Screen.GetInstance().CreateFrame(3, 2, 22, 20, 0, 0, 5, 5, 2, 2);
			// TODO Use other way (ex: import from file)
			gameMaster.PlaceCharacter(gameMaster.Player, 2, 4);
			// TODO Use other way (ex: import from file)
			Thread = new Thread(new ThreadStart(ThreadProc));

			Console.CursorVisible = false;
		}

		/// <summary>
		/// Thread proc
		/// </summary>
		private void ThreadProc()
		{
			GameMaster gameMaster = GameMaster.GetInstance();
			int interval = FrameMaster.GetInverval(60);

			while (true)
			{
				// TODO Move to GameMaster
				gameMaster.ProcessUpdate();
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
