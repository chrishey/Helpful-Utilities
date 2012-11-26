using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

/// <summary>
/// Utility class for initializing a log
/// </summary>
public static class Logger
{
    private static List<ILog> _logs; 

    public static ILog GenerateLog(string name)
    {
        if (_logs == null)
        {
            _logs = new List<ILog>();
        }

        var log = _logs.SingleOrDefault(x => x.Logger.Name.Equals(name));

        if (log == null)
        {
            log = LogManager.GetLogger(name);
            _logs.Add(log);
        }

        return log;
    }
}
