using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.API.Cofigurations.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionFilter( IHostEnvironment hostEnvironment ) =>
            _hostEnvironment = hostEnvironment;
        public void OnException( ExceptionContext context )
        {
            if ( !_hostEnvironment.IsDevelopment( ) )
            {
                // Don't display exception details unless running in Development.
                return;
            }

            context.Result = new ContentResult
            {
                StatusCode = 500 ,
                Content = context.Exception.Message

            };
        }
    }
}
