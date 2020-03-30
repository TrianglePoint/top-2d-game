namespace Top2dGame.Model.Existence
{
	/// <summary>
	/// It contain everything.
	/// </summary>
	public class Existence
	{
		/// <summary>
		/// Sprite displayed on the screen (Use SpriteEnum)
		/// </summary>
		public char Sprite { get; protected set; }
		/// <summary>
		/// Location X
		/// </summary>
		private int _x;
		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				// TODO Process negative location
				if (value < 0)
				{
					value = 0;
				}

				_x = value;
			}
		}

		/// <summary>
		/// Location Y
		/// </summary>
		private int _y;

		public int Y
		{
			get
			{
				return _y;
			}
			set
			{
				// TODO Process negative location
				if (value < 0)
				{
					value = 0;
				}

				_y = value;
			}
		}
	}
}
