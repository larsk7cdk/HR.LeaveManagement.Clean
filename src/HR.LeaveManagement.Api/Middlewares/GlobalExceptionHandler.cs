using HR.LeaveManagement.Application.Contracts.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Middlewares;

public class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    IAppLogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogWarning("An unhandled exception occurred while processing the request.", exception);

        httpContext.Response.StatusCode = exception switch
        {
            Application.Exceptions.BadRequestException => StatusCodes.Status400BadRequest,
            Application.Exceptions.NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred!",
                Status = httpContext.Response.StatusCode,
                Detail = exception.Message,
                Type = exception.GetType().Name
            }
        });
    }
}