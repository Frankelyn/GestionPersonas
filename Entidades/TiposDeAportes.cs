using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPersonas.Entidades
{
    public class TiposDeAportes
    {
        [Key]
        public int TipoDeAporteId { get; set; }
        public string Descripcion { get; set; }
        public float Meta { get; set; }
        public float Logrado { get; set; }
    }
}
