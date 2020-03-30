using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.Existence
{
	public class Character : Existence
	{
		public Character() : this(0, 0) { }

		public Character(int x, int y)
		{
			Sprite = (char)SpriteEnum.Player;
			X = x;
			Y = y;
		}
	}
}
