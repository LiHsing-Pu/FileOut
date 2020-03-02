using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MPB.MTNET.EL.Web.Helpers
{
    public static class DataExtension
    {
        private static readonly string numericPattern = "^\\d+(?:\\.\\d*)?$";

        public static bool IsNumeric(this string data)
        {
            return !string.IsNullOrEmpty(data) && Regex.IsMatch(data, numericPattern);
        }

        public static int ToInt(this string data)
        {
            return int.Parse(data);
        }

        public static double ToDouble(this string data)
        {
            return double.Parse(data);
        }
    }
}