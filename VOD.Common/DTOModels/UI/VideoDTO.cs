﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOModels.UI
{
    public class VideoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
    }
}
