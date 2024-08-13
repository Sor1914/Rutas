using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsRegistro
    {
        ClsConexionSql clsConexionSql;
        public bool RegistrarUsuario(Registro registro, string usuarioCreo)
        {            
            if (!ValidarUsuario(registro.Usuario))            
                throw new Exception("El usuario ya existe");            
            if (!ValidarEmail(registro.Email))            
                throw new Exception("El Email ya existe para otro usuario");            
            var registroRespuesta = new Registro();
            var consulta = string.Format(
                SqlRegistro.InsertaUsuario,
                registro.Usuario,
                registro.Contrasenia,
                registro.Email,
                registro.Nombres,
                registro.Apellidos,
                usuarioCreo);
            clsConexionSql = new ClsConexionSql();            
            return clsConexionSql.CrearDML(consulta);
        }

        public bool ValidarEmail(string email)
        {
            var consulta = string.Format(
                SqlRegistro.ValidaEmailRegistrado,
                email
                );
            clsConexionSql = new ClsConexionSql();
            var dtRespuesta = clsConexionSql.CrearConsulta(consulta);
            return dtRespuesta.Rows.Count == 0;
        }

        public bool ValidarUsuario(string usuario)
        {
            var consulta = string.Format(
                SqlRegistro.ValidaUsuarioRegistrado,
                usuario
                );
            clsConexionSql = new ClsConexionSql();
            var dtRespuesta = clsConexionSql.CrearConsulta(consulta);
            return dtRespuesta.Rows.Count == 0;
        }
    }
}