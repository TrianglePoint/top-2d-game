using System;
using System.Threading;
using Top2dGame.InputMaster.GameController;
using Top2dGame.Model;
using Top2dGame.Model.Existence;

namespace Top2dGame.Client
{
	// TODO Need create Time class or Turn class
	public class TestThread
	{
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
			Character = new Character();
			GameController = new GameController(Character);
			Thread = new Thread(new ThreadStart(ThreadProc));
		}

		private void ThreadProc()
		{
			int interval = FrameMaster.GetInverval(60);

			GameController.StartReceiveInput();

			while (true)
			{
				Console.Clear();
				Console.WriteLine(string.Format("Location : {0}, {1}", Character.X, Character.Y));

				Thread.Sleep(interval);
			}
		}

		public void StartThread()
		{
			Thread.Start();
		}
	}
}
