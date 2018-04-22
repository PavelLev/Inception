using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inception.Utility.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;



        public ExceptionFilter(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }



        public void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                // do nothing
                return;
            }

            switch (context.Exception)
            {
                case BusinessException businessException:

                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        businessException.Description,
                        businessException.Error
                    });

                    break;
                }

                default:

                {
                    context.Result = new StatusCodeResult(500);

                    break;
                }
            }
        }
    }
}
