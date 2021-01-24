using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Path;
using lapr5_masterdata_viagens.Domain.Node;
using lapr5_masterdata_viagens.Shared;
using System.Text.Json;

namespace lapr5_masterdata_viagens.Infrastructure.MDRHttpClient
{
    public class MDRHttpClientService : MDRHttpClientInterface
    {
        public HttpClient Client { get; private set; }

        public MDRHttpClientService(HttpClient client)
        {
            string address;
            /* if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                address = "http://localhost:3000/";
            else */
                address = "https://lapr5-3di-g5-masterdata.herokuapp.com/";

            client.BaseAddress = new Uri(address);
            Client = client;
        }

        public async Task<Result<List<PathDTO>>> FetchPathsByLine(string line)
        {
            var response = await Client.GetAsync(
            "api/path/" + line);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync
                <List<PathDTO>>(responseStream);

            return Result<List<PathDTO>>.Ok(result);
        }

        public async Task<Result<NodeDto>> FetchNodeById(string nodeId)
        {
            var response = await Client.GetAsync(
                        "api/node/" + nodeId);

            response.EnsureSuccessStatusCode();


            using var responseStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync
                <NodeDto>(responseStream);

            return Result<NodeDto>.Ok(result);

        }

    }
}