using System;

namespace MSys.Core.Util
{
    public class DateTimeUtil
    {
        public const string YYYMMDD = "dd/MM/yyyy";
        public const string YYYMMDD_HHMMSS = "dd/MM/yyyy HH:mm:ss";

        /// <summary>
        /// yyyy/MM/dd
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns>true or false</returns>
        public static bool IsDate(object strDate)
        {
            try
            {
                DateTime datDate = DateTime.Parse(strDate.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// string: 12/6/2016 10:24:44 AM
        /// </summary>
        /// <returns>12/6/2016</returns>
        public static string DateTimeToStringDate(Nullable<DateTime> dateTime)
        {
            if (dateTime != null)
            {
                return dateTime.Value.ToString(YYYMMDD);
            }
            return string.Empty;
        }
    }
}
