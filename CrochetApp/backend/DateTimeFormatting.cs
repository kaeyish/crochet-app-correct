using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend
{
    public static class DateTimeFormatting
    {
        public static string FormatSQL(DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yyyy", (CultureInfo.InvariantCulture));
        }
    }
}
