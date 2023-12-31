using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Task2Identity.Models
{
    public class DbFile:IdentityDbContext<ApplicationUser>
    {
        public DbFile(DbContextOptions<DbFile> opt):base(opt)
        {
            
        }

        public DbSet<Student> Students { get; set; }
       

    }
}
