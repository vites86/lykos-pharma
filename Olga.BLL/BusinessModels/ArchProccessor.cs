using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.BLL.Interfaces;
using Ionic.Zip;
using NUnrar.Archive;



namespace Olga.BLL.BusinessModels
{
     public class ArchProccessor : IArchProccessor
    {

        public List<string> ProcessArchive(string archivePath, string pathToExtract)
        {
            List<string> exrtactedFiles = new List<string>();

            if (!RarArchive.IsRarFile(archivePath) && !ZipFile.IsZipFile(archivePath))
            {
                return null;
            }

            if (ZipFile.IsZipFile(archivePath))
            {
                exrtactedFiles = ProcessZipArchive(archivePath, pathToExtract);
            }
            if (RarArchive.IsRarFile(archivePath))
            {
                ProcessRarArchive(archivePath, pathToExtract);
            }
            return exrtactedFiles;
        }

        public List<string> ProcessZipArchive(string archivePath, string pathToExtract)
        {
            try
            {
                List<string> exrtactedFiles = new List<string>();
                using (ZipFile zip = ZipFile.Read(archivePath))
                {
                    var renamedFolder = string.Empty;
                    var guid = Guid.NewGuid().ToString().Substring(0, 6);

                    foreach (ZipEntry zipEntry in zip)
                    {
                        var zipFileName = zipEntry.FileName;
                        var extractFolder = Path.Combine(pathToExtract, zipFileName);
                        int slashIndex = zipFileName.IndexOf(@"/", StringComparison.CurrentCulture);

                        if (Directory.Exists(extractFolder) || File.Exists(extractFolder))
                        {
                            extractFolder = slashIndex != -1 ? zipFileName.Substring(0, slashIndex) : extractFolder;
                            renamedFolder = zipEntry.IsDirectory
                                ? string.Format($"{zipFileName.Replace(@"/", "")}_{guid}")
                                : string.Format($"{extractFolder}_{guid}");
                            pathToExtract = Path.Combine(pathToExtract, renamedFolder);
                            Directory.CreateDirectory(pathToExtract);
                            zipEntry.Extract(pathToExtract);
                        }
                        else
                        {
                            zipEntry.Extract(pathToExtract);
                        }

                        if (zipEntry.IsDirectory) continue;

                        var fileNameWithFolders = string.Concat(@"/Archives/", renamedFolder, @"/", zipFileName);
                        exrtactedFiles.Add(fileNameWithFolders);
                    }

                }
                return exrtactedFiles;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        public void ProcessRarArchive(string archivePath, string PathToExtract)
        {

        }
    }
}
