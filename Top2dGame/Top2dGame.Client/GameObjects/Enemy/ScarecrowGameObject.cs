using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Character;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Enemy
{
	public class ScarecrowGameObject : CharacterGameObject
	{
		public override string Name => "Scarecrow";

		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Character).ToString() };

		public ScarecrowGameObject(string currentMapName) : base(currentMapName)
		{
			SetTag(TagConst.ENEMY, true);
		}

		protected override void AddScript()
		{
			// TODO Is it fine below?
			base.AddScript();
		}
	}
}
