﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Dto;

namespace UseCases.User.Commands.BlockUserCommand
{
    public class BlockUserRequest : IRequest<int>
    {
        public int Id { get; }

        public BlockUserRequest(int id) => Id = id;
    }
}
