using System;

namespace SCF.Model.Table
{
    /// <summary>
    /// OrderDetails:ƒe[ƒuƒ‹Ši”[ƒNƒ‰ƒX
    /// </summary>
    /// <remarks>
    /// ì¬ŽÒ@@F@KSC
    /// ì¬“ú@@F@2011/10/18
    /// XV—š—ð@F
    ///
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    [Serializable()]
    public class OrderDetails
    {
        /// <summary>
        /// OrderID:int(4)
        /// </summary>
        /// <remarks>Constraints : PRIMARY KEY NOT NULL</remarks>
        private decimal _orderid;
        public decimal OrderID
        {
            get { return this._orderid; }
            set { this._orderid = value; }
        }

        /// <summary>
        /// ProductID:int(4)
        /// </summary>
        /// <remarks>Constraints : PRIMARY KEY NOT NULL</remarks>
        private decimal _productid;
        public decimal ProductID
        {
            get { return this._productid; }
            set { this._productid = value; }
        }

        /// <summary>
        /// UnitPrice:money(8)
        /// </summary>
        /// <remarks>Constraints :  NOT NULL</remarks>
        private decimal _unitprice;
        public decimal UnitPrice
        {
            get { return this._unitprice; }
            set { this._unitprice = value; }
        }

        /// <summary>
        /// Quantity:smallint(2)
        /// </summary>
        /// <remarks>Constraints :  NOT NULL</remarks>
        private decimal _quantity;
        public decimal Quantity
        {
            get { return this._quantity; }
            set { this._quantity = value; }
        }

        /// <summary>
        /// Discount:real(4)
        /// </summary>
        /// <remarks>Constraints :  NOT NULL</remarks>
        private decimal _discount;
        public decimal Discount
        {
            get { return this._discount; }
            set { this._discount = value; }
        }
    }
}
