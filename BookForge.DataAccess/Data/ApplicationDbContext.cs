using BookForge.Models;
using Microsoft.EntityFrameworkCore;

namespace BookForge.DataAccess.Data
{
    // represent database context
    public class ApplicationDbContext : DbContext
    {
        // constructor to initialize database context

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // create table Categories in database
        public DbSet<Category> Categories { get; set; }


        // data from database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // create 3 records in Categories table 
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = "1" },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = "2" },
                new Category { Id = 3, Name = "History", DisplayOrder = "3" }

                );
        }
    }
}
