using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class WallGameObject : GameObject
	{
		public override char Sprite => (char)SpriteEnum.Wall;

		protected override void AddScript() { }
	}
}
