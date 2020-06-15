using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Const;

namespace Top2dGame.Client.GameObjects.Item
{
	public class ItemGameObject : GameObject
	{
		// TODO Maybe need Effect class

		/// <summary>
		/// Health effect when use
		/// </summary>
		public int HealthEffect { get; set; }
		/// <summary>
		/// Satisfaction effect when use
		/// </summary>
		public int SatisfactionEffect { get; set; }

		public ItemGameObject() : base()
		{
			SetTag(TagConst.ITEM, true);
		}

		protected override void AddScript()
		{

		}
	}
}
