using SharpDX.XInput;
using System;
using System.Threading;
using Top2dGame.Client.Master;
using Top2dGame.Model;
using Top2dGame.Model.GameObject;

namespace Top2dGame.InputMaster.GameController
{
	public class GameController
	{
		/// <summary>
		/// Game controller
		/// </summary>
		private Controller Controller { get; set; }
		/// <summary>
		/// The thread for receive the input.
		/// </summary>
		private Thread InputThread { get; set; }
		private Character Player { get; set; }

		public GameController(Character player)
		{
			Controller = new Controller(UserIndex.One);

			// Not connected
			if (!Controller.IsConnected)
			{
				Console.WriteLine("Please connect game controller (keyboard is supported soon)");

				Environment.Exit(1);
			}

			Player = player;
		}

		/// <summary>
		/// Start the receive input.
		/// </summary>
		public void StartReceiveInput()
		{
			InputThread = new Thread(new ThreadStart(WaitingInput));
			InputThread.Start();
		}

		private void WaitingInput()
		{
			// TODO Need const class
			const int ONE_PIXEL = 1;

			GameMaster gameMaster = GameMaster.GetInstance();
			int interval = FrameMaster.GetInverval(10);

			while (Controller.IsConnected)
			{
				int newX = Player.GameTile.X;
				int newY = Player.GameTile.Y;

				Controller.GetState(out State state);

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp))
				{
					newY -= ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown))
				{
					newY += ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight))
				{
					newX += ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
				{
					newX -= ONE_PIXEL;
				}

				gameMaster.PlaceCharacter(Player, newX, newY);

				Thread.Sleep(interval);
			}
		}
	}
}
