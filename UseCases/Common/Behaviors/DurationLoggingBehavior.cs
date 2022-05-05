using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Common.Behaviors
{
    public class DurationLoggingBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<DurationLoggingBehavior<TRequest, TResponse>> _logger;
        public DurationLoggingBehavior(ILogger<DurationLoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var sw = new Stopwatch();
            try
            {
                sw.Start();
                return await next();
            }
            finally
            {
                sw.Stop();
                _logger.LogInformation($"Duration {sw.ElapsedMilliseconds}мс");
            }
        }
    }
}
