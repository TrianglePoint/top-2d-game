using SharpDX.XInput;
using System;
using Top2dGame.Client.Master;
using Top2dGame.Client.Scripts.Base;

namespace Top2dGame.Client.Scripts.Player
{
	public class PlayerMoveScript : GameScript
	{
		/// <summary>
		/// Game controller
		/// </summary>
		private Controller Controller { get; set; }

		protected override void Start()
		{
			Controller = new Controller(UserIndex.One);

			// Not connected
			// TODO Remove below when ready input for keyboard.
			if (!Controller.IsConnected)
			{
				Console.WriteLine("Please connect game controller (keyboard is supported soon)");

				Environment.Exit(1);
			}
		}

		public override void Update()
		{           
			// TODO Need const class
			const int ONE_PIXEL = 1;

			GameMaster gameMaster = GameMaster.GetInstance();

			if (Controller.IsConnected)
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
			}
		}

		public override void UpdateEveryTurn()
		{
			throw new System.NotImplementedException();
		}
	}
}
