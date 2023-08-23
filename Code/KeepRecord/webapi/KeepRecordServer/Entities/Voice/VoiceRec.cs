using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Voice
{
    public class VoiceRec
    {
        public long RecId { get; set; }

        [NotMapped]
        public long recid 
        {
            get { return RecId; }
            set { RecId = value; }
        }

        [NotMapped]
        public string RoomName { get; set; } 
        public int RoomId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleFlag { get; set; }
        public string Commentary { get; set; }
        public string Remark { get; set; }
        public string RelativeFilePath { get; set; }
        public string AbsoluteFilePath { get; set; }
        public float FileIndex { get; set; }
        public bool Status { get; set; }  
    }

    public class VoiceView 
    { 
        public long recid { get; set; }
        public string RoomName { get; set; }
        public int RoomId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleFlag { get; set; }
        public string Commentary { get; set; }
        public string Remark { get; set; }
        public string RelativeFilePath { get; set; } 

    }
}
