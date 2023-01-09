using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using VOD.Database.Exceptions;

namespace VOD.API.Filters
{
    public class VodApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public VodApiExceptionFilter(IWebHostEnvironment environment)
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
                case VODInvalidInstructorException ice:
                    error = "Invalid courseId.";
                    actionResult = new BadRequestObjectResult(new
                    {
                        Error = error,
                        Message = message,
                        StackTrace = stackTrace
                    });
                    break;
                default:
                error = "General Error.";
                actionResult = new ObjectResult(new
                { Error = error, Message = message, StackTrace = stackTrace })
                { StatusCode = 500 };
                break;
            }
        }
    }
}
