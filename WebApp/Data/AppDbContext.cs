using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Models;

namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie_Actor>()
                .HasOne(m => m.Movie)
                .WithMany(ma => ma.Movie_Actors)
                .HasForeignKey(mi => mi.MovieId);

            modelBuilder.Entity<Movie_Actor>()
                .HasOne(m => m.Actor)
                .WithMany(ma => ma.Movie_Actors)
                .HasForeignKey(mi => mi.ActorId);

            //Seeding
            modelBuilder.Entity<Movie>().HasData(
                new Movie()
                {
                    Id = 1,
                    Title = "1st Movie Title",
                    Description = "1st Movie Description",
                    Rate = 4,
                    NumOfRates = 1,
                    Genre = "Comedy",
                    DateAdded = DateTime.Now,
                    ProducerId = 2
                },
                new Movie()
                {
                    Id = 2,
                    Title = "2nd Movie Title",
                    Description = "2nd Movie Description",
                    Genre = "Horror",
                    DateAdded = DateTime.Now,
                    ProducerId = 1
                }
            );
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie_Actor> Movie_Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
