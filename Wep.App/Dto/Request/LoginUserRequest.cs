using UseCases.Common.User.Model;

namespace Wep.App.Dto.Request
{
    public class LoginUserRequest
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
