using Entities.Events.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.User.Events
{
    public class SendUserSignInEmail : INotificationHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
