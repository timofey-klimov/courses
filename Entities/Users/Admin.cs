using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Users
{
    public class Admin : Participant
    {
        protected Admin() { }

        public Admin(string login, string name, string surname, string password, string hashedPassword, UserRole role) 
            : base(login, name, surname, password, hashedPassword, role)
        {

        }
    }
}
