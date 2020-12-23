using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Shared;

namespace lapr5_masterdata_viagens.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ViagensDbContext _context;

        public UnitOfWork(ViagensDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}