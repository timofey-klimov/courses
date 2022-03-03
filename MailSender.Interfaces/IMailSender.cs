using System.Threading.Tasks;

namespace MailSender.Interfaces
{
    public interface IMailSender
    {
        Task<MailSendResult> SendEmailAsync(string to, string subject, string bodyMessage);
    }
}
