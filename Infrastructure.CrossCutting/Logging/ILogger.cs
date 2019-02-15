using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CrossCutting.Logging
{
	public interface ILogger
	{
		void LogInfo(string message);

		void LogWarn(string message);

		void LogDebug(string message);

		void LogError(string message);
	}
}
