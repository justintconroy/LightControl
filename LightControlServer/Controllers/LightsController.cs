using LightControlServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace LightControlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LightsController : ControllerBase
    {
        private readonly LightControlModel _ctx;
        private readonly IManagedMqttClient _mqtt;

        public LightsController(
            LightControlModel ctx,
            IManagedMqttClient mqtt)
        {
            _ctx = ctx;
            _mqtt = mqtt;
        }

        // PUT: api/Lights/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LightDto light)
        {
            var dbLight = _ctx.Lights
                .Include(l => l.Strand.Lights)
                .FirstOrDefault(l => l.Id == id);

            if (light == null) { return NotFound(); }

            dbLight.Color = light.Color;
            _ctx.SaveChanges();

            var strandDto = new StrandDto(dbLight.Strand);
            var json = JsonSerializer.Serialize(strandDto);
            await _mqtt.PublishAsync($"strand/{dbLight.Strand.Id}", json);

            return Ok(new LightDto(dbLight));
        }
    }
}
