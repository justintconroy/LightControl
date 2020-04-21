using LightControlServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace LightControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrandsController : ControllerBase
    {
        private readonly LightControlModel _ctx;
        public StrandsController(
            LightControlModel ctx)
        {
            _ctx = ctx;
        }

        // GET: api/Strand
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ctx.Strands);
        }

        // GET: api/Strand/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            return Ok(_ctx.Strands
                .FirstOrDefault(s => s.Id == id));
        }

        // POST: api/Strand
        [HttpPost]
        public void Post([FromBody] Strand value)
        {

        }

        // PUT: api/Strand/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
