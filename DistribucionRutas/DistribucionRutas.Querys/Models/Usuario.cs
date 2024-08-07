using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Querys.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int IdRol { get; set; }        
    }
}