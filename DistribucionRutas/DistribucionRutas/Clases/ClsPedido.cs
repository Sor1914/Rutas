using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsPedido
    {
        ClsConexionSql conexionSql;        

        public bool InsertarPedido(UnionDetallePedido pedido, string usuarioCreo)
        {
            bool resultado;
            string consultaEncabezado = string.Format(SqlPedido.insertaPedido,
                                                            pedido.Latitud,
                                                            pedido.Longitud,
                                                            usuarioCreo                                                            
                                                            );
            conexionSql = new ClsConexionSql();
            resultado = conexionSql.CrearDML(consultaEncabezado);
            if (resultado) 
            {
                string consultaDetalle = string.Format(SqlPedido.insertaDetallePedido,
                                                       pedido.Cantidad,
                                                       pedido.IdProducto,
                                                       usuarioCreo);      
                resultado = conexionSql.CrearDML(consultaDetalle);
            }            
            return resultado;
        }        
    }
}