using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;

namespace DistribucionRutas.Clases
{
    public class ClsLogin
    {
        ClsConexionSql conexionSql;
        public Usuarios Autenticar(Usuarios usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            Usuarios usuarioResponse = new Usuarios();
            conexionSql = new ClsConexionSql();
            var consulta = string.Format(SqlLogin.ObtieneUsuario, usuario.Usuario, usuario.Contrasenia);
            var UsuarioDb = conexionSql.CrearConsulta(consulta);

            if (UsuarioDb.Rows.Count > 0)
            {
                usuarioResponse = Util.ConvertirDataRowAObjeto<Usuarios>(UsuarioDb.Rows[0]);
                usuarioResponse.Existe = true;
            }
            return usuarioResponse;
        }

        public Permisos AsignarPermisos(int IdRol)
        {
            Permisos permisos = new Permisos()
            {
                GestionUsuarios = IdRol == 1,
                GestionConductores = IdRol == 1,
                GestionTipoVehiculo = IdRol == 1,
                GestionVehiculos = IdRol == 1,
                GestionProveedores = IdRol == 1,
                GestionProductos = IdRol == 1
            };
            return permisos;
        }
    }
}
