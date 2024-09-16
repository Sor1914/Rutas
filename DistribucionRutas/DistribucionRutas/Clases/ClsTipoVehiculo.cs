using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsTipoVehiculo
    {
        ClsConexionSql conexionSql;

        public List<TipoVehiculo> ObtenerRegistros(int inicio, int cantidad, string busqueda)
        {
            var consulta = string.Format(SqlTipoVehiculo.ObtieneDatos, busqueda, inicio, cantidad);
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<TipoVehiculo> lista = Util.ConvertirDataTableALista<TipoVehiculo>(dtConductores);
            return lista;
        }

        public int ContarRegistros(string busqueda)
        {

            conexionSql = new ClsConexionSql();
            var dtCantidadRegistros = conexionSql.CrearConsulta(string.Format(SqlTipoVehiculo.CuentaRegistro, busqueda));
            var cantidadRegistros = Convert.ToInt32(dtCantidadRegistros.Rows[0][0].ToString());
            conexionSql = new ClsConexionSql();
            return cantidadRegistros;
        }

        public bool InsertarRegistro(TipoVehiculo dato, string usuarioCreo)
        {
            string consulta = string.Format(SqlTipoVehiculo.InsertaRegistro,
                                                            dato.CapacidadPeso,
                                                            dato.KMXGalon,
                                                            dato.Galones,
                                                            dato.TipoGas,
                                                            dato.Descripcion,
                                                            dato.Estado,
                                                            usuarioCreo
                                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarRegistro(TipoVehiculo dato, string usuarioModifico, string id)
        {
            string consulta = string.Format(SqlTipoVehiculo.ActualizaRegistro,
                                            dato.CapacidadPeso,
                                            dato.KMXGalon,
                                            dato.Galones,
                                            dato.Descripcion,
                                            usuarioModifico,
                                            id,
                                            dato.TipoGas
                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarEstado(int estado, string idRegistro, string usuarioModifica)
        {
            string consulta = string.Format(SqlTipoVehiculo.CambiaEstadoRegistro,
                                                            estado,                                                            
                                                            usuarioModifica,
                                                            idRegistro);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

    }
}