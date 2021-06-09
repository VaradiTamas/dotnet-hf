using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    [ApiController]
    public class ProducersController : ControllerBase
    {
        public ProducersService _producersService;
        private readonly ILogger<ProducersController> _logger;

        //injecting the ProducersService
        public ProducersController(ProducersService producersService, ILogger<ProducersController> logger)
        {
            _logger = logger;
            _producersService = producersService;
        }

        [HttpPost("add-producer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddProducer([FromBody] ProducerVM producer)
        {
            _logger.LogInformation($"AddProducer has been called with parameter name: {producer.Name}");

            try
            {
                var newProducer = _producersService.AddProducer(producer);
                return Created(nameof(AddProducer), newProducer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't add producer because {ex.Message}.");
                return BadRequest(ex.Message);
            }
            
        }

        //returns the producer 
        [HttpGet("get-producer-by-id/{id}")]
        public IActionResult GetProducerById(int id)
        {
            _logger.LogInformation($"GetProducer has been called with parameter id: {id}");

            var _response = _producersService.GetProducerById(id);
            if(_response != null)
            {
                _logger.LogInformation($"Successfully returned a product with id: {_response.Id} and name: {_response.Name} ");
                return Ok(_response);
            }
            else
            {
                _logger.LogError($"Product with id: {id} not found!");
                return NotFound();
            }
        }

        //returns the details of the given producer (even its movies and the actors in it)
        [HttpGet("get-producer-data/{id}")]
        public IActionResult GetProducerData(int id)
        {
            _logger.LogInformation($"GetProducerData has been called with parameter id: {id}");

            var _response = _producersService.GetProducerData(id);
            return Ok(_response);
        }

        [HttpGet("get-all-producers")]
        public IActionResult GetAllProducers(string searchString)
        {
            //throw new Exception("This is an exception thrown from GetAllProducers()!");
            try
            {
                _logger.LogInformation($"GetAllProducers has been called with parameter searchString: {searchString}");

                var _result = _producersService.GetAllProducers(searchString);
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't fetch producers!");
                return BadRequest("Couldn't fetch producers");
            }
        }

        [HttpDelete("delete-producer-by-id/{id}")]
        public IActionResult DeleteProducerById(int id)
        {
            try
            {
                _logger.LogInformation($"DeleteProducerById has been called with parameter id: {id}");
                _producersService.DeleteProducerById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Couldn't delete producer because {ex.Message}.");
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
