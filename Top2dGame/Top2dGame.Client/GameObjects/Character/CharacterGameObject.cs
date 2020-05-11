using Top2dGame.Client.GameObjects.Base;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class CharacterGameObject : GameObject
	{
		public TileGameObject GameTile { get; set; }

		protected override void AddScript() { }
	}
}
