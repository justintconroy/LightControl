using LightControlServer.Models;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Put(int id, [FromBody] Light light)
        {
            var dbLight = _ctx.Lights.Find(id);
            if (light == null) { return NotFound(); }

            dbLight.Color = light.Color;
            _ctx.SaveChanges();

            await _mqtt.PublishAsync("hello/world", "hi!");

            return Ok(light);
        }
    }
}
