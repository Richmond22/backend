//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Take_A_Lot_webAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tblcredit
    {
        public int ID { get; set; }
        public Nullable<int> paymentID { get; set; }
        public string CardDescription { get; set; }
        public string NameonCard { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Type { get; set; }
        public string Cvv { get; set; }
    
        public virtual payment payment { get; set; }
    }
}
