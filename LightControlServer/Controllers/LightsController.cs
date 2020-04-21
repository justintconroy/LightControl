using LightControlServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LightControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        private readonly LightControlModel _ctx;
        public LightsController(LightControlModel ctx)
        {
            _ctx = ctx;
        }

        // PUT: api/Lights/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Light light)
        {
            var dbLight = _ctx.Lights.Find(id);
            if (light == null) { return NotFound(); }

            dbLight.Color = light.Color;
            _ctx.SaveChanges();
            return Ok(light);
        }
    }
}
