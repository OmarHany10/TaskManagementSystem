using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Models;

namespace TaskManagementSystem.EF
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Core.Models.Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
    }
}

