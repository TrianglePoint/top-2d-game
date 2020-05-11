using System.Collections.Generic;
using Top2dGame.Client.Scripts.Base;

namespace Top2dGame.Client.GameObjects.Base
{
	public abstract class GameObject
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
		/// Sprite displayed on the screen (Use SpriteEnum)
		/// </summary>`
		public virtual char Sprite { get; }

		/// <summary>
		/// Scripts
		/// </summary>
		public IList<GameScript> Scripts { get; set; }

		protected GameObject()
		{
			Scripts = new List<GameScript>();
			AddScript();
		}
		
		/// <summary>
		/// Add script
		/// </summary>
		protected abstract void AddScript();
	}
}
