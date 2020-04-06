using Top2dGame.Model.Container;

namespace Top2dGame.Model.GameObject
{
	/// <summary>
	/// It contain everything.
	/// </summary>
	public class Existence
	{
		/// <summary>
		/// Sprite displayed on the screen (Use SpriteEnum)
		/// </summary>
		public virtual char Sprite { get; }
		/// <summary>
		/// Current tile info
		/// </summary>
		public GameTile GameTile { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="gameTile"></param>
		public Existence() { }
	}
}
