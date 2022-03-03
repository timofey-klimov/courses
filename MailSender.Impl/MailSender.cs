using MailKit.Net.Smtp;
using MailSender.Impl.Settings;
using MailSender.Interfaces;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace MailSender.Impl
{
    public class MailSender : IMailSender
    {
        private readonly SmtpClientSettings _settings;

        public MailSender(SmtpClientSettings settings)
        {
            _settings = settings;
        }

        public async Task<MailSendResult> SendEmailAsync(string to, string subject, string bodyMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Cources", _settings.Email));
            emailMessage.To.Add(new MailboxAddress("", to));

            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = bodyMessage
            };

            using var client = new SmtpClient();

            try
            {

                await client.ConnectAsync(_settings.SmtpDomain, _settings.Port, true);
                await client.AuthenticateAsync(_settings.Email, _settings.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                return MailSendResult.Fail(ex.Message);
            }

            return MailSendResult.Ok();
        }
    }
}
