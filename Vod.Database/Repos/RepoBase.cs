using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities.Base;
using VOD.Database.Contexts;

namespace VOD.Database.Repos
{
    public abstract class RepoBase<T> : IRepo<T> where T: EntityBase, new ()
    {
        public DbSet<T> Table { get; }

        public VODContext Context;
        
        protected RepoBase(VODContext context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        private readonly bool _disposeContext;

        protected RepoBase(DbContextOptions<VODContext> options) : this (new VODContext(options))
        {
            _disposeContext = true;
        }

        public virtual void Dispose() 
        {
            if (_disposeContext)
            {
                Context.Dispose();
            }
        }
    }
}
