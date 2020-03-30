using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.Existence
{
	public class Space : Existence
	{
		public Space() : this(0, 0) { }

		public Space(int x, int y)
		{
			Sprite = (char)SpriteEnum.Space;
			X = x;
			Y = y;
		}
	}
}
