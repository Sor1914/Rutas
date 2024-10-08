﻿using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DistribucionRutas.Clases;

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
                GestionUsuarios = IdRol == 1                
            };
            return permisos;
        }        
    }
}
