using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Voice
{
    public class VoiceRequest
    {
        public string voiceflag { get; set; }
        public string text { get; set; }
        public double speed { get; set; }
        public double pitch { get; set; } 
    }
}
