using System.Collections.Generic;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class CharacterGameObject : GameObject
	{
		public override IList<string> Sprite => new List<string> { ((char)SpriteEnum.Character).ToString() };

		public TileGameObject GameTile { get; set; }

		protected override void AddScript()
		{
			Scripts.Add(new CharacterStatusScript(this));
		}
	}
}
