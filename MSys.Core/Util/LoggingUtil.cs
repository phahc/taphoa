using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MSys.Core.Util
{
    /// <summary>
    /// ログ出力クラス
    /// </summary>
    public class LoggingUtil
    {
        /// <summary>
        /// 通常レベルログ出力
        /// </summary>
        /// <param name="message">出力メッセージ</param>
        /// <remarks></remarks>
        public static void WriteLog(string message)
        {
            Logger.Write(message, "General", 0, 0, TraceEventType.Information);
        }

        /// <summary>
        /// 画面アクセスログ出力
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="screenName">画面ID</param>
        /// <param name="ipAddress">IPアドレス(Option)</param>
        public static void WriteAccessLog(string userName, string screenName, string ipAddress = null)
        {
            System.Text.StringBuilder msg = new System.Text.StringBuilder();

            msg.Append("【User Name】 : " + userName + " ");
            msg.Append("【Screen Name】 : " + screenName + " ");

            if (!string.IsNullOrEmpty(ipAddress))
            {
                msg.Append("【IP Address】 : " + ipAddress);
            }

            Logger.Write(msg.ToString(), "General", 0, 0, TraceEventType.Information);
        }


        /// <summary>
        /// デバッグレベルログ出力
        /// </summary>
        /// <param name="message">出力メッセージ</param>
        /// <remarks></remarks>
        public static void WriteDebugLog(string message)
        {
            Logger.Write(message, "Debug", 0, 0, TraceEventType.Verbose);
        }

        /// <summary>
        /// エラーレベルログ出力(SQL)
        /// </summary>
        /// <param name="command">出力メッセージ</param>
        /// <remarks></remarks>
        public static void WriteExceptionLog(string message, System.Data.Common.DbCommand command)
        {
            List<string> @params = new List<string>();
            string sql = string.Empty;

            if (command.Parameters != null)
            {
                sql = command.CommandText;
                foreach (System.Data.Common.DbParameter param in command.Parameters)
                {
                    if (param.Value == null | object.ReferenceEquals(param.Value, DBNull.Value))
                    {
                        @params.Add(param.ParameterName + "-" + "null" + "-[" + param.DbType.ToString() + "]");
                    }
                    else
                    {
                        @params.Add(param.ParameterName + "-" + param.Value.ToString() + "-[" + param.DbType.ToString() + "]");
                    }
                }
            }

            Logger.Write(string.Format("{0}" + Constants.vbCrLf + "SQL:{1}" + Constants.vbCrLf + "Param:{2}", message, sql, string.Join(",", @params.ToArray())), "Exception", 0, 0, TraceEventType.Verbose);
        }

#if !DEBUG
	    /// <summary>
	    /// エラーレベルログ出力(IT,本番,トレーニング,テスト)
	    /// </summary>
	    /// <param name="message">出力メッセージ</param>
	    /// <remarks></remarks>
	    public static void WriteExceptionLog(string message)
        {
            Logger.Write(message, "Exception", 0, 0, TraceEventType.Information);
            WriteErrorEventLog(message);
        }

        /// <summary>
        /// イベントログにエラーのエントリを書き込む
        /// </summary>
        /// <param name="message"></param>
        /// <remarks></remarks>
        private static void WriteErrorEventLog(string message)
        {
            int MAX_LENGTH = 31800;
            string source = "W2PSCFront";
            if (MAX_LENGTH < message.Length)
            {
                message = message.Substring(0, MAX_LENGTH);
            }
            EventLog.WriteEntry(source, message, EventLogEntryType.Error, 10);
        }
#else
        /// <summary>
        /// エラーレベルログ出力（Local,UT）
        /// </summary>
        /// <param name="message">出力メッセージ</param>
        /// <remarks></remarks>
        public static void WriteExceptionLog(string message)
        {
            Logger.Write(message, "Exception", 0, 0, TraceEventType.Information);
        }
#endif

    }
}
