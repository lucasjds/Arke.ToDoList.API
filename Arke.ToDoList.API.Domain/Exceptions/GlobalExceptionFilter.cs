using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Arke.ToDoList.API.Domain.Contracts;

[ExcludeFromCodeCoverage]
public class GlobalExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new JsonResult(new ErrorResponse(context.Exception.Message, (int)HttpStatusCode.NotFound))
            {
                StatusCode = StatusCodes.Status404NotFound
            };
        }
        if (context.Exception is GeneralErrorException)
        {
            context.Result = new JsonResult(new ErrorResponse(context.Exception.Message, (int)HttpStatusCode.BadRequest))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        return Task.CompletedTask;
    }
}
