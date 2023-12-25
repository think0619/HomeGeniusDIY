using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class ReceiveMsg
    { 
        public List<string> Msg { get; set; }

        public string Sender { get; set; }
    }
}
