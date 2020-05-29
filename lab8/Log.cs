using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace lab8
{
    public class Log
    {
        public delegate void AddLog(string data);
        public delegate string latestLog(string dest);
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
            /*
            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
            {
                var lastLine = System.IO.File.ReadLines("file.txt").Last();
                return lastLine;
            }
            */
            return logs.Last() ;
                
        }
    }
}
