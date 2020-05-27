using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Terrain
{
	public class ExitGameObject : GameObject
	{
		public override string Name => "Exit";

		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Exit).ToString() };

		public ExitGameObject(string currentMapName) : base(currentMapName)
		{
			SetTag(TagConst.TERRAIN, true);
		}

		protected override void AddScript() { }
	}
}
