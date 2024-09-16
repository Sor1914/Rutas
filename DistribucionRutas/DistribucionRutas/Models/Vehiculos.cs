using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class Vehiculos
    {
        public int Numero { get; set; }
        public string Placa { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Km Recorridos")]
        public decimal KmRecorridos { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Licencia Conductor")]
        public string LicenciaConductor { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Id Vehículo")]
        public int idTipoVehiculo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Estado { get; set; }

    }
}