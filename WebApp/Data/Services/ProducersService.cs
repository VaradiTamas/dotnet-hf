using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Models;
using WebApp.Data.ViewModels;

namespace WebApp.Data.Services
{
    public class ProducersService
    {
        private AppDbContext _context;
        private readonly ILogger<ProducersService> _logger;

        //inject AppDbContext file & ILogger
        public ProducersService(AppDbContext context, ILogger<ProducersService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Producer AddProducer(ProducerVM producer)
        {
            var _producer = new Producer()
            {
                Name = producer.Name
            };
            _context.Producers.Add(_producer);
            _context.SaveChanges();

            _logger.LogInformation($"Producer added successfully! Producer: {_producer.Name} With Id: {_producer.Id}");

            return _producer;
        }

        public Producer GetProducerById(int id) => _context.Producers.FirstOrDefault(n => n.Id == id);

        public ProducerWithMoviesAndActorsVM GetProducerData(int producerId)
        {
            var _producerData = _context.Producers.Where(n => n.Id == producerId)
                .Select(n => new ProducerWithMoviesAndActorsVM()
                {
                    Name = n.Name,
                    MovieActors = n.Movies.Select(n => new MovieActorVM()
                    {
                        MovieTitle = n.Title,
                        MovieActors = n.Movie_Actors.Select(n => n.Actor.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            _logger.LogInformation($"Producer fetched successfully! Producer: {_producerData.Name}");

            return _producerData;
        }

        public List<Producer> GetAllProducers(string searchString)
        {
            IQueryable<Producer> allProducers = _context.Producers.ToList().AsQueryable();
            var producers = allProducers.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                producers = allProducers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
                _logger.LogInformation($"Producers fetched successfully with searchString {searchString}!");
            }

            return producers;
        }

        public void DeleteProducerById(int id)
        {
            var _producer = _context.Producers.FirstOrDefault(n => n.Id == id);

            if (_producer != null)
            {
                _context.Producers.Remove(_producer);
                _context.SaveChanges();
                _logger.LogInformation($"Producer with id {id} has been deleted successfully!");
            }
            else
            {
                throw new Exception($"The producer with id: {id} does not exist");
            }
        }
    }
}
