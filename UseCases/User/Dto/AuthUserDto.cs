namespace UseCases.User.Dto
{
    public class AuthUserDto
    {
        public string Token { get; }

        public AuthUserDto(string token)
        {
            Token = token;
        }
    }
}
