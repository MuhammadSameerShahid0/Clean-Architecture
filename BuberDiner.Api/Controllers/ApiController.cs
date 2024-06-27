using BuberDiner.Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDiner.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKey.Errors] = errors;
        var FirstError = errors[0];

        var statusCode = FirstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode : statusCode, title: FirstError.Description);
    }
}

//using BuberDiner.Api.Common.Http;
//using ErrorOr;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

//namespace BuberDiner.Api.Controllers;

//[ApiController]
//public class ApiController : ControllerBase
//{
//    protected IActionResult Problem(List<Error> errors)
//    {
//        if (errors.Count is 0)
//        {
//            return Problem();
//        }
//        if (errors.All(error => error.Type == ErrorType.Validation))
//        {
//            return ValidationProblem(errors);
//        }
//        HttpContext.Items[HttpContextItemKey.Errors] = errors;

//        return Problem(errors[0]);
//    }

//    private IActionResult Problem(Error error)
//    {
//        var statusCode = error.Type switch
//        {
//            ErrorType.Conflict => StatusCodes.Status409Conflict,
//            ErrorType.NotFound => StatusCodes.Status404NotFound,
//            ErrorType.Validation => StatusCodes.Status400BadRequest,
//            _ => StatusCodes.Status500InternalServerError,
//        };

//        return Problem(statusCode: statusCode, title: error.Description);
//    }

//    private IActionResult ValidationProblem(List<Error> errors)
//    {
//        var modelstatedictionary = new ModelStateDictionary();
//        foreach (var error in errors)
//        {
//            modelstatedictionary.AddModelError(
//                error.Code,
//                error.Description);
//        }
//        return ValidationProblem(modelstatedictionary);
//    }
//}
