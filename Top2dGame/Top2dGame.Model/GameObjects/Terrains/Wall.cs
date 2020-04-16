using System.Collections.Generic;
using Top2dGame.Model.Container;
using Top2dGame.Model.Sprite;

namespace Top2dGame.Model.GameObjects.Terrains
{
	public class Wall : Terrain
	{
		public override char Sprite => (char)SpriteEnum.Wall;
	}
}
