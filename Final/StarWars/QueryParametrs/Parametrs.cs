using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.QueryParametrs
{
    public class Parametrs
    {
        public const int maxPageCount = 100;

        public int Page { get; set; } = 1;
        private int _pageCount = 100;
        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > maxPageCount) ? maxPageCount : value; }
        }
    }
}
