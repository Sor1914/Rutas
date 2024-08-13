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
            return conexionSql.CrearDML(consulta);
        }

        public DataTable ObtenerRoles()
        {
            return conexionSql.CrearConsulta(SqlUsuarios.ObtieneRoles);
        }

        public DataTable ObtenerUsuarios(int inicio, int cantidad)
        {
            return conexionSql.CrearConsulta(SqlUsuarios.ObtieneUsuarios);
        }
    }
}