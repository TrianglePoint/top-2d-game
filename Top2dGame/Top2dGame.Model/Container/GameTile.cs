using Top2dGame.Model.GameObject;

namespace Top2dGame.Model.Container
{
	/// <summary>
	/// Existence Container(tile unit)
	/// </summary>
	public class GameTile
	{
		/// <summary>
		/// Location X
		/// </summary>
		public int X { get; set; }
		/// <summary>
		/// Location Y
		/// </summary>
		public int Y { get; set; }
		/// <summary>
		/// Space
		/// </summary>
		public Space Space { get; set; }
		/// <summary>
		/// Terrain
		/// </summary>
		public Terrain Terrain { get; set; }
		/// <summary>
		/// Character
		/// </summary>
		public Character Character { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public GameTile(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
