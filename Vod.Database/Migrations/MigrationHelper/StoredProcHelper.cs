using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
namespace VOD.Database.Migrations.MigrationHelper
{
    public static class StoredProcHelper
    {
        public static void AddOnCascadeDelete(MigrationBuilder migrationBuilder) 
        {
            var sql = @"
            
            ALTER TABLE [VOD].[VOD].[Courses]
            DROP CONSTRAINT [FK_Courses_Instructors_InstructorId];
      
            
			ALTER TABLE [VOD].[VOD].[Courses] WITH CHECK
            ADD CONSTRAINT [FK_Courses_Instructors_InstructorId] FOREIGN KEY ([InstructorId])
            REFERENCES [VOD].[VOD].[Instructors] ([Id])
            ON DELETE CASCADE;
			
            
            ALTER TABLE [VOD].[VOD].[Modules] DROP CONSTRAINT [FK_Modules_Courses_CourseId] ; 
           
            
            ALTER TABLE [VOD].[VOD].[Modules]  WITH CHECK
            ADD  CONSTRAINT [FK_Modules_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[VOD].[Courses] ([Id])
            ON DELETE CASCADE;
           
           
            ALTER TABLE [VOD].[VOD].[Downloads]
            DROP CONSTRAINT [FK_Downloads_Courses_CourseId]  
           ;
           
            ALTER TABLE [VOD].[VOD].[Downloads]  WITH CHECK
            ADD  CONSTRAINT [FK_Downloads_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[Courses] ([Id])
            ;
           
            ALTER TABLE [VOD].[VOD].[Downloads] 
            DROP CONSTRAINT [FK_Downloads_Modules_ModuleId]
          ;
            
            ALTER TABLE [VOD].[VOD].[Downloads] 
            WITH CHECK
            ADD CONSTRAINT [FK_Downloads_Modules_ModuleId]
            FOREIGN KEY([ModuleId])
            REFERENCES [VOD].[VOD].[Modules] ([Id])
           ;

            ALTER TABLE [VOD].[VOD].[UserCourses] 
            DROP CONSTRAINT [FK_UserCourses_Courses_CourseId]
           ;
            
            ALTER TABLE [VOD].[VOD].[UserCourses] 
            ADD CONSTRAINT [FK_UserCourses_Courses_CourseId] 
            FOREIGN KEY([CourseId])
            REFERENCES [VOD].[VOD].[Courses] ([Id])
            ON DELETE CASCADE
            ;
            ";  
            migrationBuilder.Sql(sql); 
        }

        public static void RemoveOnCascadeDelete(MigrationBuilder migrationBuilder)
        {
            var _sql = @"ALTER TABLE [VOD].[Courses]
            DROP CONSTRAINT [FK_Courses_Instructors_InstructorId];
      
            
			ALTER TABLE [VOD].[VOD].[Courses] WITH CHECK
            ADD CONSTRAINT [FK_Courses_Instructors_InstructorId] FOREIGN KEY ([InstructorId])
            REFERENCES [VOD].[VOD].[Instructors] ([Id])
            ON DELETE CASCADE;
			
            
            ALTER TABLE [VOD].[VOD].[Modules] DROP CONSTRAINT [FK_Modules_Courses_CourseId] ; 
           
            
            ALTER TABLE [VOD].[VOD].[Modules]  WITH CHECK
            ADD  CONSTRAINT [FK_Modules_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[VOD].[Courses] ([Id])
            ON DELETE CASCADE;
           
           
            ALTER TABLE [VOD].[VOD].[Downloads]
            DROP CONSTRAINT [FK_Downloads_Courses_CourseId]  
           ;
           
            ALTER TABLE [VOD].[VOD].[Downloads]  WITH CHECK
            ADD  CONSTRAINT [FK_Downloads_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[VOD].[Courses] ([Id])
            ;
           
            ALTER TABLE [VOD].[VOD].[Downloads] 
            DROP CONSTRAINT [FK_Downloads_Modules_ModuleId]
          ;
            
            ALTER TABLE [VOD].[VOD].[Downloads] 
            WITH CHECK
            ADD CONSTRAINT [FK_Downloads_Modules_ModuleId]
            FOREIGN KEY([ModuleId])
            REFERENCES[VOD].[VOD].[Modules] ([Id])
           ;

           
            ALTER TABLE [VOD].[VOD].[UserCourses] 
            DROP CONSTRAINT [FK_UserCourses_Courses_CourseId]
           ;
            
            ALTER TABLE [VOD].[VOD].[UserCourses] 
            ADD CONSTRAINT [FK_UserCourses_Courses_CourseId] 
            FOREIGN KEY([CourseId])
            REFERENCES [VOD].[VOD].[Courses] ([Id])
            ON DELETE CASCADE
            ;";
             migrationBuilder.Sql(_sql); 
        }
    }
}
