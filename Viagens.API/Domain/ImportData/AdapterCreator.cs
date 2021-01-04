using System.IO;

namespace lapr5_masterdata_viagens.Domain.ImportData
{
    public class AdapterCreator
    {
        ///<summary>
        /// Returns the parser based on the type of the file given
        ///</summary>
        public static Parser CreateParser(string fileType, Stream fileStream)
        {
            switch (fileType)
            {
                case "xml":
                    return new XMLParser(fileStream);
                default:
                    throw new System.Exception("File type not recoznized.");
            }

        }
    }
}