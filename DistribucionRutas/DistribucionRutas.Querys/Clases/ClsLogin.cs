using DistribucionRutas.Querys.Consultas;
using DistribucionRutas.Querys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistribucionRutas.Querys.Clases
{
    public class ClsLogin
    {
        ClsConexionSql conexionSql;
        public Usuario Autenticar(Usuario usuario)
        {
            conexionSql = new ClsConexionSql();
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            var buscarUsuario = conexionSql.CrearConsulta(
                string.Format(SqlLogin.ObtieneUsuario, usuario.NombreUsuario, usuario.Contrasenia));
            if (buscarUsuario.Rows[0]["Existe"].ToString().Equals("1"))
            {

            }
        }
    }
}
