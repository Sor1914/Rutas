using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class Proveedores
    {
        public int Numero { get; set; }
        [DisplayName("Id Proveedor")]
        public string IdProveedor { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Nombre")]
        public string NombreProveedor { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Direccion")]
        public string DireccionProveedor { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Longitud")]
        public string Longitud { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Latitud")]
        public string Latitud { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Estado { get; set; }
    }
}