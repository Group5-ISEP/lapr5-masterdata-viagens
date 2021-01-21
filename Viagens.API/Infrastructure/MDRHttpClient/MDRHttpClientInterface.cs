using System.Collections.Generic;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Path;
using lapr5_masterdata_viagens.Domain.Node;
using lapr5_masterdata_viagens.Shared;

namespace lapr5_masterdata_viagens.Infrastructure.MDRHttpClient
{
    public interface MDRHttpClientInterface
    {
        Task<Result<List<PathDTO>>> FetchPathsByLine(string line);
        Task<Result<NodeDto>> FetchNodeById(string nodeId);
    }
}