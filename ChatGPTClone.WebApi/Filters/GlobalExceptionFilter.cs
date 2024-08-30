using ChatGPTClone.Application.Common.Models.Errors;
using ChatGPTClone.Application.Common.Models.General;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatGPTClone.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            context.ExceptionHandled = true;

            if (context.Exception is ValidationException)
            {
                var exception = context.Exception as ValidationException;

                var responseMessage = "One or more validation errors occured.";

                List<ErrorDto> errors = new();

                // PropertyName = "Model"
                // ErrorMessage = "Must be a string with a maximum length of 50."
                // PropertyName = "Model"
                // Error Message = "Must be a string with a minimum length of 5."

                var propertyNames = exception.Errors
                    .Select(e => e.PropertyName)
                    .Distinct();

                // Model
                foreach (var propertyName in propertyNames)
                {
                    var messages = exception.Errors
                        .Where(e => e.PropertyName == propertyName)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    // ErrorMessage = "Must be a string with a maximum length of 50."

                    errors.Add(new ErrorDto(propertyName, messages));
                }

                // 400
                context.Result = new BadRequestObjectResult(new ResponseDto<string>(responseMessage, errors))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                context.Result =
                    new ObjectResult(new ResponseDto<string>("Internal server error occured. Please try again later."))
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
            }
        }
    }
}