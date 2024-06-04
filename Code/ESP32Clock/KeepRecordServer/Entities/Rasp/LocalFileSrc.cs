using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace Entities.Rasp
{
    public  class LocalFileSrc
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string PublicAuthInfo { get; set; }

        [System.Runtime.Serialization.IgnoreDataMember]
        public string Folder { get; set; }
        public string FileName { get; set; }
        public string ShowName { get; set; }

        [NotMapped]
        public string FullFileSrc
        {
            get
            {
                //smb://username:password@ip_address:port_number/file_path
                return $"{PublicAuthInfo}{Path.Combine(Folder, FileName)}" ;
            }
        }
        public int Status { get; set; }
    }
}







