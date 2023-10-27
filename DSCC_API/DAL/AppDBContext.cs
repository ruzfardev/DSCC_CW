using DSCC_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace DSCC_API.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=ACER;Initial Catalog=BookStore;Integrated Security=True;TrustServerCertificate=True");
            }
        }
    }
}