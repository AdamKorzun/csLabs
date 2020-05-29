using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace lab8
{
    public class Log
    {
        public delegate void AddLog(string data);
        public delegate string latestLog();
        public event latestLog GetLog;
        public event AddLog NewLog;
        private List<string> logs;



        public Log()
        {
            logs = new List<string>();
        }
        public void Add(string data)
        {
            logs.Add(data);
            this.NewLog?.Invoke(data);
        }
        public string GetLastLog()
        {
           
            if (GetLog != null)
            {
                return this.GetLog.Invoke();

            }
            try
            {
                return logs.Last();
            }
            catch (InvalidOperationException)
            {
                return "No logs found";
            }
            catch (Exception e)
            {
                return "Someting went wrong";
            }


        }
    }
}
