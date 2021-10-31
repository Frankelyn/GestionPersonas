using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.Entidades
{
    public class AportesDetalle
    {
        [Key]
        public int AporteDetalleId { get; set; }
        public int AporteId { get; set; }
        
        public int TipoDeAporteId { get; set; }

        public float Aporte { get; set; }

        [ForeignKey("TipoDeAporteId")]
        public virtual TiposDeAportes TipoDeAporte { get; set; }
    }
}
