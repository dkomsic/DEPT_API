﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DEPT_API.Models
{
    public class TrailerModel
    {
        public string Title { get; set; } = "";
        public string FullTitle { get; set; } = "";
        public string YtVideoUrl { get; set; } = "";
        public string ImdbVideoUrl { get; set; } = "";
        public string LinkEmbed { get; set; } = "";

    }
}