using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class TipoVehiculo
    {
        public int Numero { get; set; }
        [DisplayName("Id Vehículo")]
        public int IdTipoVehiculo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Capacidad Peso")]
        public decimal CapacidadPeso { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Km x Galon")]
        public decimal KMXGalon { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Galones { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Tipo Combustible")]
        public string TipoGas { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Descripcion { get; set; }
        public int Estado { get; set; }

    }
}