using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            var UsuarioDb = conexionSql.CrearConsulta(
                string.Format(SqlLogin.ObtieneUsuario, usuario.Usuario, usuario.Contrasenia));
            
            if (UsuarioDb.Rows.Count > 0)
            {
                usuarioResponse = ConvertirDataRowAObjeto<Usuarios>(UsuarioDb.Rows[0]);
                usuarioResponse.Existe = true;
            }
            return usuarioResponse;
        }

        public Permisos AsignarPermisos(int IdRol)
        {
            Permisos permisos = new Permisos()
            {
                CrearRuta = IdRol == 1
            };
            return permisos;
        }

        public static T ConvertirDataRowAObjeto<T>(DataRow row) where T : new()
        {
            T obj = new T();
            Type objType = typeof(T);

            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo prop = objType.GetProperty(column.ColumnName);
                if (prop != null && row[column] != DBNull.Value)
                {
                    prop.SetValue(obj, Convert.ChangeType(row[column], prop.PropertyType), null);
                }
            }
            return obj;
        }
    }
}
