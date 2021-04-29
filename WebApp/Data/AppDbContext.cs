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
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie_Actor> Movie_Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
    }
}
