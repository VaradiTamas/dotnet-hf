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
            _producersService.AddProducer(producer);
            return Ok();
        }
    }
}
