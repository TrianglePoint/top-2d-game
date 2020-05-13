﻿using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class StairGameObject : GameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Stair).ToString() };

		/// <summary>
		/// To game map
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

		protected override void AddScript() { }
	}
}
