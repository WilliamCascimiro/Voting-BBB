namespace Voting.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                HandleErrorAsync(context, exception);
            }
        }

        private void HandleErrorAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine($"Erro: {exception.Message}");
            Console.WriteLine($"Stack: {exception.StackTrace}");
        }
    }
}
