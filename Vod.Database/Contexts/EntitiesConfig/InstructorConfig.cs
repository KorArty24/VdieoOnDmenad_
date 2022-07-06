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

    internal class InstructorConfig: IEntityTypeConfiguration<Instructor>
    {
        public void Configure (EntityTypeBuilder<Instructor> entity)
        {
            entity.HasMany(i => i.Courses).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
    
}
