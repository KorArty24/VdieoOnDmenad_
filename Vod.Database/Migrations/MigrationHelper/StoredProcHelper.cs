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
            CREATE PROCEDURE [VOD].[DropCourseInstructorFK] AS
            BEGIN 
            SET NOCOUNT ON;
            ALTER TABLE [VOD].[VOD].[Courses]
            DROP CONSTRAINT [FK_Courses_Instructors_InstructorId]
            END
        GO
            CREATE PROCEDURE [VOD].[ADDCourseInstructorFK] AS
			BEGIN
			ALTER TABLE [VOD].[VOD].[Courses] WITH CHECK
            ADD CONSTRAINT [FK_Courses_Instructors_InstructorId] FOREIGN KEY ([InstructorId])
            REFERENCES [VOD].[Instructors] ([Id])
            ON DELETE CASCADE
			END
        GO
            CREATE PROCEDURE [VOD].[DropModulesCoursesFK] AS 
            BEGIN 
            SET NOCOUNT ON   
            ALTER TABLE [VOD].[VOD].[Modules] DROP CONSTRAINT [FK_Modules_Courses_CourseId]  
            END
        GO
            CREATE PROCEDURE [VOD].[AddModulesCoursesFK] AS 
            BEGIN 
            ALTER TABLE [VOD].[VOD].[Modules]  WITH CHECK
            ADD  CONSTRAINT [FK_Modules_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[VOD].[Courses] ([Id])
            ON DELETE CASCADE
            END
        GO
            CREATE PROCEDURE [VOD].[DropDownloadsFK] AS 
            BEGIN 
            SET NOCOUNT ON   
            ALTER TABLE [VOD].[VOD].[Downloads]
            DROP CONSTRAINT [FK_Modules_Courses_CourseId]  
            END
        GO
            CREATE PROCEDURE [VOD].[AlterDownloadsFK] AS 
            BEGIN
            ALTER TABLE [VOD].[Downloads]  WITH CHECK
            ADD  CONSTRAINT [FK_Downloads_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[VOD].[Courses] ([Id])
            ON DELETE CASCADE
            END
        GO
            CREATE PROCEDURE [VOD].[DropDownloadsFKModule] AS 
            BEGIN
            SET NOCOUNT ON
            ALTER TABLE [VOD].[VOD].[Downloads] 
            DROP CONSTRAINT [FK_Downloads_Modules_ModuleId]
            END
        GO
            CREATE PROCEDURE [VOD].[AlterDownloadsFKModule] AS 
            BEGIN
            ALTER TABLE [VOD].[VOD].[Downloads] 
            WITH CHECK
            ADD CONSTRAINT [FK_Downloads_Modules_ModuleId]
            FOREIGN KEY([ModuleId])
            REFERENCES [VOD].[Modules] ([Id])
            END
        GO



            ";  
            migrationBuilder.Sql(sql); 
        }

        public static void AddOnCascadeDeleteModulesSproc(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            CREATE PROCEDURE [VOD].[AlterModulesTableCascade] AS
            BEGIN 
            SET NOCOUNT ON;
            ALTER TABLE VOD.VOD.Modules
            DROP CONSTRAINT [FK_Modules_Courses_CourseId]
            ADD  CONSTRAINT [FK_Modules_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[Courses] ([Id])
            ON DELETE CASCADE
            END
        GO;
            ";
            migrationBuilder.Sql(sql);
        }

        public static void AddOnCascadeDeleteDownloadSproc(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            CREATE PROCEDURE [VOD].[AlterDownloadTableCascade] AS
            BEGIN 
            SET NOCOUNT ON;
            ALTER TABLE VOD.VOD.Download
            DROP CONSTRAINT [FK_Downloads_Courses_CourseId]
            DROP CONSTRAINT [FK_Downloads_Modules_ModuleId]
            ADD  CONSTRAINT [FK_Downloads_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[Courses] ([Id])
            ON DELETE CASCADE
            ADD  CONSTRAINT [FK_Downloads_Modules_ModuleId] FOREIGN KEY([ModuleId])
            REFERENCES [VOD].[Modules] ([Id])
            ON DELETE CASCADE
            END
        GO;
            ";
            migrationBuilder.Sql(sql);
        }

        public static void AddOnCascadeDeleteDownloadSproc(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            CREATE PROCEDURE [VOD].[AlterModulesTableCascade] AS
            BEGIN 
            SET NOCOUNT ON;
            ALTER TABLE VOD.VOD.Modules
            DROP CONSTRAINT [FK_Modules_Courses_CourseId]
            ADD  CONSTRAINT [FK_Modules_Courses_CourseId] FOREIGN KEY([CourseId])
            REFERENCES [VOD].[Courses] ([Id])
            ON DELETE CASCADE
            END
        GO;
            ";
            migrationBuilder.Sql(sql);
        }
        
    }
}
