using Serilog;

namespace CRUD.UI.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public ExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IDiagnosticContext diagnosticContext)
    {
        _next = next;
        _logger = logger;
        _diagnosticContext = diagnosticContext;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
            {
                _logger.LogError("{Exception Type} {Exception Message}", e.InnerException.GetType().ToString(),
                    e.InnerException.Message);
            }
            else
                _logger.LogError("{Exception Type} {Exception Message}", e.GetType().ToString(), e.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            throw;
        }
    }
}