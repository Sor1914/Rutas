using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsVehiculos
    {
        ClsConexionSql conexionSql;

        public List<Vehiculos> ObtenerRegistros(int inicio, int cantidad, string busqueda)
        {
            var consulta = string.Format(SqlVehiculos.ObtieneDatos, busqueda, inicio, cantidad);
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<Vehiculos> lista = Util.ConvertirDataTableALista<Vehiculos>(dtConductores);
            return lista;
        }

        public int ContarRegistros(string busqueda)
        {

            conexionSql = new ClsConexionSql();
            var dtCantidadRegistros = conexionSql.CrearConsulta(string.Format(SqlVehiculos.CuentaRegistro, busqueda));
            var cantidadRegistros = Convert.ToInt32(dtCantidadRegistros.Rows[0][0].ToString());
            conexionSql = new ClsConexionSql();
            return cantidadRegistros;
        }

        public bool InsertarRegistro(Vehiculos dato, string usuarioCreo)
        {
            string consulta = string.Format(SqlVehiculos.InsertaRegistro,
                                                            dato.Placa,
                                                            dato.KmRecorridos,
                                                            dato.LicenciaConductor,
                                                            dato.idTipoVehiculo,                                                    
                                                            dato.Estado,
                                                            usuarioCreo
                                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarRegistro(Vehiculos dato, string usuarioModifico, string id)
        {
            string consulta = string.Format(SqlVehiculos.ActualizaRegistro,
                                            dato.Placa,
                                            dato.LicenciaConductor,
                                            dato.idTipoVehiculo,                                            
                                            usuarioModifico,
                                            id
                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarEstado(int estado, string idRegistro, string usuarioModifica)
        {
            string consulta = string.Format(SqlVehiculos.CambiaEstadoRegistro,
                                                            estado,
                                                            usuarioModifica,
                                                            idRegistro);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public List<Conductores> ObtenerConductores() 
        {
            var consulta = string.Format(SqlVehiculos.ObtieneConductores);
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<Conductores> lista = Util.ConvertirDataTableALista<Conductores>(dtConductores);
            return lista;
        }

        public List<TipoVehiculo> ObtenerTipoVehiculos()
        {
            var consulta = string.Format(SqlVehiculos.ObtieneTiposVehiculo);
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<TipoVehiculo> lista = Util.ConvertirDataTableALista<TipoVehiculo>(dtConductores);
            return lista;
        }

    }
}