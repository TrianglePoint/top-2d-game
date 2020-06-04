using System.Collections.Generic;

namespace Top2dGame.Client.Resource
{
	public class GameObjectResourceContainer : ResourceContainer
	{
		/// <summary>
		/// Script list resource
		/// </summary>
		public IList<ResourceContainer> Scripts { get; set; }
	}
}
