using UseCases.Common.User.Model;

namespace UseCases.User.Dto
{
    public class AuthUserDto
    {
        public string Token { get; }

        public bool FirstSignIn { get; } 

        public AuthUserDto(string token, bool firstSignIn)
        {
            Token = token;
            FirstSignIn = firstSignIn;
        }
    }
}
