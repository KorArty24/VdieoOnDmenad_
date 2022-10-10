using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericEventRunner.DomainParts;
namespace VOD.Common.DomainEvents
{
    public enum CourseChangeTypes { Added, Updated, Deleted }

    [RemoveDuplicateEvents]
    public class CourseChangeEvent :IEntityEvent
    {
        public CourseChangeEvent(CourseChangeTypes courseChangeType)
        {
            CourseChangeType = courseChangeType;
        }
        public CourseChangeTypes CourseChangeType { get; }
    }
}
