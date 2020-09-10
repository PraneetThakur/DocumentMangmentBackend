using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Models
{
    public class MDepartment
    {
        [Key]
        public long DeptId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDisc { get; set; }
        public long? OrganizationId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
