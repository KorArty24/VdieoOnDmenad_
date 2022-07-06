using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Database.Contexts.EntitiesConfig
{
    internal class ModuleConfig: IEntityTypeConfiguration<Module>
    {
        public void Configure (EntityTypeBuilder<Module> entity)
        {
            entity.HasMany(module => module.Downloads).WithOne().OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(module => module.Videos).WithOne().OnDelete(DeleteBehavior.Cascade);            
        }
    }
}
