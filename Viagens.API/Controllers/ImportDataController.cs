using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using lapr5_masterdata_viagens.Domain.ImportData;
using System.Collections.Generic;

namespace lapr5_masterdata_viagens.Controllers
{
    [ApiController]
    [Route("api/importdata")]
    public class ImportDataController : ControllerBase
    {
        private readonly ImportDataService _service;

        public ImportDataController(ImportDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<List<string>>> Create(IFormFile file)
        {
            var fileType = file.ContentType.Remove(0, 12); //remove 'application/' prefix

            var result = await _service.ImportDataFromFile(fileType, file.OpenReadStream());
            if (result.IsSuccess == false)
            {
                return Conflict(result.Error);
            }
            return Ok(result.Value);
        }
    }
}