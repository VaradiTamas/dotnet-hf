﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Services;
using WebApp.Data.ViewModels;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public MoviesService _moviesService;

        //injecting the MoviesService
        public MoviesController(MoviesService moviesService)
        {
            _moviesService = moviesService;
        }

        //adding a new movie record to db
        [HttpPost("add-movie")]
        public IActionResult AddMovie([FromBody] MovieVM movie)
        {
            _moviesService.AddMovie(movie);
            return Ok();
        }

        //getting all the movies from db
        [HttpGet("get-all-movies")]
        public IActionResult GetAllMovies()
        {
            var allMovies = _moviesService.GetAllMovies();
            return Ok(allMovies);
        }

        //getting the only movie which id corresponds with the given id
        [HttpGet("get-movie-by-id/{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _moviesService.GetMovieById(id);
            return Ok(movie);
        }

        //updating the movie which id corresponds with the given id
        [HttpPut("update-movie-by-id/{id}")]
        public IActionResult UpdateMovieById(int id, [FromBody] MovieVM movie)
        {
            var updatedMovie = _moviesService.UpdateMovieById(id, movie);
            return Ok(updatedMovie);
        }

        //deletinig the movie that the given id corresponds with
        [HttpDelete("delete-movie-by-id/{id}")]
        public IActionResult DeleteMovieById(int id)
        {
            _moviesService.DeleteMovieById(id);
            return Ok();
        }
    }
}
