using System;
namespace SCF.Model
{
    /// <summary>
    /// MemberInfo: ログイン情報格納クラス
    /// </summary>
    [Serializable()]
    public class KinMemberInfo
    {
        /// <summary>
        /// 顧客コード
        /// </summary>
        public string KKYK_CD { get; set; }
        /// <summary>
        /// カタログサービス利用フラグ
        /// </summary>
        public string CTLG_SRVC_RY_FLG { get; set; }
        /// <summary>
        /// SSOフラグ
        /// </summary>
        public string SSO_FLG { get; set; }
    }
}