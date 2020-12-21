using System.Threading.Tasks;

namespace lapr5_masterdata_viagens.Domain.Shared
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}