using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Models
{
    public class TUserDoucment
    {
        public long UserDocumentId { get; set; }
        public long? DocumentId { get; set; }
        public long? Assignto { get; set; }
        public long? Assignfrom { get; set; }
        public DateTime? Assigndate { get; set; }
        public long? UserId { get; set; }
        public long? OrganizationId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateBy { get; set; }
        public string DoucmentUrl { get; set; }

        public virtual MOrganization Organization { get; set; }
        public virtual MUserMaster User { get; set; }
    }
}
