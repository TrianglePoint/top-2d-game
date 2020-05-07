namespace Top2dGame.Model.GameObjects
{
	public abstract class Character : Existence
	{
		/// <summary>
		/// Health point
		/// </summary>
		public int HealthPoint { get; set; }

		/// <summary>
		/// Satiation
		/// </summary>
		public int Satiation { get; set; }

		public Character() : base() { }
	}
}
