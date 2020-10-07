using DAL.Models;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using SharedLib.ViewModels;
using System.Text;

namespace BLL.Services
{
    public class YahooEmailService : IEmailService
    {
        private readonly NetworkCredential _networkCredential;
        private readonly SmtpClient _smtpClient;
        private readonly MailMessage _mailMessage;

        public YahooEmailService()
        {
            _networkCredential = new NetworkCredential();
            _smtpClient = new SmtpClient();
            _mailMessage = new MailMessage();
        }

        public bool Send(SendingEmailViewModel model)
        {
            _networkCredential.UserName = model.SenderEmail;
            _networkCredential.Password = model.SenderPassword;
            _smtpClient.Credentials = _networkCredential;
            
            _mailMessage.From = new MailAddress(model.SenderEmail, SharedLib.Strings.CompanyName);
            _mailMessage.To.Add(new MailAddress(model.ReceiverEmail));

            _mailMessage.Subject = model.Subject;
            _mailMessage.Body = model.Content;
            _mailMessage.BodyEncoding = Encoding.UTF8;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            _smtpClient.SendAsync(_mailMessage, "Sending");

            return true;
        }
    }
}
