﻿using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Model.Const;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Character
{
	public class CharacterGameObject : GameObject
	{
		/// <summary>
		/// Is alive?
		/// </summary>
		public bool IsAlive { get; set; }

		public CharacterGameObject() : base()
		{
			Sprite = new List<string> { ((char)SpriteEnum.Character).ToString() };
			IsAlive = true;
			SetTag(TagConst.CHARACTER, true);
		}

		protected override void AddScript()
		{
			Scripts.Add(new CharacterStatusScript() { GameObject = this });
			// TODO "SightRange = 10" should be remove
			Scripts.Add(new CharacterDetectScript() { GameObject = this, SightRange = 10 });
		}
	}
}
