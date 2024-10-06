using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class Ruta
    {
        public int IdPedido { get; set; }
        public string LatitudInicial { get; set; }
        public string LongitudInicial { get; set; }
        public string LatitudFinal { get; set; }
        public string Longitudfinal { get; set; }

        public string UsuarioCreo { get; set; }
        public string NombreProducto { get; set; }

    }
}