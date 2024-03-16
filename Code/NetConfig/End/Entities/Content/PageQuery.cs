using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Content
{
    public class PageQuery
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
