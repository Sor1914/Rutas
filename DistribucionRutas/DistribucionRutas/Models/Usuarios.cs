using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Models
{
    public class Usuarios
    {
        
        public bool Existe { get; set; }        
        [DisplayName("Nombre de usuario")]
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public string Usuario { get; set; }
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int IdRol { get; set; }        
        public string NombreRol {  get; set; }
        public int Estado { get; set; }
    }
}