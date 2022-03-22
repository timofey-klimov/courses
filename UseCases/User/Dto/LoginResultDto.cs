namespace UseCases.User.Dto
{
    public class LoginResultDto
    {
        public string Token { get; }

        public UserDto User { get; }

        public LoginResultDto(string token, UserDto user)
        {
            Token = token;
            User = user;
        }
    }
}
