using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Authorization.Impl.Settings
{
    public class JwtSecuritySettings
    {
        public string SecurityKey { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }


        public SymmetricSecurityKey GetSymmetricKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
    }
}
