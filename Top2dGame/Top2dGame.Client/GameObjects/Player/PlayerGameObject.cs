using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Character;
using Top2dGame.Client.Scripts.Player;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Player
{
	public class PlayerGameObject : CharacterGameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Player).ToString() };

		public PlayerGameObject(string currentMapName) : base(currentMapName)
		{
			SetTag(TagConst.PLAYER, true);
		}

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
