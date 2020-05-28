using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Model.Enum;

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

		public ItemGameObject(string name, string currentMapName, int healthEffect, int satisfactionEffect) : base(name, currentMapName)
		{
			HealthEffect = healthEffect;
			SatisfactionEffect = satisfactionEffect;
			SetTag(TagConst.ITEM, true);
		}

		protected override void AddScript()
		{

		}
	}
}
