using MediatR;
using Microsoft.Extensions.Logging;

namespace HRPayroll.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>: IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestId = Guid.CreateVersion7();

            _logger.LogInformation("{Guid} Handling {RequestName}", requestId, requestName);

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var response = await next();

            stopwatch.Stop();

            _logger.LogInformation(
                "{Guid} Handled {RequestName} in {ElapsedMs}ms",
                requestId,
                requestName,
                stopwatch.ElapsedMilliseconds);

            return response;
        }
    }
}
