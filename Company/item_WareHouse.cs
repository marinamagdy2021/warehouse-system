//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Company
{
    using System;
    using System.Collections.Generic;
    
    public partial class item_WareHouse
    {
        public string WH_Name { get; set; }
        public int I_Code { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}