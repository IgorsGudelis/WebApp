using System;
using System.IO;
using IServicesApp;

namespace Logs
{
    public class LogsInFile: IStrategyLogs
    {
        private readonly int _userId;
        private DateTime _dateTime;

        public LogsInFile(int id)
        {
            _userId = id;
        }
        
        public void WriteLogs()
        {            
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var pathToLogs =root + "/Logs/Logs.txt";

            using (StreamWriter file = new StreamWriter(pathToLogs, true))
            {
                _dateTime = DateTime.Now;

                var textAdd = String.Format("User #{0} uploaded img - {1}", _userId, _dateTime.Date);

                file.WriteLine(textAdd);
            }
        }

        public void AlgorithmLogs()
        {
            WriteLogs();
        }
    }
}
