//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MSys.Model.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public long userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public Nullable<int> sex { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string bank_name { get; set; }
        public string bank_serial { get; set; }
        public Nullable<int> enable_account { get; set; }
        public Nullable<System.DateTime> update_time { get; set; }
    }
}
