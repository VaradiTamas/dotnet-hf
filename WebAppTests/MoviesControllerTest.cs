using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Controllers.v1;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Data.Services;
using WebApp.Data.ViewModels;

namespace WebAppTests
{
    class MoviesControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "MovieDbTest")
           .Options;

        AppDbContext context;
        MoviesService moviesService;
        MoviesController moviesController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            moviesService = new MoviesService(context);
            moviesController = new MoviesController(moviesService);
        }

        //GetAllMovies tests

        [Test, Order(1)]
        public void HTTPGET_GetAllMovies_Test()
        {
            IActionResult actionResult = moviesController.GetAllMovies();

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<MovieWithActorsVM>;

            Assert.That(actionResultData.First().Title, Is.EqualTo("1st Movie Title"));
            Assert.That(actionResultData.First().Description, Is.EqualTo("1st Movie Description"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(1));
            Assert.That(actionResultData.Count, Is.EqualTo(5));
        }

        //GetMovieById tests

        [Test, Order(2)]
        public void HTTPGET_GetMovieById_ReturnsOk_Test()
        {
            IActionResult actionResult = moviesController.GetMovieById(3);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var movie = (actionResult as OkObjectResult).Value as MovieWithActorsVM;

            Assert.That(movie.Title, Is.EqualTo("1st Movie Title"));
            Assert.That(movie.Description, Is.EqualTo("1st Movie Description"));
            Assert.That(movie.Id, Is.EqualTo(3));
        }

        //UpdateMovie tests

        [Test, Order(3)]
        public void HTTPPUT_UpdateMovieById_Test()
        {
            IActionResult actionResult = moviesController.GetMovieById(3);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var beforeMovie = (actionResult as OkObjectResult).Value as MovieWithActorsVM;

            Assert.That(beforeMovie.Title, Is.EqualTo("1st Movie Title"));
            Assert.That(beforeMovie.Description, Is.EqualTo("1st Movie Description"));
            Assert.That(beforeMovie.Id, Is.EqualTo(3));

            var updatedMovie = new MovieVM()
            {
                Title = "Updated Movie Title",
                Description = "Updated Movie Description",
                Rate = 4,
                NumOfRates = 1,
                Genre = "Comedy",
                ProducerId = 2
            };

            IActionResult updatedMovieActionResult = moviesController.UpdateMovieById(3, updatedMovie);

            Assert.That(updatedMovie.Title, Is.EqualTo("Updated Movie Title"));
            Assert.That(updatedMovie.Description, Is.EqualTo("Updated Movie Description"));
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
                    Id = 3,
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
                    Id = 4,
                    Title = "2nd Movie Title",
                    Description = "2nd Movie Description",
                    Genre = "Horror",
                    DateAdded = DateTime.Now,
                    ProducerId = 1
                },
                new Movie()
                {
                    Id = 5,
                    Title = "3rd Movie Title",
                    Description = "3rd Movie Description",
                    Rate = 5,
                    NumOfRates = 4,
                    Genre = "Action",
                    DateAdded = DateTime.Now,
                    ProducerId = 1
                }
            };
            context.Movies.AddRange(movies);

            var movie_actors = new List<Movie_Actor>
            {
                new Movie_Actor()
                {
                    Id = 1,
                    MovieId = 3,
                    ActorId = 1
                },
                new Movie_Actor()
                {
                    Id = 2,
                    MovieId = 3,
                    ActorId = 2
                },
                new Movie_Actor()
                {
                    Id = 3,
                    MovieId = 4,
                    ActorId = 3
                },
                new Movie_Actor()
                {
                    Id = 4,
                    MovieId = 4,
                    ActorId = 4
                },
                new Movie_Actor()
                {
                    Id = 5,
                    MovieId = 5,
                    ActorId = 1
                },
            };
            context.Movie_Actors.AddRange(movie_actors);

            context.SaveChanges();
        }
    }
}
