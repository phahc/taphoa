using System;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace MSys.Core.Util
{
    public class CommonUtil
    {
        #region "Kết quả thông báo"
        /// <summary>
        /// NHận kết quả thông báo
        /// </summary>
        /// <param name="status"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// ※将来の一括変更に備えて設置
        /// <remarks>
        /// </remarks>
        public static string GetResultMessage(string status, Exception ex = null)
        {
            string message = "";
            if (ex != null)
            {
                message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "(" + ex.InnerException.ToString() + ")";
                }
            }
            return message;
        }

        /// <summary>
        /// 例外メッセージ取得
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// <remarks>
        /// Global.asaxでも使用するためロジック部分はCoreに作成
        /// </remarks>
        public static string GetExceptionMessage(Exception ex)
        {
            return ExceptionInfo.GetExceptionMessage(ex);
        }
        #endregion

        #region "モデルコピー関連"
        /// <summary>
        /// モデル間データコピー
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toModel">コピー先[To]</param>
        /// <param name="fromModel">コピー元[From]</param>
        /// <remarks>
        /// </remarks>
        public static void CopyModel<T>(ref T toModel, T fromModel) where T : class, new()
        {
            if (toModel == null)
                toModel = new T();
            SetProperties(fromModel, toModel);
        }

        /// <summary>
        /// モデル間データコピー(List形式)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="toModelList"></param>
        /// <param name="fromModelList"></param>
        /// <remarks></remarks>
        public static void CopyModel<T1>(ref List<T1> toModelList, IEnumerable<T1> fromModelList) where T1 : class, new()
        {
            if (fromModelList == null)
            {
                toModelList = new List<T1>();
                return;
            }

            foreach (T1 fromModel in fromModelList)
            {
                T1 toModel = new T1();
                SetProperties(fromModel, toModel);
                toModelList.Add(toModel);
            }
        }

        /// <summary>
        /// プロパティー値Copy
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="fromRecord"></param>
        /// <param name="toRecord"></param>
        /// <remarks>
        /// ２つの型の間で、同名のプロパティーの値をコピーする
        /// </remarks>
        public static void SetProperties<T1, T2>(T1 fromRecord, T2 toRecord)
        {
            foreach (PropertyInfo propFrom in fromRecord.GetType().GetProperties())
            {
                foreach (PropertyInfo propTo in toRecord.GetType().GetProperties())
                {
                    if (propFrom.Name != propTo.Name)
                        continue;
                    object fromValue = propFrom.GetValue(fromRecord, null);
                    if (fromValue != null)
                    {
                        propTo.SetValue(toRecord, fromValue, null);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// モデル間データコピー
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="toModel">コピー先[To]</param>
        /// <param name="fromModel">コピー元[From]</param>
        /// <remarks>
        /// </remarks>
        public static void CopyModel2<T1, T2>(ref T1 toModel, T2 fromModel) where T1 : class, new() where T2 : class
        {
            if (toModel == null)
                toModel = new T1();
            SetProperties(fromModel, toModel);
        }

        /// <summary>
        /// モデル間データコピー(List形式)
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="toModelList"></param>
        /// <param name="fromModelList"></param>
        /// <remarks></remarks>
        public static void CopyModel2<T1, T2>(ref List<T1> toModelList, IEnumerable<T2> fromModelList) where T1 : class, new() where T2 : class
        {
            if (fromModelList == null)
            {
                toModelList = new List<T1>();
                return;
            }

            foreach (T2 fromModel in fromModelList)
            {
                T1 toModel = new T1();
                SetProperties(fromModel, toModel);
                toModelList.Add(toModel);
            }
        }
        #endregion

        #region "日付編集"
        /// <summary>
        /// 日付編集
        /// </summary>
        /// <param name="dateValue"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        /// <remarks>
        /// Date値がない場合は""を返す
        /// </remarks>
        public static string FormatDate(Nullable<DateTime> dateValue, string formatStr)
        {
            if (dateValue.HasValue)
            {
                return string.Format(dateValue.Value.ToString(), formatStr);
            }
            return "";
        }
        #endregion

        #region "乱数生成"
        /// <summary>
        /// ランダムな文字列の生成
        /// </summary>
        /// <param name="intKeyLen">文字数</param>
        /// <returns>ランダムな文字列（0～9、A～Z、a～zの組み合わせ）</returns>
        public static string CreateRandomString(int intKeyLen)
        {

            // 指定の文字数になるまでランダムな文字を生成
            string strKey = string.Empty;
            while (!(Strings.Len(strKey) >= intKeyLen))
            {
                // ランダムな文字を生成
                string strKeyChar = Strings.Chr(RollDice(122 - 47) + 47).ToString();
                // 数字・英字の範囲かチェック
                switch (strKeyChar)
                {
                    case "0": // TODO: to "9"
                    case "A": // TODO: to "Z"
                    case "a": // TODO: to "z"
                        strKey = strKey + strKeyChar;
                        break;
                }
            }

            return strKey;

        }

        /// <summary>
        /// 暗号サービス プロバイダの暗号乱数ジェネレータを使っての乱数の生成（MSのヘルプから引用）
        /// </summary>
        /// <param name="NumSides">出力値の最大値</param>
        /// <returns>乱数（1～指定した最大値）</returns>
        private static int RollDice(int NumSides)
        {
            // 乱数取得用のバイト配列
            byte[] randomNumber = new byte[1];

            // RNGCryptoServiceProviderを利用して乱数の生成
            System.Security.Cryptography.RNGCryptoServiceProvider Gen = new System.Security.Cryptography.RNGCryptoServiceProvider();

            // 取得した値をバイト配列にセット
            Gen.GetBytes(randomNumber);

            // バイト配列から数値に変換
            int rand = Convert.ToInt32(randomNumber[0]);

            // 最大値ないの値を返す
            return rand % NumSides + 1;
        }
        #endregion

        #region "端数の切捨て"
        /// <summary>
        /// 端数の切捨て
        /// </summary>
        /// <param name="val">対象となる値</param>
        /// <param name="decimalPoint">小数点数</param>
        /// <returns>端数切り捨てた値</returns>
        public static decimal TruncateDecimal(decimal val, decimal decimalPoint = 0)
        {
            decimal decimalVal = 1;
            if (decimalPoint > 0)
            {
                decimalVal = Convert.ToDecimal(Math.Pow(10, Convert.ToDouble(decimalPoint)));
            }

            return Math.Truncate(val * decimalVal) / decimalVal;
        }

        #endregion

        #region "ページのタイトルに【環境名】を表示する"
        /// <summary>
        /// ページのタイトルに【環境名】を表示する
        /// </summary>
        /// <param name="pageTitle">ページのタイトル</param>
        public static string overwritePageTitle(string pageTitle)
        {
            string title = pageTitle;
            // 編集後のページのタイトル
            // ページのタイトルに【環境名】を表示する
            if (!string.IsNullOrEmpty(pageTitle) && pageTitle.Length > 0)
            {
                if (pageTitle.Length < System.Configuration.ConfigurationManager.AppSettings["MailHeader"].ToString().Length || pageTitle.Substring(0, System.Configuration.ConfigurationManager.AppSettings["MailHeader"].ToString().Length) != System.Configuration.ConfigurationManager.AppSettings["MailHeader"].ToString())
                {
                    title = System.Configuration.ConfigurationManager.AppSettings["MailHeader"].ToString() + pageTitle;
                }
            }

            return title;
        }

        #endregion
    }
}
