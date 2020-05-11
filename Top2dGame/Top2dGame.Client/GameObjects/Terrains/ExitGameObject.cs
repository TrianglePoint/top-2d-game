using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class ExitGameObject : GameObject
	{
		public override char Sprite => (char)SpriteEnum.Exit;

		protected override void AddScript() { }
	}
}
