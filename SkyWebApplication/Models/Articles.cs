﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyWebApplication.Models
{
    public class Articles
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool Hot { get; set; }
        public bool Essence { get; set; }
        public bool Top { get; set; }
        public string Tags { get; set; }
        public DateTime CreatTime { get; set; }
        public string Password { get; set; }
        public bool Comment { get; set; }
    }
}