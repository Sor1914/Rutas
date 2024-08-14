using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DistribucionRutas.Models
{
    public class Registro
    {
        public bool Existe { get; set; }
        [DisplayName("Nombre de usuario")]
        [Required(ErrorMessage = "El campo usuario es obligatorio")]
        public string Usuario { get; set; }
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string Contrasenia { get; set; }
        [DataType(DataType.Password)]
        [Compare("Contrasenia", ErrorMessage = "Las contraseñas no coinciden")]
        [DisplayName("Confirmar Contraseña")]
        [Required]
        public string ConfirmContrasenia { get; set; }
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Ingrese un correo electrónico válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Nombres es obligatorio")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        public string Apellidos { get; set; }
    }
}