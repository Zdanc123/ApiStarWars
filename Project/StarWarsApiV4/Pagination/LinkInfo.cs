﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsApiV4.Pagination
{
    public class LinkInfo
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
    }
}
