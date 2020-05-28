using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Terrain
{
	public class WallGameObject : GameObject
	{
		public WallGameObject(string name, string currentMapName) : base(name, currentMapName)
		{
			Sprite = new List<string> { ((char)SpriteEnum.Wall).ToString() };
			SetTag(TagConst.TERRAIN, true);
		}

		public WallGameObject(string currentMapName) : this("Wall", currentMapName) { }

		protected override void AddScript() { }
	}
}
