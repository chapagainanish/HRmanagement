using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MgmtAPI.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            var problemDetails = exception switch
            {
                ValidationException validationException => CreateValidationProblemDetails(validationException),
                KeyNotFoundException => CreateProblemDetails(
                    HttpStatusCode.NotFound,
                    "Not Found",
                    exception.Message),
                UnauthorizedAccessException => CreateProblemDetails(
                    HttpStatusCode.Unauthorized,
                    "Unauthorized",
                    exception.Message),
                ArgumentException => CreateProblemDetails(
                    HttpStatusCode.BadRequest,
                    "Bad Request",
                    exception.Message),
                _ => CreateProblemDetails(
                    HttpStatusCode.InternalServerError,
                    "Internal Server Error",
                    "An unexpected error occurred.")
            };

            httpContext.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static ProblemDetails CreateProblemDetails(HttpStatusCode statusCode, string title, string detail)
        {
            return new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = detail
            };
        }

        private static ProblemDetails CreateValidationProblemDetails(ValidationException exception)
        {
            var errors = exception.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            return new ValidationProblemDetails(errors)
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Validation Failed"
            };
        }
    }
}
