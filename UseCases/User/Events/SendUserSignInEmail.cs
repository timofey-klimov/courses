using Entities.Events.User;
using MailSender.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.User.Events
{
    public class SendUserSignInEmail : INotificationHandler<UserCreatedEvent>
    {
        private readonly IMailSender _mailSender;
        public SendUserSignInEmail(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var result = await _mailSender.SendEmailAsync(notification.Email, "Первый вход на сайта", $"Логин:{notification.Email}, пароль:{notification.Password}");
        }
    }
}
