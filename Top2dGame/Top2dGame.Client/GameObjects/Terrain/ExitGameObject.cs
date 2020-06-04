using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Terrain
{
	public class ExitGameObject : GameObject
	{
		public ExitGameObject() : base()
		{
			Sprite = new List<string> { ((char)SpriteEnum.Exit).ToString() };
			SetTag(TagConst.TERRAIN, true);
		}

		protected override void AddScript() { }
	}
}
