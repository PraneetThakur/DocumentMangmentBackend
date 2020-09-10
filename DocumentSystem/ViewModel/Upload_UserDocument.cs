using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.ViewModel
{
    public class Upload_UserDocument
    {
        public int UserId { get; set; }
        public Dictionary<string, string> Base64 { get; set; }
        public string Extension { get; set; }
        public string DoucmentName { get; set; }
    }
}
