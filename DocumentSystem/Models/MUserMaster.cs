using DocumentSystem.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Models
{
    public class MUserMaster
    {

        [Key]
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string IdentityIds { get; set; }
        public AppUser Identity { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Apassword { get; set; }
        public long? DeptId { get; set; }
        public long? OrganizationId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateBy { get; set; }

        public virtual MDepartment Dept { get; set; }
        public virtual MOrganization Organization { get; set; }
    }
}
