using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Services;
using WebApp.Data.ViewModels;

namespace WebApp.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v1/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        public ActorsService _actorsService;

        //injecting the ActorsService
        public ActorsController(ActorsService actorsService)
        {
            _actorsService = actorsService;
        }

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

        [HttpGet("get-version")]
        public IActionResult Get()
        {
            return Ok("This is actors version 1");
        }
    }
}
