using CundecinosWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CundecinosWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.Migrate();
        }
        public DataContext()
        {

        }


        public DbSet<PublicationComments> PublicationComments { get; set; }

        public DbSet<PublicationAttachment> PublicationAttachments { get; set; }

        public DbSet<Publication> Publication { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<CollegeCareer> CollegeCareer { get; set; }


        public DbSet<Extension> Extensions { get; set; }
    }
}