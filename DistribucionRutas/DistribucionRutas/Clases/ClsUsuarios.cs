using DistribucionRutas.Consultas;
using System;
using System.Data;
using System.Text;

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
                    SqlUsuarios.GestionarUsuario,
                    usuario, usuarioModifico, estado);
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearDML(consulta);
        }

        public DataTable ObtenerRoles()
        {
            conexionSql = new ClsConexionSql();
            return conexionSql.CrearConsulta(SqlUsuarios.ObtieneRoles);
        }

        public DataTable ObtenerUsuarios(int inicio, int cantidad, string busqueda)
        {
            conexionSql = new ClsConexionSql();
            var consulta = string.Format(SqlUsuarios.ObtieneUsuarios, inicio, cantidad, busqueda);
            return conexionSql.CrearConsulta(consulta);
        }

        public int CuentaUsuarios(string busqueda)
        {
            conexionSql = new ClsConexionSql();
            var dtCantidadUsuarios = conexionSql.CrearConsulta(string.Format(SqlUsuarios.CuentaUsuarios, busqueda));
            var cantidadRegistros = Convert.ToInt32(dtCantidadUsuarios.Rows[0][0].ToString());
            conexionSql = new ClsConexionSql();
            return cantidadRegistros;
        }
    }
}