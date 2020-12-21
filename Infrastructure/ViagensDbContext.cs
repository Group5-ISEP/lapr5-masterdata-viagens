using Microsoft.EntityFrameworkCore;
using lapr5_masterdata_viagens.Domain.Vehicles;

namespace lapr5_masterdata_viagens.Infrastructure
{
    public class ViagensDbContext : DbContext
    {
        public ViagensDbContext(DbContextOptions<ViagensDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
    }
}