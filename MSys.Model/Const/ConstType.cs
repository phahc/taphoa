namespace MSys.Model.ConstData
{
    #region "SessionName"

    /// <summary>
    /// Thong tin session
    /// </summary>
    /// <remarks></remarks>
    public class SESSION_NAME
    {
        // TimeOut
        public const string TIMEOUT = "SessionTimeout";
        // So sanh cookie
        public const string COMPARE_COOKIE = "CompareId";
    }

    /// <summary>
    /// CookieID
    /// </summary>
    /// <remarks></remarks>
    public class COOKIE_ID
    {
        // Seesion id
        public const string SESSION = "ASP.NET_SessionId";
    }

    #endregion

    public class CONST_VALUE
    {
        /// <summary>
        /// ON
        /// </summary>
        /// <remarks></remarks>
        public const string FLG_ON = "1";
        /// <summary>
        /// OFF
        /// </summary>
        /// <remarks></remarks>
        public const string FLG_OFF = "0";
    }

    public class Regex
    {
        public const string NUMBER_REGEX = "^[0-9]+$";
        public const string MAIL_REGEX = @"([a-zA-Z0-9])+([a-zA-Z0-9\._-])*@([a-zA-Z0-9_-])+([a-zA-Z0-9\._-]+)+";
    }
}