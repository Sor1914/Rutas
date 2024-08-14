using DistribucionRutas.Consultas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsUsuarios
    {
        ClsConexionSql conexionSql;

        public bool AsignarRol(int rol, string usuario, string usuarioModifico)
        {
            var consulta = string.Format(
                SqlUsuarios.AsignaRolUsuario,
                rol, usuario, usuarioModifico);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public bool CambiarEstadoUsuario(string usuario, string usuarioModifico, int estado)
        {
            var consulta = string.Format(
                    SqlUsuarios.DesactivaUsuario, 
                    usuario, usuarioModifico, estado);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public DataTable ObtenerRoles()
        {
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearConsulta(SqlUsuarios.ObtieneRoles);
        }

        public DataTable ObtenerUsuarios(int inicio, int cantidad)
        {
            conexionSql = new ClsConexionSql();
            var consulta = string.Format(SqlUsuarios.ObtieneUsuarios, inicio, cantidad);
            return conexionSql.CrearConsulta(consulta);
        }

        public int CuentaUsuarios()
        {
            var dtCantidadUsuarios = conexionSql.CrearConsulta(SqlUsuarios.CuentaUsuarios);
            var cantidadRegistros = Convert.ToInt32(dtCantidadUsuarios.Rows[0][0].ToString());
            conexionSql = new ClsConexionSql();
            return cantidadRegistros;
        }
    }
}