using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebApp.Data;
using WebApp.Data.Models;

namespace WebAppTests
{
    public class PublishersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieDbTest")
            .Options;

        AppDbContext context;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var producers = new List<Producer>
            {
                new Producer()
                {
                    Id = 1,
                    Name = "Producer 1"
                },
                new Producer()
                {
                    Id = 2,
                    Name = "Producer 2"
                },
                new Producer()
                {
                    Id = 3,
                    Name = "Producer 3"
                }
            };
            context.Producers.AddRange(producers);

            var actors = new List<Actor>
            {
                new Actor()
                {
                    Id = 1,
                    FullName = "Actor 1"
                },
                new Actor()
                {
                    Id = 2,
                    FullName = "Actor 2"
                },
                new Actor()
                {
                    Id = 3,
                    FullName = "Actor 3"
                },
                new Actor()
                {
                    Id = 4,
                    FullName = "Actor 4"
                }
            };
            context.Actors.AddRange(actors);

            var movies = new List<Movie>
            {
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
                },
                new Movie()
                {
                    Id = 1,
                    Title = "3rd Movie Title",
                    Description = "3rd Movie Description",
                    Rate = 5,
                    NumOfRates = 4,
                    Genre = "Action",
                    DateAdded = DateTime.Now,
                    ProducerId = 3
                }
            };
            context.Movies.AddRange(movies);

            var movie_actors = new List<Movie_Actor>
            {
                new Movie_Actor()
                {
                    Id = 1,
                    MovieId = 1,
                    ActorId = 1
                },
                new Movie_Actor()
                {
                    Id = 2,
                    MovieId = 1,
                    ActorId = 2
                },
                new Movie_Actor()
                {
                    Id = 3,
                    MovieId = 2,
                    ActorId = 3
                },
                new Movie_Actor()
                {
                    Id = 4,
                    MovieId = 3,
                    ActorId = 4
                },
                new Movie_Actor()
                {
                    Id = 5,
                    MovieId = 3,
                    ActorId = 1
                },
            };
            context.Movie_Actors.AddRange(movie_actors);

            context.SaveChanges();
        }
    }
}