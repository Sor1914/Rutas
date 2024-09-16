using DistribucionRutas.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class Conductores
    {
        public int Numero { get; set; }

        [Required(ErrorMessage = "El campo licencia es obligatorio")]

        public string Licencia { get; set; }
        [Required(ErrorMessage = "El campo tipo licencia es obligatorio")]
        [DisplayName("Tipo Licencia")]

        public string TipoLicencia { get; set; }
        [Required(ErrorMessage = "El campo id Horario es obligatorio")]
        [DisplayName("Id Horario")]

        public int idHorario {  get; set; }
        [DisplayName("Límite Paradas")]

        public int LimiteParadas { get; set; }
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public string Usuario { get; set; }
        public int Estado { get; set; }
    }
}