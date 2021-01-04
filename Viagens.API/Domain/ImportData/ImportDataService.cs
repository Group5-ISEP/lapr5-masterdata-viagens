using System.Threading.Tasks;
using lapr5_masterdata_viagens.Shared;
using lapr5_masterdata_viagens.Domain.Shared;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace lapr5_masterdata_viagens.Domain.ImportData
{
    public class ImportDataService
    {
        //private readonly IImportDataRepo _repo;
        //private readonly IUnitOfWork _unitOfWork;

        public ImportDataService(
            // IImportDataRepo repo,
            //IUnitOfWork unitOfWork
            )
        {
            // this._unitOfWork = unitOfWork;
            //this._repo = repo;
        }

        public async Task<Result<List<string>>> ImportDataFromFile(IFormFile file)
        {
            var a = new List<string>() { "a" };
            return Result<List<string>>.Ok(a);
        }

    }
}