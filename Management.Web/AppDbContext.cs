using Management.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Management.Web
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id= Guid.Parse("fd4cec25-97b4-4559-a91c-2074f133f354"),
                Username = "admin1",
                Email="admin1@abc.com",
                Password = "Abc1234",
                // other properties
            });
        }


    }
}
