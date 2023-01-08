using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace VOD.API.Filters
{
    public class APIExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public APIExceptionFilter(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        
        public override void OnException(ExceptionContext context)
        {
            bool isDevelopment = _hostingEnvironment.IsDevelopment();
            var ex = context.Exception;
            string stackTrace = (isDevelopment) ? context.Exception.StackTrace : string.Empty;

            string message = ex.Message;
            string error = string.Empty;
            IActionResult actionResult;
            switch (ex) 
            {
                case DbUpdateConcurrencyException ce:
                    //Returns a 400
                    error = "Concurency Issue";
                    actionResult = new BadRequestObjectResult(
                        new
                        {
                            Error = error,
                            Message = message,
                            StackTrace = stackTrace
                        });
                    break;
                case DbUpdateException due:
                    // Returns a 400
                    error = "Error while saving the data";
                    actionResult = new BadRequestObjectResult(new
                    {
                        Error = error,
                        Message = message,
                        StackTrace = stackTrace
                    });
                    break;
                case VODInvalidCourseException ice:
                    error = "Invalid courseId.";
                    actionResult = new BadRequestObjectResult(new
                    {
                        Error = error,
                        Message = message,
                        StackTrace = stackTrace
                    });
                    break;

            }
        }
    }
}
