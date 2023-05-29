using System;
using System.Web;

namespace MSys.Core.Helpers
{
    public class CacheHelper
    {
        /// <summary>
        /// Vô hiệu hóa bộ nhớ cache của tệp bên ngoài
        /// </summary>
        /// <param name="contentpath"></param>
        /// <returns></returns>
        /// <remarks>Cho đường dẫn một đối số Ticks</remarks>
        public static string Invalid(string contentpath)
        {
            // Đường dẫn ảo -> Đường dẫn ứng dụng
            string filePath = VirtualPathUtility.ToAbsolute(contentpath);
            // Cung cấp cho Ticks
            filePath = string.Format("{0}?v={1}", filePath, DateTime.Now.Ticks);

            return filePath;
        }
    }
}
