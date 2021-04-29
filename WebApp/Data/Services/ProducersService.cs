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

        //inject AppDbContext file
        public ProducersService(AppDbContext context)
        {
            _context = context;
        }

        public Producer AddProducer(ProducerVM producer)
        {
            var _producer = new Producer()
            {
                Name = producer.Name
            };
            _context.Producers.Add(_producer);
            _context.SaveChanges();

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

            return _producerData;
        }

        public void DeleteProducerById(int id)
        {
            var _producer = _context.Producers.FirstOrDefault(n => n.Id == id);

            if (_producer != null)
            {
                _context.Producers.Remove(_producer);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The producer with id: {id} does not exist");
            }
        }
    }
}
