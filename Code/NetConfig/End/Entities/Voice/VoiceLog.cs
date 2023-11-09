using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Voice
{
    public class VoiceLog
    {
        public long RecId { get; set; }
        public string VoiceFlag { get; set; }
        public string FilePath { get; set; }
        public bool Status { get; set; }
    }
}
