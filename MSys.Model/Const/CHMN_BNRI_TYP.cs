using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace CMN.Model.ConstData
{
    /// <summary>
    /// 注文分類区分
    /// </summary>
    public class CHMN_BNRI_TYP
    {
        /// <summary>
        /// 標準
        /// </summary>
        public const string Standard = "001";
    }

    /// <summary>
    /// 用紙用途区分(商品別用紙カバー)
    /// </summary>
    public class YSH_YT_TYP
    {
        //本文
        public const string TEXT = "001";
        //表紙
        public const string HYSH_YSH_CD = "002";
        //表カバー
        public const string OMT_CVR_CLR_TYP = "004";
        //裏カバー
        public const string UR_CVR_CLR_TYP = "005";
    }

    /// <summary>
    /// カラー区分(商品別用紙カバー)
    /// </summary>
    /// <remarks>
    /// 白黒:"001", カラー:"003"は HNYTYP.COPY_TYPを使用
    /// </remarks>
    public class SHHNBTS_YSHCVR_CLR_TYP
    {
        /// <summary>
        /// 共用
        /// </summary>
        public const string SHARE = "999";
    }

}