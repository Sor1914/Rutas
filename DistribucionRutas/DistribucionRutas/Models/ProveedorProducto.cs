using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace DistribucionRutas.Models
{
    public class ProveedorProducto
    {
        public int Numero { get; set; }
        [DisplayName("Id Proveedor")]
        [Required]
        public int IdProveedor { get; set; }
        [DisplayName("Id Producto")]
        public int IdProducto { get; set; }
        [Required]
        [DisplayName("Precio")]
        public decimal PrecioProducto { get; set; }
        [Required]
        [DisplayName("Existencia")]
        public int Existencia { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreProducto { get; set; }
        public string UsuarioCreo { get; set; }
        public DateTime FechaCreo { get; set; }
        public int Estado { get; set; }


    }
}