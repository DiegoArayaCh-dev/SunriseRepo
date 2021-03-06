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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(UsuarioMetaData))]
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.HistDetalleCRUD = new HashSet<HistDetalleCRUD>();
            this.HISTORICO = new HashSet<HISTORICO>();
        }
    
        public int ID { get; set; }
        public string cedula { get; set; }
        public Nullable<int> estado { get; set; }
        public string correo { get; set; }
        public string contrasenha { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public Nullable<int> IDRol { get; set; }
        public string telefono { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistDetalleCRUD> HistDetalleCRUD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORICO> HISTORICO { get; set; }
        public virtual ROL ROL { get; set; }
    }
}
