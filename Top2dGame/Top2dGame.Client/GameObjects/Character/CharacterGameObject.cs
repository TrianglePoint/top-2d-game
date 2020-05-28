using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Character
{
	public class CharacterGameObject : GameObject
	{
		/// <summary>
		/// Is alive?
		/// </summary>
		public bool IsAlive { get; set; }

		public CharacterGameObject(string name, string currentMapName) : base(name, currentMapName)
		{
			Sprite = new List<string> { ((char)SpriteEnum.Character).ToString() };
			IsAlive = true;
			SetTag(TagConst.CHARACTER, true);
		}

		protected override void AddScript()
		{
			Scripts.Add(new CharacterStatusScript(this));
		}
	}
}
