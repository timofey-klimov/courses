using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wep.App.Dto.Responses;

namespace Wep.App.Controllers.Base
{
    public class ApplicationController : ControllerBase
    {
        protected IMediator Mediator;
        public ApplicationController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        protected ApiResponse Ok()
        {
            return ApiResponse.Ok();
        }

        protected ApiResponse<T> Ok<T>(T data)
        {
            return ApiResponse<T>.Ok(data);
        }

    }
}
