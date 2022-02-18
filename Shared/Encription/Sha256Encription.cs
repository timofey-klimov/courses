using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Shared.Encription
{
    public static class Sha256Encription
    {
        public static string Encript(string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, new byte[0], KeyDerivationPrf.HMACSHA256, 1000000, 256 / 8));
        }
    }
}
