using System.Collections.Generic;

namespace LightControlServer.Models
{
    public class Strand
    {
        public int Id { get; set; }
        public IEnumerable<Light> Lights { get; set; }
    }

    public class Light
    {
        public int Id { get; set; }
        public string Color { get; set; }
    }
}
