using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsProveedores
    {
        ClsConexionSql conexionSql;

        public List<Proveedores> ObtenerRegistros(int inicio, int cantidad, string busqueda)
        {
            var consulta = string.Format(SqlProveedores.ObtieneDatos, busqueda, inicio, cantidad);;
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<Proveedores> lista = Util.ConvertirDataTableALista<Proveedores>(dtConductores);
            return lista;
        }

        public int ContarRegistros(string busqueda)
        {
            conexionSql = new ClsConexionSql();
            var dtCantidadRegistros = conexionSql.CrearConsulta(string.Format(SqlProveedores.CuentaRegistro, busqueda));
            var cantidadRegistros = Convert.ToInt32(dtCantidadRegistros.Rows[0][0].ToString());
            conexionSql = new ClsConexionSql();
            return cantidadRegistros;
        }

        public bool InsertarRegistro(Proveedores dato, string usuarioCreo)
        {
            string consulta = string.Format(SqlProveedores.InsertaRegistro,
                                                            dato.NombreProveedor,
                                                            dato.DireccionProveedor,
                                                            dato.Longitud,
                                                            dato.Latitud,
                                                            usuarioCreo
                                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarRegistro(Proveedores dato, string usuarioModifico, string id)
        {
            string consulta = string.Format(SqlProveedores.ActualizaRegistro,
                                            dato.NombreProveedor,
                                            dato.DireccionProveedor,
                                            dato.Longitud,
                                            dato.Latitud,
                                            usuarioModifico,
                                            id
                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarEstado(int estado, string idRegistro, string usuarioModifica)
        {
            string consulta = string.Format(SqlProveedores.CambiaEstadoRegistro,
                                                            estado,
                                                            usuarioModifica,
                                                            idRegistro);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }
    }
}