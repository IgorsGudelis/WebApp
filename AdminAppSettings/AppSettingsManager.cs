using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using IServicesApp;

namespace AdminAppSettings
{
    public class AppSettingsManager : IMyAppSettings
    {
        public List<string> DetectLogsTypes()
        {
            const string key = "LogsType";
            var configurationContext = WebConfigurationManager.OpenWebConfiguration("~");
            var logs = configurationContext.AppSettings.Settings[key].Value;

            return logs.Split(',').ToList();
        }
    }
}
