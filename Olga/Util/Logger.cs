using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Olga.Util
{
    public static class Logger
    {
        public static ILog Log { get; } =
            LogManager.GetLogger("LOGGER");
    }
}