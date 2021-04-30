using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Models;
using WebApp.Data.ViewModels;

namespace WebApp.Data.Services
{
    public class MoviesService
    {
        private AppDbContext _context;

        //inject AppDbContext file
        public MoviesService(AppDbContext context)
        {
            _context = context;
        }

        //add a new movie record to the Movies database, but the Rate and NumOfRates attributes
        //will be null because we can add these data in another method
        public void AddMovie(MovieVM movie)
        {
            var _movie = new Movie()
            {
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Rate = 0,
                NumOfRates = 0,
                DateAdded = DateTime.Now,
                ProducerId = movie.ProducerId
            };
            _context.Movies.Add(_movie);
            _context.SaveChanges();

            //adding actors to the movie (adding records to the Movie_Actors table)
            foreach (var id in movie.ActorIds)
            {
                var _movie_actor = new Movie_Actor()
                {
                    MovieId = _movie.Id,
                    ActorId = id            //we are going through this in the foreach
                };
                _context.Movie_Actors.Add(_movie_actor);
                _context.SaveChanges();
            }
        }

        //returns all movie records from database
        public List<Movie> GetAllMovies() => _context.Movies.ToList();

        //returns the only movie which id corresponds with the given one
        //if there is no such movie with the given id it returns null
        public MovieWithActorsVM GetMovieById(int movieId)
        {
            var _movieWithActors = _context.Movies.Where(x => x.Id == movieId).Select(movie => new MovieWithActorsVM()
            {
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                ProducerName = movie.Producer.Name,
                ActorNames = movie.Movie_Actors.Select(n => n.Actor.FullName).ToList()
            }).FirstOrDefault();

            return _movieWithActors;
        }

        public Movie RateMovie(int movieId, int rate)
        {
            var _movie = _context.Movies.FirstOrDefault(n => n.Id == movieId);
            if (_movie != null)
            {
                _movie.Rate = (((_movie.Rate) * _movie.NumOfRates) + rate) / (_movie.NumOfRates + 1);
                _movie.NumOfRates++;

                _context.SaveChanges();
            }

            return _movie;
        }

        //returns a list of movies with the given genre
        public List<Movie> GetMoviesByGenre(string genre) => _context.Movies.Where(x => x.Genre == genre).ToList();

        //update an already existing movie (which id corresponds with the given movieId) with the given movie
        //if there is no such movie with the given id it returns null
        public Movie UpdateMovieById(int movieId, MovieVM movie)
        {
            var _movie = _context.Movies.FirstOrDefault(n => n.Id == movieId);
            if (_movie != null)
            {
                _movie.Title = movie.Title;
                _movie.Description = movie.Description;
                _movie.Rate = movie.Rate.Value;
                _movie.Genre = movie.Genre;

                _context.SaveChanges();
            }

            return _movie;
        }

        //if there is a movie with the given id it deletes the movie
        public void DeleteMovieById(int movieId)
        {
            var _movie = _context.Movies.FirstOrDefault(n => n.Id == movieId);
            if (_movie != null)
            {
                _context.Movies.Remove(_movie);
                _context.SaveChanges();
            }
        }
    }
}
