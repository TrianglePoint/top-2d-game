using Top2dGame.Client.GameObjects.Base;

namespace Top2dGame.Client.GameObjects.Tile
{
	public class TileGameObject : GameObject
	{
		/// <summary>
		/// Space
		/// </summary>
		public SpaceGameObject Space { get; set; }
		/// <summary>
		/// Terrain
		/// </summary>
		public GameObject Terrain { get; set; }
		/// <summary>
		/// Character
		/// </summary>
		public GameObject Character { get; set; }

		protected override void AddScript() { }
	}
}
