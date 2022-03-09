using Entities.Exceptions;

namespace Entities.Users.States
{
    public class PasswordChangedState : ActivationState
    {
        public override void ChangePassword(string hashedPassword, Participant participant)
        {
            throw new UserAlreadyActiveException();
        }
    }
}
