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
    
    public partial class Permetion_Import
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Permetion_Import()
        {
            this.Import_Quantity = new HashSet<Import_Quantity>();
        }
    
        public int Per_Num_I { get; set; }
        public int I_Code { get; set; }
        public string Supp_Email { get; set; }
        public string WH_Name { get; set; }
        public Nullable<System.DateTime> Per_Date_I { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Import_Quantity> Import_Quantity { get; set; }
        public virtual Item Item { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}