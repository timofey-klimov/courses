using Entities.Exceptions;
using System;

namespace Entities.Users.States
{
    public class FirstSignState : ActivationState
    {
        public override void ChangePassword(string hashedPassword, Participant participant)
        {
            participant.ChangePassword(hashedPassword);
        }
    }
}
