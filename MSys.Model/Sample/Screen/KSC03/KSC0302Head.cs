using System;

namespace SCF.Model.Screen.KSC03
{
    /// <summary>
    /// KSC0302Head: 注文詳細更新画面のヘッダ(注文情報)表示クラス
    /// </summary>
    /// <remarks>
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    [Serializable()]
    public class KSC0302Head : Model.Screen.KSC02.KSC0202Disp
    {
        /// <summary>
        /// 金額合計
        /// </summary>
        public decimal TotalAmount { get; set; }

    }
}
