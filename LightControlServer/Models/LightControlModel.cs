using Microsoft.EntityFrameworkCore;

namespace LightControlServer.Models
{
    public class LightControlModel : DbContext
    {
        public LightControlModel(DbContextOptions<LightControlModel> options)
            : base(options) { }
        public DbSet<Strand> Strands { get; set; }
        public DbSet<Light> Lights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("LightControlDb");

    }
}
