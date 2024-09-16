using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class Productos
    {
        public int Numero { get; set; }
        [DisplayName("Id Producto")]
        public string IdProducto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Nombre Producto")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Tipo Producto")]
        public string TipoProducto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DisplayName("Peso Producto")]
        public decimal PesoProducto { get; set; }
        public int Estado { get; set; }
    }
}
