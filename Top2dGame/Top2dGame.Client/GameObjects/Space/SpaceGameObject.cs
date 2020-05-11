using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class SpaceGameObject : GameObject
	{
		public override char Sprite => (char)SpriteEnum.Space;

		protected override void AddScript() { }
	}
}
