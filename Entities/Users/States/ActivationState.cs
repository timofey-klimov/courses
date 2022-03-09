namespace Entities.Users.States
{
    public abstract class ActivationState
    {
        public abstract void ChangePassword(string hashedPassword, Participant participant);

        public static ActivationState Create(ActiveState activeState)
        {
            switch (activeState)
            {
                case ActiveState.PasswordChanged:
                    return new PasswordChangedState();

                case ActiveState.FirstSign:
                    return new FirstSignState();

                default:
                    throw new System.Exception();
            }
        }
      
    }
}
