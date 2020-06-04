using SharpDX.XInput;
using System;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.GameObjects.Character;
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

		private long Time { get; set; }

		/// <summary>
		/// Millisecond unit
		/// </summary>
		public long InputInterval { get; set; }

		public PlayerMoveScript() : base() { }

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
			long currentTime = DateTime.Now.Ticks;

			if (currentTime < Time) { return; }

			// TODO Need const class
			const int ONE_PIXEL = 1;

			GameMaster gameMaster = GameMaster.GetInstance();

			if (Controller.IsConnected)
			{
				bool inputted = false;
				int newX = GameObject.X;
				int newY = GameObject.Y;

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
					gameMaster.PlaceCharacter(GameObject as CharacterGameObject, newX, newY);
					gameMaster.NextTurn();

					Time = currentTime + InputInterval * TimeSpan.TicksPerMillisecond;
				}
			}
		}

		public override void UpdateEveryTurn()
		{

		}
	}
}
