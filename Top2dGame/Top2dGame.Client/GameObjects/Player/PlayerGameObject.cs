﻿using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Tile;
using Top2dGame.Client.Scripts.Player;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Player
{
	public class PlayerGameObject : CharacterGameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Player).ToString() };

		protected override void AddScript()
		{
			// TODO Is it fine below?
			base.AddScript();

			Scripts.Add(new PlayerMoveScript(this)
			{
				InputInterval = 100
			});
		}
	}
}
