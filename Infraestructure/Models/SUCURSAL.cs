//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SUCURSAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUCURSAL()
        {
            this.HistDetalleEntradaSalida = new HashSet<HistDetalleEntradaSalida>();
            this.HistDetalleEntradaSalida1 = new HashSet<HistDetalleEntradaSalida>();
            this.ProdSuc = new HashSet<ProdSuc>();
        }
    
        public int ID { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public Nullable<int> estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistDetalleEntradaSalida> HistDetalleEntradaSalida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistDetalleEntradaSalida> HistDetalleEntradaSalida1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProdSuc> ProdSuc { get; set; }
    }
}
