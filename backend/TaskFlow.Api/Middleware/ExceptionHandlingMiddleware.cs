using System.Text.Json;

namespace TaskFlow.Api.Middleware;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;

  public ExceptionHandlingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception exception)
    {
      await HandleExceptionAsync(context, exception);
    }
  }

  private static async Task HandleExceptionAsync(
      HttpContext context,
      Exception exception)
  {
    context.Response.ContentType = "application/json";

    var statusCode = exception switch
    {
      KeyNotFoundException => StatusCodes.Status404NotFound,
      InvalidOperationException => StatusCodes.Status409Conflict,
      UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
      _ => StatusCodes.Status500InternalServerError
    };

    context.Response.StatusCode = statusCode;

    var response = new
    {
      message = exception.Message
    };

    await context.Response.WriteAsync(
        JsonSerializer.Serialize(response));
  }
}