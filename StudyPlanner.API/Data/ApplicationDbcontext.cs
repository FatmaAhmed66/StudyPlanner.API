using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyPlanner.API.Models;

namespace StudyPlanner.API.Data
{
    public class ApplicationDbcontext : IdentityDbContext<ApplicationUser>

    {
        public ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options)
           : base(options)
        {
        }

        public DbSet<StudyTask> StudyTasks { get; set; }
    }
}
