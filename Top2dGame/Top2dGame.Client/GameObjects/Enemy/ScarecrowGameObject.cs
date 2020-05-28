using Top2dGame.Client.GameObjects.Character;
using Top2dGame.Model.Enum;

namespace Top2dGame.Client.GameObjects.Enemy
{
	public class ScarecrowGameObject : CharacterGameObject
	{
		// TODO Change to enemy type (Stasis type?)
		public ScarecrowGameObject(string name, string currentMapName) : base(name, currentMapName)
		{
			SetTag(TagConst.ENEMY, true);
		}

		public ScarecrowGameObject(string currentMapName) : this("Scarecrow", currentMapName) { }

		protected override void AddScript()
		{
			// TODO Is it fine below?
			base.AddScript();
		}
	}
}
