using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olga.Util
{
    public static class FileHelper
    {
        public static string TrimFileName(this string fileName)
        {
            return fileName.Replace(",", "_").Replace("#", "№").Replace(" ", "_")
                .Replace("(", "_").Replace(")", "_").Replace("%", "_");
        }
    }
}