using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace VOD.Admin.Helpers
{
    public static class ListExtensions
    {
        public static SelectList ToSelectList<TEntity>(this List<TEntity> items, string valuefield, string textfield) where TEntity : class
        {
            return new SelectList(items, valuefield, textfield);
        }
    }
}
