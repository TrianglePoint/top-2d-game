using Top2dGame.Client.GameObjects.Base;

namespace Top2dGame.Client.Scripts.Base
{
	public abstract class GameScript
	{
		public GameObject GameObject { get; set; }

		protected GameScript()
		{
			Start();
		}

		/// <summary>
		/// Process first time
		/// </summary>
		protected abstract void Start();

		/// <summary>
		/// Process every frame
		/// </summary>
		public abstract void Update();

		/// <summary>
		/// Process every turn
		/// </summary>
		public abstract void UpdateEveryTurn();
	}
}
