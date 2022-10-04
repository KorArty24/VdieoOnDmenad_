using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Service.DatabaseServices
{
    public interface IDbReadService
    {
        (int courses, int downloads, int instructors, int modules, int videos, int users) Count();
    }
}
