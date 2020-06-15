namespace Top2dGame.Client.Common
{
	public class Position
	{
		/// <summary>
		/// Location X
		/// </summary>
		public int X { get; set; }
		/// <summary>
		/// Location Y
		/// </summary>
		public int Y { get; set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as Position);
		}

		/// <summary>
		/// Is equal to position X, Y?
		/// </summary>
		/// <param name="other">Other position</param>
		/// <returns>Is equal?</returns>
		private bool Equals(Position other)
		{
			return other != null && X == other.X && Y == other.Y;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
