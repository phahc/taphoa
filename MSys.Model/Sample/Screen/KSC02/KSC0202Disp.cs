using System;

namespace SCF.Model.Screen.KSC02
{
    /// <summary>
    /// KSC0302Disp: 注文更新画面クラス
    /// </summary>
    /// <remarks>
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    [Serializable()]
    public class KSC0202Disp : Table.Orders
    {
        /// <summary>
        /// 得意先名称 
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 担当者名称 
        /// </summary>
        public string EmployeeName { get; set; }
    }
}
