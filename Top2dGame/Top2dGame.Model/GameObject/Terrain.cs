using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.GameObject
{
	public class Terrain : Existence
	{
		public override char Sprite => (char)SpriteEnum.Wall;

		public Terrain() : base() { }
	}
}
