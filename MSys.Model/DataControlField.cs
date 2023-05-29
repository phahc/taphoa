using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCF.Model
{
    /// <summary>
    /// FPGridViewで使用するDataControlFieldのプロパティをModel化
    /// </summary>
    public class DataControlField
    {
        #region "Property"
        /// <summary>
        /// FiledTypeName
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string FiledTypeName { get; set; }

        /// <summary>
        /// FieldID
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string FieldID { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Width { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Visible { get; set; }

        /// <summary>
        /// FieldName
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string FieldName { get; set; }
        
        #endregion
    }
}
