using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCF.Model
{
    public class FPGridViewInfo
    {
        #region "Property"
        /// <summary>
        /// UserCd
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UserCd { get; set; }

        /// <summary>
        /// OuterXml
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public byte[] OuterXml { get; set; }

        /// <summary>
        /// CreateUserCd
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CreateUserCd { get; set; }

        /// <summary>
        /// CreateUserDt
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Nullable<DateTime> CreateDt { get; set; }

        /// <summary>
        /// UpdateUserCd
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UpdateUserCd { get; set; }

        /// <summary>
        /// UpdateUserDt
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Nullable<DateTime> UpdateDt { get; set; }

        #endregion
    }
}
