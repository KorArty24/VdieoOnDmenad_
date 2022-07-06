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
    internal class DownloadConfig: IEntityTypeConfiguration<Download>
    {
        public void Configure(EntityTypeBuilder<Download> entity)
        {
             entity.HasOne(d=>d.Module).WithMany(x =>x.Downloads).OnDelete(DeleteBehavior.Cascade);
             entity.HasOne<Course>().WithMany().HasForeignKey(en => en.CourseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
