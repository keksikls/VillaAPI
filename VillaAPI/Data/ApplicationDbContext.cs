using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Какой то текстКакой то текстКакой то текстКакой то текстКакой то текстКакой то текст",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    SqFt = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now

                },
                new Villa()
                {
                    Id = 2,
                    Name = "Diamond Villa",
                    Details = "Какой то текстКакой то текстКакой то текстКакой то текстКакой то текстКакой то текст",
                    ImageUrl = "",
                    Occupancy = 4,
                    Rate = 600,
                    SqFt = 900,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                 new Villa()
                 {
                     Id = 3,
                     Name = "Golden Villa",
                     Details = "Какой то текстКакой то текстКакой то текстКакой то текстКакой то текстКакой то текст",
                     ImageUrl = "",
                     Occupancy = 3,
                     Rate = 200,
                     SqFt = 300,
                     Amenity = "",
                     CreatedDate = DateTime.Now
                 });
               
                

        }
    }
}
