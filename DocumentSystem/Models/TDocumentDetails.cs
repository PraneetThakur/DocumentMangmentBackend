using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Models
{
    public class TDocumentDetails
    {
        [Key]
        public long DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DoucmentType { get; set; }
        public string DocumentNo { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string LastStatus { get; set; }
        public DateTime? LastStatusDate { get; set; }
        public bool? IsDelete { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? DocumentRecordid { get; set; }
        public string UploadPath { get; set; }

        public virtual TDocumentRecord DocumentRecord { get; set; }
    }
}
