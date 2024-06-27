using BuberDiner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDiner.Api.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/Error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statuscode, message) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode , serviceException.ErrorMessage),
          _ => (StatusCodes.Status500InternalServerError , "An Unexpected Error Occued"),
        };

        return Problem(statusCode: statuscode, title : message);
    }

}
