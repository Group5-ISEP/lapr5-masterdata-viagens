using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Path;
using lapr5_masterdata_viagens.Shared;

namespace lapr5_masterdata_viagens.Infrastructure.MDRHttpClient
{
    public class MDRHttpClientService : MDRHttpClientInterface
    {
        public HttpClient Client { get; private set; }

        public MDRHttpClientService(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:3000/");

            Client = client;
        }

        public async Task<Result<List<PathDTO>>> FetchPathsByLine(string line)
        {
            // TODO: implement fetch
            return Result<List<PathDTO>>.Fail(" fetch Not implemented");
        }
    }
}