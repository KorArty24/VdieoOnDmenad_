using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.Extensions
{
    public static class ListExtensions
    {
        public static SelectList ToSelectList<TEntity>(this List<TEntity> items, string valueField,
            string TextField) where TEntity : class
        {
            return new SelectList(items, valueField, TextField);
        }
    }
}
