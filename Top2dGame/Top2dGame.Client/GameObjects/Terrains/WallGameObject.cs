using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class WallGameObject : GameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Wall).ToString() };

		protected override void AddScript() { }
	}
}
