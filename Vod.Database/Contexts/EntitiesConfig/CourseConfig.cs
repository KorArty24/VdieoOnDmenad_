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
   internal class CourseConfig: IEntityTypeConfiguration<Course>
    {
        public void Configure (EntityTypeBuilder<Course> entity)
        {
            
        }
    }
}
