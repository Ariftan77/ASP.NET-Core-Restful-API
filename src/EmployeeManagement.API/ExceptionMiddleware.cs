using EmployeeManagement.Business.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EmployeeManagement.API;

public class ExceptionMiddleware
{
    private RequestDelegate Next { get; set; }

    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (DependentEmployeeExistException ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = string.Empty,
                Instance = "",
                Title = $"Dependent Employees {JsonSerializer.Serialize(ex.Employees.Select(x => x.Id))} exist.",
                Type = "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
        catch (AddressNotFoundException ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = string.Empty,
                Instance = "",
                Title = $"Address for id {ex.Id} not found",
                Type = "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
        catch (JobNotFoundException ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = string.Empty,
                Instance = "",
                Title = $"Job for id {ex.Id} not found",
                Type = "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
        catch (EmployeeNotFoundException ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = string.Empty,
                Instance = "",
                Title = $"Employee for id {ex.Id} not found",
                Type = "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
        catch (EmployeesNotFoundException ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = string.Empty,
                Instance = "",
                Title = $"Employee {JsonSerializer.Serialize(ex.EmployeeIds)} not found",
                Type = "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
        catch (TeamNotFoundException ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = string.Empty,
                Instance = "",
                Title = $"Team for id {ex.Id} not found",
                Type = "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
        catch (ValidationException ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = JsonSerializer.Serialize(ex.Errors),
                Instance = "",
                Title = "Validation Error",
                Type = "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
        catch (Exception ex)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails()
            {
                Status = statusCode,
                Detail = ex.Message,
                Instance = "",
                Title= "Internal Server Error - something went wrong",
                Type= "Error"
            };

            var problemDetailJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(problemDetailJson);
        }
    }
}
