using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olga.BLL
{ public static class FileHelper
    {
        public static string TrimFileName(this string fileName)
        {
            return fileName.Replace(",", "_").Replace("#", "№").Replace(" ", "_")
                .Replace("(", "_").Replace(")", "_").Replace("%", "_");
        }
    }
}
