using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using Wep.App.Dto.Responses;

namespace Wep.App.Middlewares
{
    public class ExceptionHandler
    {
        private RequestDelegate _next;

        public ExceptionHandler(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandler>>();
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                logger.LogError($"{ex.Code}: {ex.Message}");
                var response = ApiResponse.Fail((int)ex.Code, ex.Message);

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = ApiResponse.Fail(500, "Unhandled");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response), Encoding.UTF8);
            }
        }
    }
}
