using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoplayerXS.Models
{
    public class SysConfig
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public SysConfig() { }  

        public SysConfig(int _Id, string _Category, string _Name, string _Value)
        {
            Id = _Id;
            Category = _Category;
            Name = _Name;
            Value = _Value;
        }
    }
}
