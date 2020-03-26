using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Top2dGame.Model.Existence;

namespace Top2dGame.InputMaster.GameController
{
	public class GameController
	{
		private Controller Controller { get; set; }
		/// <summary>
		/// The thread for receive the input.
		/// </summary>
		private Thread InputThread { get; set; }
		private Character Character { get; set; }

		public GameController(Character character)
		{
			Controller = new Controller(UserIndex.One);
			Character = character;
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
			const int ONE_SECOND = 1000;
			const int FRAME = 60;
			const int INTERVAL = ONE_SECOND / FRAME;

			while (Controller.IsConnected)
			{
				Controller.GetState(out State state);

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp))
				{
					Character.Y += ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DpadDown))
				{
					Character.Y -= ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight))
				{
					Character.X += ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
				{
					Character.X -= ONE_PIXEL;
				}

				Thread.Sleep(INTERVAL);
			}
		}
	}
}
