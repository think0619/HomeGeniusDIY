using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Page
    { 
        public int Id { get; set; }
        public string PageIndex { get; set; }
        public string ModuleName { get; set; }

        public string Caption { get; set; } 
        public string SubTitle { get; set; }
        public string DateName { get; set; }
        public string ItemID { get; set; }
        public string Value { get; set; }
        public string UnitName { get; set; }
        public bool Status { get; set; } 


        // public string PageName { get; set; }
        public string ChartID { get; set; }
     
        public int OrderID { get; set; }
         
    }

    public class PageNameCfg 
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string PageIndex { get; set; } 

    }
}
