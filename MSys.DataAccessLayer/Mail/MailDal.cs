using MSys.Interface;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;

namespace MSys.DataAccessLayer
{
    public class MailDal: IMail
    {
        #region "SendMail: メール送信"
        /// <summary>
        /// メール送信
        /// </summary>
        /// <param name="mailAddress">宛先</param>
        /// <param name="subject">件名</param>
        /// <param name="body">本文</param>
        /// <param name="bccMailAddress">BCC宛先(オプション)</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SendMail(string mailAddress, string subject, string body, string bccMailAddress = "")
        {
            bool sendFlg = false;
            try
            {
                //' MailMessageの作成
                using (MailMessage msg = new MailMessage())
                {
                    //' 差出人("iso-2022-jp" Encode)
                    dynamic sashidasinnknNm = System.Configuration.ConfigurationManager.AppSettings["MailfromKnnm"].ToString();
                    dynamic sashidasinnMladdr = System.Configuration.ConfigurationManager.AppSettings["MailfromMladdr"].ToString();

                    //byte[] byteAry = ASCIIEncoding.GetEncoding("iso-2022-jp").GetBytes(sashidasinnknNm);
                    //msg.From = new System.Net.Mail.MailAddress(sashidasinnMladdr, "=?iso-2022-jp?B?" + Convert.ToBase64String(byteAry) + "?=");

                    ////recipient address
                    //byteAry = ASCIIEncoding.GetEncoding("iso-2022-jp").GetBytes("Dear " + name + ",");
                    //msg.To.Add(new MailAddress(mailAddress, "=?iso-2022-jp?B?" + Convert.ToBase64String(byteAry) + "?="));

                    msg.From = new System.Net.Mail.MailAddress(sashidasinnMladdr, sashidasinnknNm, Encoding.UTF8);

                    //recipient address
                    //byteAry = ASCIIEncoding.GetEncoding("iso-2022-jp").GetBytes("Dear " + name + ",");
                    msg.To.Add(new MailAddress(mailAddress));

                    //' BCC宛先＆宛名
                    if (!string.IsNullOrEmpty(bccMailAddress))
                    {
                        msg.Bcc.Add(bccMailAddress);
                    }

                    //' 件名("iso-2022-jp" Encode)
                    byte[]  byteAry = ASCIIEncoding.GetEncoding("iso-2022-jp").GetBytes(subject);
                    msg.Subject = "=?iso-2022-jp?B?" + Convert.ToBase64String(byteAry) + "?=";

                    //' 本文
                    msg.Body = body;
                    //' 本文Encencode("iso-2022-jp")
                    //msg.BodyEncoding = Encoding.GetEncoding("iso-2022-jp");
                    msg.BodyEncoding = Encoding.UTF8;

                    //' SmtpClientの設定
                    SmtpClient sc = new SmtpClient();

                    var _with1 = sc;
                    //' メールＨＯＳＴを設定
                    sc.Host = "smtp.gmail.com";
                    //' メールPORT番号を設定
                    sc.Port = 587;
                    //' タイムアウトを設定
                    sc.Timeout = 60000;
                    //' アイドル状態を持続できる時間を設定
                    sc.ServicePoint.MaxIdleTime = 1000;
                    //' 差出人の認証に使用する資格情報を設定
                    sc.Credentials = new System.Net.NetworkCredential(msg.From.Address, "hcph@123");
                    //' SSL使用を設定
                    sc.EnableSsl = true;
                    //' メッセージを送信する
                    sc.Send(msg);
                }

                sendFlg = true;
            }
            catch (Exception ex)
            {
                //' ログ出力内容の変更
                Core.Util.LoggingUtil.WriteExceptionLog(new StackFrame(1).GetMethod().Name + " - Mail transmission failed." + Environment.NewLine + " Destination：" + mailAddress + Environment.NewLine + 
                                                        (!string.IsNullOrEmpty(bccMailAddress) ? " Destination BCC：" + bccMailAddress + Environment.NewLine : "" + " Subject：" + subject + Environment.NewLine + " Text：" + body + Environment.NewLine + Core.Util.ExceptionInfo.GetExceptionMessage(ex)));
                sendFlg = false;
            }
            finally
            {
                //' 送信したメールをテーブルに追加
                try
                {
                    
                }
                catch (Exception ex)
                {
                    //' エラーログ出力
                    Core.Util.LoggingUtil.WriteExceptionLog(new StackFrame(1).GetMethod().Name + " - Registration of outgoing mail failed.(E-mail address：" + mailAddress + ")" + Environment.NewLine + Core.Util.ExceptionInfo.GetExceptionMessage(ex));
                }
            }
            return sendFlg;
        }
        #endregion
    }
}
