using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Space
{
	public class SpaceGameObject : GameObject
	{
		public SpaceGameObject(string name, string currentMapName) : base(name, currentMapName)
		{
			Sprite = new List<string> { ((char)SpriteEnum.Space).ToString() };
			SetTag(TagConst.SPACE, true);
		}

		public SpaceGameObject(string currentMapName) : this("Space", currentMapName) { }

		protected override void AddScript() { }
	}
}
