using System;
using Top2dGame.Client.GameObjects.Base;
using Top2dGame.Client.Scripts.Base;

namespace Top2dGame.Client.Scripts.Character
{
	public class CharacterStatusScript : GameScript
	{
		/// <summary>
		/// Health point
		/// </summary>
		public int HealthPoint { get; set; }

		/// <summary>
		/// Max health point
		/// </summary>
		public int MaxHealthPoint { get; set; }

		/// <summary>
		/// Satiation
		/// </summary>
		public int Satiation { get; set; }

		/// <summary>
		/// Max satiation
		/// </summary>
		public int MaxSatiation { get; set; }

		public CharacterStatusScript(GameObject gameObject) : base(gameObject) { }

		protected override void Start()
		{

		}

		public override void Update()
		{

		}

		public override void UpdateEveryTurn()
		{
			Satiation -= 1;

			// Character is Hunger
			if (Satiation < 0)
			{
				HealthPoint -= 1;
			}

			if (IsHpZero())
			{
				Die();
			}

			HealthPoint = Math.Clamp(HealthPoint, 0, MaxHealthPoint);
			Satiation = Math.Clamp(Satiation, 0, MaxSatiation);
			
		}

		/// <summary>
		/// Get 
		/// </summary>
		/// <returns></returns>
		private bool IsHpZero()
		{
			return HealthPoint <= 0;
		}

		/// <summary>
		/// Die game object
		/// </summary>
		/// <returns></returns>
		private void Die()
		{
			GameObject.Destroy();
		}
	}
}
