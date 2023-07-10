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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publication>()
                .HasMany(p => p.PublicationComments)
                .WithOne(pc => pc.Publication)
                .HasForeignKey(pc => pc.PublicationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.SentMessages)
                .WithOne(m => m.Sender)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.ReceivedMessages)
                .WithOne(m => m.Addressee)
                .HasForeignKey(m => m.AddresseeID)
                .OnDelete(DeleteBehavior.Restrict);
        }


        //public DbSet<NotificationMessage> NotificationMessage { get; set; }

        public DbSet<PublicationComments> PublicationComments { get; set; }

        public DbSet<PublicationAttachment> PublicationAttachments { get; set; }

        public DbSet<Publication> Publication { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<CollegeCareer> CollegeCareer { get; set; }

        public DbSet<Category> categories { get; set; }
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}