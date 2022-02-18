using Authorization.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Interfaces
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetUser();
    }
}
