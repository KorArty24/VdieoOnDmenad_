using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections;
using System.Collections.Generic;
using VOD.Database.Contexts;

namespace VOD.Admin.Filters
{
    public class EnsureEntityExistsClass : IPageFilter
    {
        private readonly VODContext _dbContext;
        public EnsureEntityExistsClass(VODContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            List<string> list = new List<string>() { "id", "userId" };
            if (context.HandlerArguments.ContainsKey("id") &&
                context.HandlerArguments.ContainsKey("userId"))
            {
                var usercourse = context.Han;
            }
            
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
