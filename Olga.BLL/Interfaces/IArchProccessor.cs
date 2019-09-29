using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.BLL.Interfaces
{
    public interface IArchProccessor
    {
        List<string> ProcessArchive(string archivePath, string pathToExtract);
        List<string> ProcessZipArchive(string archivePath, string pathToExtract);
        void ProcessRarArchive(string archivePath, string PathToExtract);
        string DownloadZip(string filesToDownload, string archName, string productId, string pathToFilesFolder);
    }
}
