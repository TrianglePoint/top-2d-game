using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Const;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Space
{
	public class SpaceGameObject : GameObject
	{
		public SpaceGameObject() : base()
		{
			Sprite = new List<string> { ((char)SpriteEnum.Space).ToString() };
			SetTag(TagConst.SPACE, true);
		}

		protected override void AddScript() { }
	}
}
