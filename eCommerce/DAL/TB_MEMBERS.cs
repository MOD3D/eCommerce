//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eCommerce.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_MEMBERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_MEMBERS()
        {
            this.TB_SHIPPING_DETAILS = new HashSet<TB_SHIPPING_DETAILS>();
        }
    
        public int MEMBER_ID { get; set; }
        public string FRIST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EMAIL_ID { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public Nullable<System.DateTime> CREATED_ON { get; set; }
        public Nullable<System.DateTime> MODIFIED_ON { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_SHIPPING_DETAILS> TB_SHIPPING_DETAILS { get; set; }
    }
}
