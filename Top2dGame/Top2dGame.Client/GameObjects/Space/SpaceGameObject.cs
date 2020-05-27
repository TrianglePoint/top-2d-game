using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Space
{
	public class SpaceGameObject : GameObject
	{
		public override string Name => "Space";

		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Space).ToString() };

		public SpaceGameObject(string currentMapName) : base(currentMapName)
		{
			SetTag(TagConst.SPACE, true);
		}

		protected override void AddScript() { }
	}
}
