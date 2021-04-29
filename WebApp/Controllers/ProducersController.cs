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
    public class ProducersController : ControllerBase
    {
        public ProducersService _producersService;

        //injecting the ProducersService
        public ProducersController(ProducersService producersService)
        {
            _producersService = producersService;
        }

        //adding a new producer record to db
        [HttpPost("add-producer")]
        public IActionResult AddProducer([FromBody] ProducerVM producer)
        {
            var newProducer = _producersService.AddProducer(producer);
            return Created(nameof(AddProducer), newProducer);
        }

        //returns the producer 
        [HttpGet("get-producer-by-id/{id}")]
        public IActionResult GetProducerById(int id)
        {
            var _response = _producersService.GetProducerById(id);
            if(_response != null)
            {
                return Ok(_response);
            }
            else
            {
                return NotFound();
            }
        }

        //returns the details of the given producer (even its movies and the actors in it)
        [HttpGet("get-producer-data/{id}")]
        public IActionResult GetProducerData(int id)
        {
            var _response = _producersService.GetProducerData(id);
            return Ok(_response);
        }

        [HttpDelete("delete-producer-by-id/{id}")]
        public IActionResult DeleteProducerById(int id)
        {
            _producersService.DeleteProducerById(id);
            return Ok();
        }
    }
}
