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
    public class ProducersController : ControllerBase
    {
        public ProducersService _producersService;

        //injecting the ProducersService
        public ProducersController(ProducersService producersService)
        {
            _producersService = producersService;
        }

        /// <summary>
        /// Creates a new producer
        /// </summary>
        /// <param name="producer">The producer to create</param>
        /// <returns>Returns the producer inserted</returns>
        /// <response code="201">Insert successful</response>
        [HttpPost("add-producer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddProducer([FromBody] ProducerVM producer)
        {
            try
            {
                var newProducer = _producersService.AddProducer(producer);
                return Created(nameof(AddProducer), newProducer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
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

        [HttpGet("get-all-producers")]
        public IActionResult GetAllProducers()
        {
            try
            {
                var _result = _producersService.GetAllProducers();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                return BadRequest("Couldn't fetch producers");
            }
        }

        [HttpDelete("delete-producer-by-id/{id}")]
        public IActionResult DeleteProducerById(int id)
        {
            try
            {
                _producersService.DeleteProducerById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("get-version")]
        public IActionResult Get()
        {
            return Ok("This is producers version 1");
        }
    }
}
