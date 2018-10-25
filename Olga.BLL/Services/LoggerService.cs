using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Olga.BLL.Services
{
    internal static class LoggerService
    {
        public static ILog Log { get; } =
            LogManager.GetLogger("LOGGER");
    }
}
