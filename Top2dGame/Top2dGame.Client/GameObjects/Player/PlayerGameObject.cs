using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.Scripts.Character;
using Top2dGame.Client.Scripts.Player;

namespace Top2dGame.Client.GameObjects.Player
{
	public class PlayerGameObject : GameObject
	{
		protected override void AddScript()
		{
			Scripts.Add(new CharacterStatusScript());
			Scripts.Add(new PlayerMoveScript());
		}
	}
}
