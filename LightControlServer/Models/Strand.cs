using System.Collections.Generic;

namespace LightControlServer.Models
{
    public class Strand
    {
        public int Id { get; set; }
        public virtual IEnumerable<Light> Lights { get; set; }
    }

    public class Light
    {
        public int Id { get; set; }
        public string Color { get; set; }

        public virtual Strand Strand { get; set; }
    }

    public class StrandDto
    {
        public StrandDto() { }
        public StrandDto(Strand strand)
        {
            Id = strand.Id;
            Lights = new List<LightDto>();
            foreach(var light in strand.Lights)
            {
                Lights.Add(new LightDto(light));
            }
        }

        public int Id { get; set; }
        public List<LightDto> Lights { get; set; }
    }
    public class LightDto
    {
        public LightDto() { }
        public LightDto(Light light)
        {
            Id = light.Id;
            Color = light.Color;
        }
        public int Id { get; set; }
        public string Color { get; set; }
    }
}
