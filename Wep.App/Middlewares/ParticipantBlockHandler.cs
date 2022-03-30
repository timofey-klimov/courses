using Authorization.Interfaces;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Entities.Participants;
using Entities.Participants.States;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Common.Exceptions;

namespace Wep.App.Middlewares
{
    public class ParticipantBlockHandler
    {
        private RequestDelegate _next;

        public ParticipantBlockHandler(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var dbContext = context.RequestServices.GetService<IDbContext>();
                var currentUserProvider = context.RequestServices.GetService<ICurrentUserProvider>();
                var cache = context.RequestServices.GetService<IMemoryCache>();
                
                var userId = currentUserProvider.GetUserId();
                Participant user = null;

                if (!cache.TryGetValue(userId, out user))
                {
                    user = await dbContext.Participants.FirstOrDefaultAsync(x => x.Id == userId);
                    cache.Set(userId, user, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));
                }
                
                if (user.State == ParticipantState.Blocked)
                    throw new ParticipantBlockedException();
            }
            await _next(context);
        }
    }
}
