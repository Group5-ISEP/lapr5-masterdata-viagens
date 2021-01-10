using System.Collections.Generic;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.Path;
using lapr5_masterdata_viagens.Infrastructure.MDRHttpClient;
using lapr5_masterdata_viagens.Shared;

namespace Viagens.Tests
{
    public class MockMDRHttpClient : MDRHttpClientInterface
    {
        public async Task<Result<List<PathDTO>>> FetchPathsByLine(string line)
        {
            var pathdto1 = new PathDTO()
            {
                LineId = "Line:1",
                PathId = "Path:1",
                IsEmpty = false,
                Orientation = "To",
                Segments = new List<SegmentDTO>(){
                        new SegmentDTO(){
                            StartNodeId = "Node:1",
                            EndNodeId="Node:2",
                            Distance =2000,
                            Duration = 500,
                            Order = 1
                        },
                        new SegmentDTO(){
                            StartNodeId = "Node:2",
                            EndNodeId="Node:3",
                            Distance =1500,
                            Duration = 500,
                            Order = 2
                        }
                    }
            };

            var pathdto2 = new PathDTO()
            {
                LineId = "Line:1",
                PathId = "Path:2",
                IsEmpty = false,
                Orientation = "From",
                Segments = new List<SegmentDTO>(){
                        new SegmentDTO(){
                            StartNodeId = "Node:3",
                            EndNodeId="Node:2",
                            Distance =1500,
                            Duration = 500,
                            Order = 1
                        },
                        new SegmentDTO(){
                            StartNodeId = "Node:2",
                            EndNodeId="Node:1",
                            Distance =2000,
                            Duration = 500,
                            Order = 2
                        }
                    }
            };

            if (line == "Line:1")
                return Result<List<PathDTO>>.Ok(new List<PathDTO>() { pathdto1, pathdto2 });

            return Result<List<PathDTO>>.Ok(new List<PathDTO>());

        }
    }
}