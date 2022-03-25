using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.User.Commands.UnblockUserCommand
{
    public class UnblockUserRequest : IRequest<int>
    {
        public int Id { get; }

        public UnblockUserRequest(int id) => Id = id;
       
    }
}
