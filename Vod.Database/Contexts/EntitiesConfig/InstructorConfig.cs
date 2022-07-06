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
    internal class InstructorConfig: IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> entity)
        {
            entity.HasMany(c => c.Courses).WithOne().HasForeignKey(p=>p.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
