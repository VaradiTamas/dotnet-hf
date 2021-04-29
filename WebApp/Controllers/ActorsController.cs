using Microsoft.AspNetCore.Http;
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
    public class ActorsController : ControllerBase
    {
        public ActorsService _actorsService;

        //injecting the ActorsService
        public ActorsController(ActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        //adding a new actor record to db
        [HttpPost("add-actor")]
        public IActionResult AddActor([FromBody] ActorVM actor)
        {
            _actorsService.AddActor(actor);
            return Ok();
        }

        [HttpGet("get-actor-with-movies-by-id/{id}")]
        public IActionResult GetActorWithMovies(int id)
        {
            var response = _actorsService.GetActorWithMovies(id);
            return Ok(response);
        }
    }
}
