﻿using SharpDX.XInput;
using System;
using System.Threading;
using Top2dGame.Client.Master;

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

		public GameController()
		{
			Controller = new Controller(UserIndex.One);

			// Not connected
			if (!Controller.IsConnected)
			{
				Console.WriteLine("Please connect game controller (keyboard is supported soon)");

				Environment.Exit(1);
			}
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
				bool inputted = false;
				int newX = gameMaster.Player.GameTile.X;
				int newY = gameMaster.Player.GameTile.Y;

				Controller.GetState(out State state);

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp))
				{
					inputted = true;
					newY -= ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown))
				{
					inputted = true;
					newY += ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight))
				{
					inputted = true;
					newX += ONE_PIXEL;
				}

				if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
				{
					inputted = true;
					newX -= ONE_PIXEL;
				}

				if (inputted)
				{
					gameMaster.PlaceCharacter(gameMaster.Player, newX, newY);
				}

				Thread.Sleep(interval);
			}
		}
	}
}
