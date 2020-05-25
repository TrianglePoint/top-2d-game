using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Character
{
	public class CharacterGameObject : GameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Character).ToString() };

		public CharacterGameObject(string currentMapName) : base(currentMapName)
		{
			SetTag(TagConst.CHARACTER, true);
		}

		protected override void AddScript()
		{
			Scripts.Add(new CharacterStatusScript(this));
		}
	}
}
