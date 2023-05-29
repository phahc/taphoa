using System;

namespace MSys.Core.Util
{
    public class DecimalUtil
    {
        //CONST
        public const string REMOVE_TRAILING_ZEROS = "{0:G29}";
        public const string DECIMAL_TO_NUMBER = "{0:#,###.##}";
        public const string DECIMAL_TO_MONEY = "{0:#,###.##}";

        /// <summary>
        /// DecimalFormat
        /// </summary>
        /// <param name="decimalNumber"></param>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        public static string DecimalFormat(Nullable<decimal> decimalNumber, string stringFormat)
        {
            string result = string.Empty;
            if (decimalNumber != null && decimalNumber != decimal.Zero)
            {
                result = string.Format(stringFormat, decimalNumber);
            }
            return result;
        }

        /// <summary>
        /// 69022.00
        /// </summary>
        /// <param name="decimalNumber"></param>
        /// <returns>69,022</returns>
        public static string DecimalToMoney(Nullable<decimal> decimalNumber)
        {
            return DecimalFormat(decimalNumber, DECIMAL_TO_MONEY);
        }
    }
}
