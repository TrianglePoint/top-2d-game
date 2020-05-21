﻿using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Const;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class ExitGameObject : GameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Exit).ToString() };

		public ExitGameObject()
		{
			SetTag(TagConst.TERRAIN, true);
		}

		protected override void AddScript() { }
	}
}
