using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Olga.BLL.Interfaces;
using Ionic.Zip;
using NUnrar.Archive;
using ZipFile = Ionic.Zip.ZipFile;
using ZipFileCustom = System.IO.Compression.ZipFile;

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

        public static string EncodeFilename(string filename)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] bytes = utf8.GetBytes(filename);
            char[] chars = new char[bytes.Length];
            for (int index = 0; index < bytes.Length; index++)
            {
                chars[index] = Convert.ToChar(bytes[index]);
            }

            string s = new string(chars);
            return s;
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
                            try
                            {
                                zipEntry.Extract(pathToExtract);
                            }
                            catch (Exception ex)
                            {
                                return new List<string>() { String.Concat($"Error in {pathToExtract}", ex.Message.ToString()) };
                            }
                        }

                        if (zipEntry.IsDirectory) continue;

                        var fileNameWithFolders = string.Concat(@"/Archives/", renamedFolder, @"/", zipFileName);
                        exrtactedFiles.Add(fileNameWithFolders);
                    }

                }
                return exrtactedFiles;
            }
            catch (Exception ex)
            {
                return new List<string>() { String.Concat("Error", ex.Message.ToString()) };
            }
        }

        public void ProcessRarArchive(string archivePath, string PathToExtract)
        {

        }

        public string DownloadZip(string filesToDownload, string archName, string productId, string pathToFilesFolder)
        {
                List<string> files = filesToDownload.Split(';').ToList();
                var curDate = DateTime.Now.ToShortDateString().Replace("-", "").Replace(":", "").Replace(".", "");
                var archiveOutFolder = AppDomain.CurrentDomain.BaseDirectory+($"\\tempout\\{curDate}\\{productId}\\");
                var tempFolder = AppDomain.CurrentDomain.BaseDirectory +"\\temp\\";
                if (!Directory.Exists(archiveOutFolder)) Directory.CreateDirectory(archiveOutFolder);
                var archive = string.Concat(archiveOutFolder, archName);

                ClearTempDirectories(curDate, tempFolder);
                files.ForEach(f => CopyFile(pathToFilesFolder, f, tempFolder));

                if (File.Exists(archive)) File.Delete(archive);
                ZipFileCustom.CreateFromDirectory(tempFolder, archive, CompressionLevel.Fastest, false);

                return $"../tempout/{curDate}/{productId}/{archName}";
        }

        public void CopyFile(string _pathToFilesFolder, string fileName, string pathToFolder)
        {
            var pathToFilesFolder = string.Concat(AppDomain.CurrentDomain.BaseDirectory, _pathToFilesFolder);
            var fileToCopy = string.Concat(pathToFilesFolder, fileName);
            var pathToCopy = string.Concat(pathToFolder, fileName);

            if (File.Exists(fileToCopy))
            {
                string directory = Path.GetDirectoryName(pathToCopy);
                CreateDirectory(new DirectoryInfo(directory));
                File.Copy(fileToCopy, pathToCopy, true);
            }
        }

        public static void CreateDirectory(DirectoryInfo directory)
        {
            if (!directory.Parent.Exists)
                CreateDirectory(directory.Parent);
            directory.Create();
        }

        public void ClearTempDirectories(string curDate, string folderToClear)
        {
            var folders = from folder in
                          Directory.EnumerateDirectories(AppDomain.CurrentDomain.BaseDirectory + "\\tempout\\")
                          where !folder.ToLower().Contains(curDate)
                          select folder;
            folders.ToList().ForEach(f => Directory.Delete(f, true));

            Directory.EnumerateFiles(folderToClear).ToList().ForEach(f => File.Delete(f));
            Directory.EnumerateDirectories(folderToClear).ToList().ForEach(f => Directory.Delete(f, true));
        }

    }
}
