using Microsoft.EntityFrameworkCore;
using lapr5_masterdata_viagens.Domain.Vehicle;

namespace lapr5_masterdata_viagens.Repositories
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