using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Models
{
    public class MOrganization
    {
        [Key]
        public long OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string LegalName { get; set; }
        public string OrganizationAdd { get; set; }
        public long? Country { get; set; }
        public long? State { get; set; }
        public long? City { get; set; }
        public string Pincode { get; set; }
        public string MobNo { get; set; }
        public string PhoneNo { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
