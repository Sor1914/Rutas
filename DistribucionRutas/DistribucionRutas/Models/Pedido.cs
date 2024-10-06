using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class DetallePedido
    {
        public int IdDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public int IdProducto { get; set; }
        public int IdPedido { get; set; }
        public string Usuario { get; set; }
        public int Estado { get; set; }
    }
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set;}
        public int Estado { get; set; }
    }

    public class UnionDetallePedido 
    {
        public int IdPedido { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public int Estado { get; set; }
        public int IdDetalle { get; set; }
        public decimal Cantidad { get; set; }
        public int IdProducto { get; set; }
        public string Usuario { get; set; }
    }

    public class PedidoDetalle
    {
        public Pedido Pedido { get; set; }
        public DetallePedido DetallePedido { get; set;}
    }
}