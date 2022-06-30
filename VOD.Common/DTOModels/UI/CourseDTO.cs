﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOModels.UI
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public string MarqueeImageUrl { get; set; }
        public string CourseImageUrl { get; set; }
    }
}
