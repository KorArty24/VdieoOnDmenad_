using System.Threading.Tasks;

namespace VOD.Admin.Services.Interfaces
{
    public interface IDbWriteService
    {
        Task<bool> SaveChangesAsync();
    }
}
