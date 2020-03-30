using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.Existence
{
	public class Terrain : Existence
	{
		public Terrain() : this(0, 0) { }

		public Terrain(int x, int y)
		{
			Sprite = (char)SpriteEnum.Wall;
			X = x;
			Y = y;
		}
	}
}
