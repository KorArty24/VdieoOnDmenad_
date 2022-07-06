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
   internal class VideoConfig: IEntityTypeConfiguration<Video>
    {
        public void Configure (EntityTypeBuilder<Video> entity)
        {
            entity.HasOne<Course>().WithMany().HasForeignKey(v => v.CourseId);
            entity.HasOne<Course>().WithMany().HasForeignKey(v => v.CourseId);

        }
    }
}