using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Models
{
    public class TDocumentRecord
    {
        [Key]
        public long DocumentRecordId { get; set; }
        public long? FromId { get; set; }
        public long? ToId { get; set; }
        public string Comment { get; set; }
        public bool? IsDelete { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? DeletedBy { get; set; }

        public virtual MUserMaster From { get; set; }
        public virtual MUserMaster To { get; set; }
    }
}
