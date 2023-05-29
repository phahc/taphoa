using System;

namespace SCF.Model
{
    /// <summary>
    /// The model class of T_LOG
    /// </summary>
    [Serializable]
    public class T_LOG
    {
        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the serverity.
        /// </summary>
        /// <value>The serverity.</value>
        public string Serverity { get; set; }

        /// <summary>
        /// Gets or sets the table nm.
        /// </summary>
        /// <value>The table nm.</value>
        public string Table_Nm { get; set; }

        /// <summary>
        /// Gets or sets the SQL ID.
        /// </summary>
        /// <value>The SQL ID.</value>
        public string Sql_ID { get; set; }

        /// <summary>
        /// Gets or sets the execute time.
        /// </summary>
        /// <value>The execute time.</value>
        public Nullable<decimal> ExecuteTime { get; set; }

        /// <summary>
        /// Gets or sets the type of the SQL.
        /// </summary>
        /// <value>The type of the SQL.</value>
        public string Sql_Type { get; set; }

        /// <summary>
        /// Gets or sets the SQL.
        /// </summary>
        /// <value>The SQL.</value>
        public string Sql { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public string Parameter { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        /// <value>The ip address.</value>
        public string Ip_Address { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        public string User_ID { get; set; }

        public override string ToString()
        {
            return string.Format("Timestamp  :{0}" + Environment.NewLine + 
                "Serverity  :{1}" + Environment.NewLine + 
                "TableName  :{2}" + Environment.NewLine + 
                "SqlID      :{3}" + Environment.NewLine + 
                "ExecuteTime:{4}" + Environment.NewLine + 
                "SQL        :{5}" + Environment.NewLine + 
                "Parameter  :{6}" + Environment.NewLine + 
                "IPAddress  :{7}" + Environment.NewLine + 
                "UserID     :{8}", 
                this.TimeStamp.ToString(), 
                this.Serverity, 
                this.Table_Nm, 
                this.Sql_ID, 
                this.ExecuteTime.ToString(), 
                this.Sql, 
                this.Parameter, 
                this.Ip_Address, 
                this.User_ID);
        }
    }
}
