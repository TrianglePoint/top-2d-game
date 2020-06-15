using Top2dGame.Client.GameObjects.Character;
using Top2dGame.Model.Const;

namespace Top2dGame.Client.GameObjects.Enemy
{
	public class EnemyGameObject : CharacterGameObject
	{
		public EnemyGameObject() : base()
		{
			SetTag(TagConst.ENEMY, true);
		}

		protected override void AddScript() { }
	}
}
