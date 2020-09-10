using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.ViewModel
{
    public class Upload_Document
    {
        public Dictionary<string, string> Base64 { get; set; }
        public string Extension { get; set; }
        public string DoucmentName { get; set; }
        public string FromUserId { get; set; }
        public int ToUserId { get; set; }
        public string comment { get; set; }
    }
}
