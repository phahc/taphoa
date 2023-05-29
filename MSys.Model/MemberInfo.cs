using System;
using System.Collections.Generic;

namespace MSys.Model
{
    /// <summary>
    /// MemberInfo: ログイン情報格納クラス
    /// </summary>
    [Serializable()]
    public class MemberInfo
    {
        public string acc { get; set; }
        public string pass { get; set; }
        public string email { get; set; }

    }

}