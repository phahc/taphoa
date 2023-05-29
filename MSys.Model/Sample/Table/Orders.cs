using System;

namespace SCF.Model.Table
{
    /// <summary>
    /// Orders:ƒe[ƒuƒ‹Ši”[ƒNƒ‰ƒX
    /// </summary>
    /// <remarks>
    /// ì¬ŽÒ@@F@KSC
    /// ì¬“ú@@F@2009/12/16
    /// XV—š—ð@F
    ///
    /// Copyright(C) 2012 System Consultant Co., Ltd. All Rights Reserved.
    /// </remarks>
    [Serializable()]
    public class Orders
    {
        /// <summary>
        /// OrderID:int(4)
        /// </summary>
        /// <remarks>Constraints : PRIMARY KEY NOT NULL</remarks>
        public Nullable<int> OrderID { get; set; }

        /// <summary>
        /// CustomerID:nchar(10)
        /// </summary>
        /// <remarks></remarks>
        public string CustomerID { get; set; }

        /// <summary>
        /// EmployeeID:int(4)
        /// </summary>
        /// <remarks></remarks>
        public Nullable<int> EmployeeID { get; set; }

        /// <summary>
        /// OrderDate:datetime(8)
        /// </summary>
        /// <remarks></remarks>
        public Nullable<DateTime> OrderDate { get; set; }

        /// <summary>
        /// RequiredDate:datetime(8)
        /// </summary>
        /// <remarks></remarks>
        public Nullable<DateTime> RequiredDate { get; set; }

        /// <summary>
        /// ShippedDate:datetime(8)
        /// </summary>
        /// <remarks></remarks>
        public Nullable<DateTime> ShippedDate { get; set; }

        /// <summary>
        /// ShipVia:int(4)
        /// </summary>
        /// <remarks></remarks>
        public Nullable<int> ShipVia { get; set; }

        /// <summary>
        /// Freight:money(8)
        /// </summary>
        /// <remarks></remarks>
        public Nullable<decimal> Freight { get; set; }

        /// <summary>
        /// ShipName:nvarchar(80)
        /// </summary>
        /// <remarks></remarks>
        public string ShipName { get; set; }

        /// <summary>
        /// ShipAddress:nvarchar(60)
        /// </summary>
        /// <remarks></remarks>
        public string ShipAddress { get; set; }

        /// <summary>
        /// ShipCity:nvarchar(30)
        /// </summary>
        /// <remarks></remarks>
        public string ShipCity { get; set; }

        /// <summary>
        /// ShipPostalCode:nvarchar(20)
        /// </summary>
        /// <remarks></remarks>
        public string ShipPostalCode { get; set; }

        /// <summary>
        /// ShipCountry:nvarchar(30)
        /// </summary>
        /// <remarks></remarks>
        public string ShipCountry { get; set; }

    }
}
