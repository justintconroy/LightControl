using LightControlServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LightControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrandController : ControllerBase
    {
        // GET: api/Strand
        [HttpGet]
        public IActionResult Get()
        {
            var strand = new Strand
            {
                Id = 1,
                Lights = new List<Light>
                {
                    new Light { Id = 1, Color = "#FF0000"},
                    new Light { Id = 2, Color = "#00FF00"},
                    new Light { Id = 3, Color = "#0000FF"},
                    new Light { Id = 4, Color = "#FF0000"},
                    new Light { Id = 5, Color = "#00FF00"},
                    new Light { Id = 6, Color = "#0000FF"},
                    new Light { Id = 7, Color = "#FF0000"},
                    new Light { Id = 8, Color = "#00FF00"},
                    new Light { Id = 9, Color = "#0000FF"},
                }
            };

            return Ok(strand);
        }

        // GET: api/Strand/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var strand = new Strand
            {
                Id = 1,
                Lights = new List<Light>
                {
                    new Light { Id = 1, Color = "#FF0000"},
                    new Light { Id = 2, Color = "#00FF00"},
                    new Light { Id = 3, Color = "#0000FF"},
                    new Light { Id = 4, Color = "#FF0000"},
                    new Light { Id = 5, Color = "#00FF00"},
                    new Light { Id = 6, Color = "#0000FF"},
                    new Light { Id = 7, Color = "#FF0000"},
                    new Light { Id = 8, Color = "#00FF00"},
                    new Light { Id = 9, Color = "#0000FF"},
                }
            };

            return Ok(strand);
        }

        // POST: api/Strand
        [HttpPost]
        public void Post([FromBody] string value)
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
