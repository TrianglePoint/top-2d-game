using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Terrain
{
	public class StairGameObject : GameObject
	{
		public override string Name => "Stair";

		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Stair).ToString() };

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

		public StairGameObject(string currentMapName) : base(currentMapName)
		{
			SetTag(TagConst.TERRAIN, true);
		}

		protected override void AddScript() { }
	}
}
