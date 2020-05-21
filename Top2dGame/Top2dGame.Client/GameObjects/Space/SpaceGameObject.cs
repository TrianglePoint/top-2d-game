using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Const;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class SpaceGameObject : GameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Space).ToString() };

		public SpaceGameObject()
		{
			SetTag(TagConst.SPACE, true);
		}

		protected override void AddScript() { }
	}
}
