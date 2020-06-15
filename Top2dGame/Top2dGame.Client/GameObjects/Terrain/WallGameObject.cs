using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Const;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Terrain
{
	public class WallGameObject : GameObject
	{
		public WallGameObject() : base()
		{
			Sprite = new List<string> { ((char)SpriteEnum.Wall).ToString() };
			SetTag(TagConst.TERRAIN, true);
		}

		protected override void AddScript() { }
	}
}
