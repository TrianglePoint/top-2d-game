using System;

namespace Top2dGame.Model.Const
{
	/// <summary>
	/// Atan2 value
	/// </summary>
	public static class DirectionConst
	{
		public static double RIGHT { get => Math.Atan2(0, 1); }
		public static double UP_RIGHT { get => Math.Atan2(1, 1); }
		public static double UP { get => Math.Atan2(1, 0); }
		public static double LEFT_UP { get => Math.Atan2(1, -1); }
		public static double LEFT { get => Math.Atan2(0, -1); }
		public static double DOWN_LEFT { get => Math.Atan2(-1, -1); }
		public static double DOWN { get => Math.Atan2(-1, 0); }
		public static double RIGHT_DOWN { get => Math.Atan2(-1, 1); }
		public static double VALID_ANGLE { get => Math.Atan2(1, 1) / 2; }
	}
}
