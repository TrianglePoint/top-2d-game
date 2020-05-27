using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Terrain
{
	public class WallGameObject : GameObject
	{
		public override string Name => "Wall";

		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Wall).ToString() };

		public WallGameObject(string currentMapName) : base(currentMapName)
		{
			SetTag(TagConst.TERRAIN, true);
		}

		protected override void AddScript() { }
	}
}
