using lapr5_masterdata_viagens.Domain.Shared;
using System.Threading.Tasks;


namespace Viagens.Tests
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public async Task<int> CommitAsync()
        {
            return 0;
        }
    }
}