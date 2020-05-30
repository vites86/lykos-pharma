using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olga.DAL.Entities;

namespace Olga.BLL.BusinessModels
{
    public class FileProcessor
    {
        public string GetAdditionalFileFolder(string serverMapPath, ProductDocument document)
        {
            StringBuilder targetFolder = new StringBuilder(serverMapPath);
            if (document.IsEan)
            {
                targetFolder.Append(ProductAdditionalDocsType.Ean);
            }
            if (document.IsGtin)
            {
                targetFolder.Append(ProductAdditionalDocsType.Ean);
            }
            return targetFolder.ToString();
        }

        public string GetAdditionalFileUniquePath(string serverMapPath, ProductAdditionalDocsType documentType, string documentName)
        {
            StringBuilder targetFolder = new StringBuilder(serverMapPath);

            var fileExt = Path.GetExtension(documentName);
            var fileTrimmName = documentName.Replace(",", "_").Replace(" ", "_").Replace("%", "_");
            var uniqueFileName = $"{Path.GetFileNameWithoutExtension(fileTrimmName)}_{Guid.NewGuid().ToString().Substring(0, 6)}{fileExt}";

            switch (documentType)
            {
                case ProductAdditionalDocsType.Ean:
                    targetFolder.Append(ProductAdditionalDocsType.Ean);
                    break;
                //case ProductAdditionalDocsType.Gtin:
                //    targetFolder.Append(ProductAdditionalDocsType.Gtin);
                //    break;
            }
           
            return targetFolder.Append("\\").Append(uniqueFileName).ToString();
        }

        public bool DeleteFile(string fileFullPath)
        {
            try
            {
                var targetPath = fileFullPath.Replace(@"/", @"\");
                if (File.Exists(targetPath))
                {
                    File.Delete($"{targetPath}");
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool SaveFile(string fileName, string targetFolder)
        {
            return true;
        }
    }
}
