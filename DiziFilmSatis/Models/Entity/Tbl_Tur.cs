//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiziFilmSatis.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Tur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Tur()
        {
            this.Tbl_Filmdizi = new HashSet<Tbl_Filmdizi>();
        }
    
        public int Id { get; set; }
        public string Turad { get; set; }
        public Nullable<bool> Durum { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Filmdizi> Tbl_Filmdizi { get; set; }
    }
}