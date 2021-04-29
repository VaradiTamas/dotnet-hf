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

        public void AddProducer(ProducerVM producer)
        {
            var _producer = new Producer()
            {
                Name = producer.Name
            };
            _context.Producers.Add(_producer);
            _context.SaveChanges();
        }
    }
}
