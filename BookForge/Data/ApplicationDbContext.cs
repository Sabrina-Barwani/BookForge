using BookForge.Models;
using Microsoft.EntityFrameworkCore;

namespace BookForge.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // create table Categories in database
        public DbSet <Category> Categories { get; set; }
    }
}
