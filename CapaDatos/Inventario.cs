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
    
    public partial class Inventario
    {
        public int Id_Producto { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
    
        public virtual Catalogo Catalogo { get; set; }
    }
}
