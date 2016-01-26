using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using IServicesApp;

namespace Email
{
    public class EmailManager: IEmailManager, IStrategyLogs
    {
        private readonly Configuration _configurationContext = WebConfigurationManager.OpenWebConfiguration("~");
        public void SendEmail()
        {
            const string keyServer = "gmailLoginServer";
            const string keyUser = "gmailLoginUser";

            var valueServer = _configurationContext.AppSettings.Settings[keyServer].Value;
            var valueUser = _configurationContext.AppSettings.Settings[keyUser].Value;
 
            var fromAddress = new MailAddress(valueServer, "From host7738");  
            var toAddress = new MailAddress(valueUser, "For Igors");

            const string fromPassword = "myHost7738";
            const string subject = "Resize image complited";
            const string body = "Resize image complited! You are the best of the best!";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress) {Subject = subject, Body = body})
            {
                smtp.Send(message);
            }       
        }

        public void AlgorithmLogs()
        {
            SendEmail();
        }
    }
}
