using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Terrain
{
	public class StairGameObject : GameObject
	{
		/// <summary>
		/// To game map name
		/// </summary>
		public string ToGameMapName { get; set; }
		/// <summary>
		/// To location X
		/// </summary>
		public int ToX { get; set; }
		/// <summary>
		/// To location Y
		/// </summary>
		public int ToY { get; set; }

		public StairGameObject() : base()
		{
			Sprite = new List<string> { ((char)SpriteEnum.Stair).ToString() };
			SetTag(TagConst.TERRAIN, true);
		}

		protected override void AddScript() { }
	}
}
