using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Models;
using WebApp.Data.ViewModels;

namespace WebApp.Data.Services
{
    public class ActorsService
    {
        private AppDbContext _context;

        //inject AppDbContext file
        public ActorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddActor(ActorVM actor)
        {
            var _actor = new Actor()
            {
                FullName = actor.FullName
            };
            _context.Actors.Add(_actor);
            _context.SaveChanges();
        }

        public ActorWithMoviesVM GetActorWithMovies(int actorId)
        {
            var _actor = _context.Actors.Where(n => n.Id == actorId).Select(n => new ActorWithMoviesVM()
            {
                FullName = n.FullName,
                MovieTitles = n.Movie_Actors.Select(n => n.Movie.Title).ToList()
            }).FirstOrDefault();

            return _actor;
        }
    }
}
