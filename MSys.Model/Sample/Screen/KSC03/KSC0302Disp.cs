using SCF.Model.Table;
using System;

namespace SCF.Model.Screen.KSC03
{
    /// <summary>
    /// KSC0302Disp: ’•¶Ú×XV‰æ–ÊƒNƒ‰ƒX
    /// </summary>
    /// <remarks>
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    [Serializable()]
    public class KSC0302Disp : OrderDetails
    {
        /// <summary>
        /// List•\Ž¦—pNo.
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// ‹àŠz‡Œv
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// »•i–¼
        /// </summary>
        public string ProductName { get; set; }
    }
}
