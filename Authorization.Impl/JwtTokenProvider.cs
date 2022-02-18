using Authorization.Impl.Settings;
using Authorization.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Authorization.Impl
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtSecuritySettings _settings;
        public JwtTokenProvider(JwtSecuritySettings settings)
        {
            _settings = settings;
        }

        public string CreateToken(IEnumerable<Claim> claims)
        {
            var handler = new JwtSecurityTokenHandler();

            var signingCredintials = new SigningCredentials(_settings.GetSymmetricKey(), SecurityAlgorithms.HmacSha256Signature);

            var identity = new ClaimsIdentity(claims);

            var token = handler.CreateJwtSecurityToken(subject: identity,
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                expires: DateTime.UtcNow.AddDays(360),
                signingCredentials: signingCredintials);

            return handler.WriteToken(token);
        }
    }
}
