using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.GameObjects.Terrains
{
	public class Stair : Terrain
	{
		public override char Sprite => (char)SpriteEnum.Stair;
		
		/// <summary>
		/// To game map
		/// </summary>
		public string ToGameMapName { get; private set; }
		/// <summary>
		/// To location X
		/// </summary>
		public int ToX { get; private set; }
		/// <summary>
		/// To location Y
		/// </summary>
		public int ToY { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="toGameMap"></param>
		/// <param name="toX"></param>
		/// <param name="toY"></param>
		public Stair(string toGameMapName, int toX, int toY) : base()
		{
			ToGameMapName = toGameMapName;
			ToX = toX;
			ToY = toY;
		}
	}
}
