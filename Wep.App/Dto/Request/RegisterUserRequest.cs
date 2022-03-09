﻿using UseCases.Common.User.Model;

namespace Wep.App.Dto.Request
{
    public class RegisterUserRequest
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
