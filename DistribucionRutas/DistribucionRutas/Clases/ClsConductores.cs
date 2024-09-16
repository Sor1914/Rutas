using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsConductores
    {
        ClsConexionSql conexionSql;
        public List<Conductores> ObtenerConductores(int inicio, int cantidad, string busqueda)
        {
            var consulta = string.Format(SqlConductores.ObtieneConductores, busqueda, inicio, cantidad);
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<Conductores> listaConductores = Util.ConvertirDataTableALista<Conductores>(dtConductores);
            return listaConductores;
        }

        public int ContarConductores(string busqueda) 
        {

            conexionSql = new ClsConexionSql();
            var dtCantidadUsuarios = conexionSql.CrearConsulta(string.Format(SqlConductores.CuentaConductores, busqueda));
            var cantidadRegistros = Convert.ToInt32(dtCantidadUsuarios.Rows[0][0].ToString());
            conexionSql = new ClsConexionSql();
            return cantidadRegistros;
        }

        public bool InsertarConductor(Conductores conductor, string usuarioCreo) 
        {
            string consulta = string.Format(SqlConductores.InsertaConductores,
                                                            conductor.Licencia,
                                                            conductor.TipoLicencia,
                                                            conductor.idHorario,
                                                            conductor.LimiteParadas,
                                                            conductor.Usuario,
                                                            usuarioCreo
                                                            );
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);    
        }

        public bool ActualizarConductor(Conductores conductor, string usuarioModifico)
        {
            string consulta = string.Format(SqlConductores.ActualizarConductores,
                                            conductor.TipoLicencia,
                                            conductor.idHorario,
                                            conductor.LimiteParadas,
                                            usuarioModifico,
                                            conductor.Licencia
                                            );
            conexionSql=new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool ActualizarEstado(int estado, string licencia, string usuarioModifica) 
        {
            string consulta = string.Format(SqlConductores.CambiarEstadoConductores,
                                                            estado,
                                                            licencia,
                                                            usuarioModifica);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public DataTable ObtenerHorarios() 
        {
            string consulta = SqlConductores.ObtieneHorarios;
            conexionSql = new ClsConexionSql();
            var dtHorarios = conexionSql.CrearConsulta(consulta);
            return dtHorarios;
        }

       
        public DataTable ObtenerUsuariosConductores() 
        {
            var consulta = SqlConductores.ObtieneUsuariosConductores;
            conexionSql = new ClsConexionSql();
            var dtUsuarios = conexionSql.CrearConsulta(consulta);
            return dtUsuarios;
        }
    }
}