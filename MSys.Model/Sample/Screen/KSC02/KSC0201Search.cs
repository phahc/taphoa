using System;

namespace SCF.Model.Screen.KSC02
{
    /// <summary>
    /// KSC0201Search.vb
    /// </summary>
    /// <remarks>
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    [Serializable()]
    public class KSC0201Search
    {
        public KSC0201Search(int utcOffset,
                    string customerName,
                    string employeeName,
                    Nullable<System.DateTime> orderDateFm,
                    Nullable<System.DateTime> orderDateTo,
                    Nullable<decimal> totAmtFm,
                    Nullable<decimal> totAmtTo)
        {
            this._utcOffset = utcOffset;
            this._customerName = customerName;
            this._employeeName = employeeName;
            this._orderDateFm = orderDateFm;
            this._orderDateTo = orderDateTo;
            this._totAmtFm = totAmtFm;
            this._totAmtTo = totAmtTo;
        }

        private int _utcOffset;
        public int UtcOffset
        {
            get { return _utcOffset; }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
        }

        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
        }

        private Nullable<System.DateTime> _orderDateFm;
        public Nullable<System.DateTime> OrderDateFm
        {
            get { return _orderDateFm; }
        }

        private Nullable<System.DateTime> _orderDateTo;
        public Nullable<System.DateTime> OrderDateTo
        {
            get { return _orderDateTo; }
        }

        private Nullable<decimal> _totAmtFm;
        public Nullable<decimal> TotAmtFm
        {
            get { return _totAmtFm; }
        }

        private Nullable<decimal> _totAmtTo;
        public Nullable<decimal> TotAmtTo
        {
            get { return _totAmtTo; }
        }

    }
}
