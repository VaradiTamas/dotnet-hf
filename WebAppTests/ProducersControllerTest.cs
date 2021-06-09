using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
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
    class ProducersControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "MovieDbTest")
            .Options;

        AppDbContext context;
        ProducersService producersService;
        ProducersController producersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            producersService = new ProducersService(context);
            producersController = new ProducersController(producersService, new NullLogger<ProducersController>());
        }

        //GetAllProducers tests

        [Test, Order(1)]
        public void HTTPGET_GetAllProducers_WithSearchString_Test()
        {
            IActionResult actionResult = producersController.GetAllProducers("Producer 1");

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Producer>;

            Assert.That(actionResultData.First().Name, Is.EqualTo("Producer 1"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(1));
            Assert.That(actionResultData.Count, Is.EqualTo(1));
        }

        [Test, Order(2)]
        public void HTTPGET_GetAllProducers_WithNoSearchString_Test()
        {
            IActionResult actionResult = producersController.GetAllProducers("");

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Producer>;

            Assert.That(actionResultData.Count, Is.EqualTo(3));
        }

        //GetProducerById tests

        [Test, Order(3)]
        public void HTTPGET_GetProducerById_ReturnsOk_Test()
        {
            IActionResult actionResult = producersController.GetProducerById(1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var producer = (actionResult as OkObjectResult).Value as Producer;

            Assert.That(producer.Name, Is.EqualTo("Producer 1"));
            Assert.That(producer.Id, Is.EqualTo(1));
        }

        [Test, Order(4)]
        public void HTTPGET_GetProducerById_ReturnsNotFound_Test()
        {
            IActionResult actionResult = producersController.GetProducerById(100);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        //AddProducer tests

        [Test, Order(5)]
        public void HTTPPOST_AddProducer_ReturnsCreated_Test()
        {
            var newProducerVM = new ProducerVM()
            {
                Name = "Producer Test"
            };

            IActionResult actionResult = producersController.AddProducer(newProducerVM);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        //GetProducerData test

        [Test, Order(6)]
        public void GetProducerData_Test()
        {
            IActionResult actionResult = producersController.GetProducerData(1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var producer = (actionResult as OkObjectResult).Value as ProducerWithMoviesAndActorsVM;

            Assert.That(producer.Name, Is.EqualTo("Producer 1"));
        }

        //DeleteProducerById tests

        [Test, Order(7)]
        public void HTTPDELETE_DeleteProducerById_ReturnsOk_Test()
        {
            IActionResult actionResult = producersController.DeleteProducerById(1);

            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(8)]
        public void HTTPDELETE_DeleteProducerById_ReturnsBadRequest_Test()
        {
            IActionResult actionResult = producersController.DeleteProducerById(1);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
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

            context.SaveChanges();
        }
    }
}
