using Final.Commons.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Commons.Concretes.Logger
{
    internal class EventLogger : LogBase
    {
        public override void Log(string message, bool isError)
        {
            lock (lockObj)
            {
                EventLog m_EventLog = new EventLog();
                m_EventLog.Source = "RentEventLog";
                m_EventLog.WriteEntry(message);
            }
        }
    }
}
