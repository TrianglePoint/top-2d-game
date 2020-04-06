using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.GameObject
{
	public class Space : Existence
	{
		public override char Sprite => (char)SpriteEnum.Space;

		public Space() : base() { }
	}
}
