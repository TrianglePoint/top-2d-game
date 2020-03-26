namespace Top2dGame.Model.Existence
{
	public class Character : IExistence
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Character() : this(0, 0) {}

		public Character(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
