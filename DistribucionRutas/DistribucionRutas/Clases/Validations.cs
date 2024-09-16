using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class Validations
    {
    }

    //public class UsuarioExisteAttribute : ValidationAttribute
    //{
    //    ClsUsuarios clsUsuarios;
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        try
    //        {
    //            var usuario = value as string;
    //            if (!string.IsNullOrEmpty(usuario))
    //            {
    //                clsUsuarios = new ClsUsuarios();
    //                string nombreCliente = clsUsuarios.ObtenerNombreUsuario(usuario);

    //                if (!string.IsNullOrEmpty(nombreCliente))
    //                {
    //                    return ValidationResult.Success;
    //                }
    //            }
    //            return new ValidationResult("El usuario no existe.");
    //        } catch (Exception ex)
    //        {
    //            return new ValidationResult("Hubo un error inesperado.");
    //        }
            
    //    }
    //}
}