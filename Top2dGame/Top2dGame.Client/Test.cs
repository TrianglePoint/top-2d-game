using System;
using System.Collections.Generic;
using System.Threading;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Player;
using Top2dGame.Client.Master;
using Top2dGame.Client.Scripts.Base;

namespace Top2dGame.Client
{
	// TODO Need create Time class or Turn class
	// TODO Set boundary
	// TODO Disable mouse click
	public class TestThread
	{
		private Thread Thread { get; set; }

		// TODO Move to GameMaster
		private IList<GameObject> GameObjects { get; set; }

		public TestThread()
		{
			GameMaster gameMaster = GameMaster.GetInstance();
			// TODO Move to GameMaster
			GameObjects = new List<GameObject>
			{
				new PlayerGameObject()
			};

			gameMaster.GameStart();

			// TODO Use other way (ex: import from file)
			Screen.GetInstance().CreateFrame(3, 2, 20, 20, 0, 0, 5, 5, 2, 2);
			// TODO Use other way (ex: import from file)
			gameMaster.PlaceCharacter(gameMaster.Player, 2, 4);
			Thread = new Thread(new ThreadStart(ThreadProc));

			Console.CursorVisible = false;
		}

		/// <summary>
		/// Thread proc
		/// </summary>
		private void ThreadProc()
		{
			int interval = FrameMaster.GetInverval(60);

			while (true)
			{
				// TODO Move to GameMaster
				ProcessUpdate();
				Screen.GetInstance().Display();

				Thread.Sleep(interval);
			}
		}

		// TODO Move to GameMaster
		private void ProcessUpdate()
		{
			foreach (GameObject gameObject in GameObjects)
			{
				foreach (GameScript script in gameObject.Scripts)
				{
					script.Update();
				}
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
