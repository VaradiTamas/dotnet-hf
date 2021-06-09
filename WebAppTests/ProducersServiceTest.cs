using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.Data;
using WebApp.Data.Models;
using WebApp.Data.Services;
using WebApp.Data.ViewModels;

namespace WebAppTests
{
    public class ProducersServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieDbTest")
            .Options;

        AppDbContext context;
        ProducersService producersService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            producersService = new ProducersService(context, new NullLogger<ProducersService>());
        }

        //GetAllProducers tests

        [Test, Order(1)]
        public void GetAllProducers_WithNoSearchString_Test()
        {
            var result = producersService.GetAllProducers("");

            Assert.That(result.Count, Is.EqualTo(3));
        }

        [Test, Order(2)]
        public void GetAllProducers_WithSearchString_Test()
        {
            var result = producersService.GetAllProducers("3");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Producer 3"));
        }

        //GetProducerById tests

        [Test, Order(3)]
        public void GetProducerById_WithResponse_Test()
        {
            var result = producersService.GetProducerById(1);

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Producer 1"));
        }

        [Test, Order(4)]
        public void GetProducerById_WithNoResponse_Test()
        {
            var result = producersService.GetProducerById(100);

            Assert.That(result, Is.Null);
        }

        //AddProducer test

        [Test, Order(5)]
        public void AddProducer_Test()
        {
            var newProducer = new ProducerVM()
            {
                Name = "Producer Test"
            };

            var result = producersService.AddProducer(newProducer);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Producer Test"));
            Assert.That(result.Id, Is.Not.Null);
        }

        //GetProducerData test

        [Test, Order(6)]
        public void GetProducerData_Test()
        {
            var result = producersService.GetProducerData(1);

            Assert.That(result.Name, Is.EqualTo("Producer 1"));
            Assert.That(result.MovieActors, Is.Not.Empty);

            var firstMovieName = result.MovieActors.OrderBy(n => n.MovieTitle).FirstOrDefault().MovieTitle;
            Assert.That(firstMovieName, Is.EqualTo("2nd Movie Title"));
        }

        //DeleteProducerById tests

        [Test, Order(7)]
        public void DeleteProducerById_DeleteValidProducer_Test()
        {
            var producerWithIdOne = producersService.GetProducerById(1);

            //producer with id 1 exists
            Assert.That(producerWithIdOne.Id, Is.EqualTo(1));
            Assert.That(producerWithIdOne.Name, Is.EqualTo("Producer 1"));

            producersService.DeleteProducerById(1);
            producerWithIdOne = producersService.GetProducerById(1);

            //producer with id 1 has been deleted
            Assert.That(producerWithIdOne, Is.Null);
        }

        [Test, Order(8)]
        public void DeleteProducerById_DeleteInvalidProducer_Test()
        {
            Assert.That(() => producersService.DeleteProducerById(100),
                Throws.Exception.With.Message.EqualTo("The producer with id: 100 does not exist"));
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