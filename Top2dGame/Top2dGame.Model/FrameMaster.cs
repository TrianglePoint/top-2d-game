namespace Top2dGame.Model
{
	public static class FrameMaster
	{
		const int ONE_SECOND = 1000;

		/// <summary>
		/// Get Inverval for Thread.sleep
		/// </summary>
		/// <param name="frame">frame</param>
		/// <returns>interval</returns>
		public static int GetInverval(int frame)
		{
			return ONE_SECOND / frame;
		}
	}
}
