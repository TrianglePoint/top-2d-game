﻿using Top2dGame.Client.GameObjects.Tile;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Client.Scripts.Player;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Player
{
	public class PlayerGameObject : CharacterGameObject
	{
		public override char Sprite => (char)SpriteEnum.Player;

		/// <summary>
		/// Health point
		/// </summary>
		public int HealthPoint { get; set; }

		/// <summary>
		/// Satiation
		/// </summary>
		public int Satiation { get; set; }

		protected override void AddScript()
		{
			Scripts.Add(new CharacterStatusScript());
			Scripts.Add(new PlayerMoveScript
			{
				InputInterval = 100
			});
		}
	}
}
