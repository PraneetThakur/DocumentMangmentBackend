using DocumentSystem.Models;
using DocumentSystem.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Data
{
    public class DocumentDBContext : IdentityDbContext<AppUser>
    {
        public DocumentDBContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<JobSeeker> JobSeekers { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<MDepartment> MDepartment { get; set; }
        public virtual DbSet<MOrganization> MOrganization { get; set; }
        public virtual DbSet<MUserMaster> MUserMaster { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<TDocumentDetails> TDocumentDetails { get; set; }
        public virtual DbSet<TDocumentRecord> TDocumentRecord { get; set; }
    }
}
