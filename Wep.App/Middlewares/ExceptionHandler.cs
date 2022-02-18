using Entities.Exceptions;
using Microsoft.AspNetCore.Http;
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
            try
            {
                await _next(context);
            }
            catch (ExceptionBase ex)
            {
                var response = ApiResponse.Fail((int)ex.Code, ex.Message);

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                var response = ApiResponse.Fail(500, "Unhandled");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response), Encoding.UTF8);
            }
        }
    }
}
