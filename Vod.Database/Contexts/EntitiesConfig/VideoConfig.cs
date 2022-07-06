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
    internal class VideoConfig: IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> entity)
        {
            entity.HasOne(v=>v.Module).WithMany(b=>b.Videos).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<Course>().WithMany().HasForeignKey(c => c.CourseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
