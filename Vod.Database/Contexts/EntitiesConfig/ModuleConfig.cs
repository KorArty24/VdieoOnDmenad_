using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VOD.Common.Entities;

namespace VOD.Database.Contexts.EntitiesConfig
{
    internal class ModuleConfig: IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> entity)
        {
             entity.HasOne(p => p.Course).WithMany(p => p.Modules).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
