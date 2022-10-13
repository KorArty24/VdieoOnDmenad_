using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Service.DatabaseServices
{
    public interface IDbWriteService
    {
        Task<bool> SaveChangesAsync();
        void Update<TEntity>(TEntity item) where TEntity : class;
        void Add<TEntity>(TEntity item) where TEntity : class;
        void Delete<TEntity>(TEntity item) where TEntity : class;
        
    }
}
