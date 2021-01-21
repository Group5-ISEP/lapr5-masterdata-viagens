using NUnit.Framework;
using lapr5_masterdata_viagens.Domain.ImportData;
using System.Collections.Generic;
using System.IO;

namespace Viagens.Tests
{
    public class ImportDataTestWithXML
    {
        private ImportDataService _service = new ImportDataService(new MockVehicleDutyRepo(), new MockTripRepo(), new MockDriverDutyRepo(), new MockUnitOfWork());

        private string GetProjectDirectory()
        {
            var a = Directory.GetCurrentDirectory();
            var toRemove = a.Length - 16; //removes the bin/... from the path
            return a.Substring(0, toRemove);
        }


     /*    [Test]
        public void expectSuccessIfEveythingValid()
        {
            var testProjectPath = GetProjectDirectory();

            var fileStream = File.OpenRead(testProjectPath + "ImportData/XML/demo-lapr5.glx.xml");
            var task = _service.ImportDataFromFile("xml", fileStream);
            task.Wait();
            var result = task.Result;

            fileStream.Close();

            Assert.IsTrue(result.IsSuccess);
        } */

    }
}