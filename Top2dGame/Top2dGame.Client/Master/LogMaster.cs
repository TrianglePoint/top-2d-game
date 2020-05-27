using System.Collections.Generic;

namespace Top2dGame.Client.Master
{
	/// <summary>
	/// Log master
	/// </summary>
	public sealed class LogMaster
	{
		/// <summary>
		/// Instance
		/// </summary>
		private static readonly LogMaster Instance = new LogMaster();

		/// <summary>
		/// Log list
		/// </summary>
		private IList<string> LogList { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		private LogMaster()
		{
			LogList = new List<string>();
		}

		/// <summary>
		/// Get instance
		/// </summary>
		/// <returns>Instance</returns>
		public static LogMaster GetInstance()
		{
			return Instance;
		}

		/// <summary>
		/// Get log from latest
		/// </summary>
		/// <param name="index">Log index</param>
		/// <returns>Log</returns>
		public string GetLogFromLatest(int index)
		{
			string log = string.Empty;
			int realIndex = LogList.Count - 1 - index;

			if (realIndex >= 0 && realIndex < LogList.Count)
			{
				log = LogList[LogList.Count - 1 - index];
			}

			return log;
		}

		/// <summary>
		/// Write log
		/// </summary>
		/// <param name="log">log data</param>
		public void WriteLog(string log)
		{
			LogList.Add(log);
		}
	}
}
