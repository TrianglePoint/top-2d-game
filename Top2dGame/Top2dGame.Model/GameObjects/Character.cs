using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.GameObjects
{
	public class Character : Existence
	{
		public override char Sprite => (char)SpriteEnum.Player;

		public Character() : base() { }
	}
}
