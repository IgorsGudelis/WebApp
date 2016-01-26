using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Users;
using Email;
using IServicesApp;

namespace Logs
{
    public class LogsManager
    {
        private readonly int _userId;
        private readonly List<string> _listLogsTypes;
        private List<IStrategyLogs> StrategyLogsContext { get; set; }

        public LogsManager(List<string> listLogsTypes, int userid)
        {
            _userId = userid;
            this._listLogsTypes = listLogsTypes;
            StrategyLogsContext = new List<IStrategyLogs>();
        }

        public void WriteLogs()
        {
            foreach (var type in _listLogsTypes)
            {
                switch (type)
                {
                    case "file":
                        {
                            StrategyLogsContext.Add(new LogsInFile(_userId));
                            break;
                        }
                    case "email":
                        {
                            StrategyLogsContext.Add(new EmailManager());
                            break;
                        }
                }
            }

            foreach (var implementsClass in StrategyLogsContext)
            {
                implementsClass.AlgorithmLogs();
            }
        }
    }
}
