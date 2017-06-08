using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GridDataHackfest
{
    public class CustomClass
    {
        public static Double? dbl_TryParse_USQL(string doubleString)
        {
            Double doubleValue;

            if (Double.TryParse(doubleString, out doubleValue))
                return doubleValue;
            else
                return null;
        }
    }
}
