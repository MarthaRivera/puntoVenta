//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapaDatos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venta()
        {
            this.Venta_Producto = new HashSet<Venta_Producto>();
        }
    
        public int Id { get; set; }
        public decimal Total { get; set; }
        public System.DateTime Fecha_Venta { get; set; }
        public Nullable<int> Id_Cliente { get; set; }
        public Nullable<int> Id_Empleado { get; set; }
        public decimal Iva { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta_Producto> Venta_Producto { get; set; }
    }
}