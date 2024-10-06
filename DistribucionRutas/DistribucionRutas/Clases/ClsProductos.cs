using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsProductos
    {
        ClsConexionSql conexionSql;
        public List<Productos> ObtenerRegistros(int inicio, int cantidad, string busqueda)
        {
            var consulta = string.Format(SqlProductos.ObtieneDatos, busqueda, inicio, cantidad); ;
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<Productos> lista = Util.ConvertirDataTableALista<Productos>(dtConductores);
            return lista;
        }

        public int ContarRegistros(string busqueda)
        {
            conexionSql = new ClsConexionSql();
            var dtCantidadRegistros = conexionSql.CrearConsulta(string.Format(SqlProductos.CuentaRegistro, busqueda));
            var cantidadRegistros = Convert.ToInt32(dtCantidadRegistros.Rows[0][0].ToString());
            conexionSql = new ClsConexionSql();
            return cantidadRegistros;
        }

        public bool InsertarRegistro(Productos dato, string usuarioCreo)
        {
            string consulta = string.Format(SqlProductos.InsertaRegistro,
                                                            dato.NombreProducto,
                                                            dato.TipoProducto,
                                                            dato.PesoProducto,
                                                            usuarioCreo
                                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarRegistro(Productos dato, string usuarioModifico, string id)
        {
            string consulta = string.Format(SqlProductos.ActualizaRegistro,
                                            dato.PesoProducto,
                                            usuarioModifico,
                                            id
                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarEstado(int estado, string idRegistro, string usuarioModifica)
        {
            string consulta = string.Format(SqlProductos.CambiaEstadoRegistro,
                                                            estado,
                                                            usuarioModifica,
                                                            idRegistro);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool InsertarProveedorProducto(ProveedorProducto dato, string usuarioCreo) 
        {
            string consulta = string.Format(SqlProductos.InsertaProveedorProducto,
                                            dato.IdProveedor,
                                            dato.IdProducto,
                                            dato.PrecioProducto,
                                            dato.Existencia,
                                            usuarioCreo
                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public List<Proveedores> obtenerProveedores()
        {
            var consulta = string.Format(SqlProductos.ObtieneProveedores); ;
            conexionSql = new ClsConexionSql();
            var dtProveedores = conexionSql.CrearConsulta(consulta);
            List<Proveedores> lista = Util.ConvertirDataTableALista<Proveedores>(dtProveedores);
            return lista;
        }

        public List<ProveedorProducto> ObtenerProveedorProducto(int idProducto)
        {
            var consulta = string.Format(SqlProductos.ObtieneProveedorProducto, idProducto);
            conexionSql = new ClsConexionSql();
            var dtProveedorProducto = conexionSql.CrearConsulta(consulta);
            List<ProveedorProducto> lista = Util.ConvertirDataTableALista<ProveedorProducto>(dtProveedorProducto);
            return lista;
        }

        public bool ActualizarEstadoProveedorProducto(int idProveedor, int idProducto)
        {
            string consulta = string.Format(SqlProductos.EliminaProveedorProducto,
                                                            idProveedor,
                                                            idProducto);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

    }
}