using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Cryptography.X509Certificates;

namespace VOD.Admin.Filters
{
    public class EnsureEntityExistsAttribute : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            throw new NotImplementedException();
        }

        public override void Page(ActionExecutingContext context)
        {
            
        }
    }
}
