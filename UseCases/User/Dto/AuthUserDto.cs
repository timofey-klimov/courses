using UseCases.Common.User.Model;

namespace UseCases.User.Dto
{
    public class AuthUserDto
    {
        public string Token { get; }

        public UserRole Role { get; }

        public AuthUserDto(string token, UserRole role)
        {
            Token = token;
            Role = role;
        }
    }
}
