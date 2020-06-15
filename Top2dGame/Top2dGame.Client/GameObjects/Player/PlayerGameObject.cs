using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Character;
using Top2dGame.Client.Scripts.Player;
using Top2dGame.Model.Const;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Player
{
	public class PlayerGameObject : CharacterGameObject
	{
		public PlayerGameObject() : base()
		{
			Sprite = new List<string> { ((char)SpriteEnum.Player).ToString() };
			SetTag(TagConst.PLAYER, true);
		}

		protected override void AddScript()
		{
			// TODO Is it fine below?
			base.AddScript();

			Scripts.Add(new PlayerMoveScript()
			{
				GameObject = this,
				InputInterval = 100
			});
		}
	}
}
