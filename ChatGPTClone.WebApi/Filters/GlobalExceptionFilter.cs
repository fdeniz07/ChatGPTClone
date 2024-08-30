using Microsoft.AspNetCore.Mvc.Filters;

namespace ChatGPTClone.WebApi.Filters
{
    public class GlobalExceptionFilter:IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
