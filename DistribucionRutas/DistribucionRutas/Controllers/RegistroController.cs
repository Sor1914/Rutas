using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class RegistroController : Controller
    {
        string LAYOUTLOGIN = "~/Views/Shared/_LayoutLogin.cshtml";
        ClsRegistro clsRegistro;
        ClsEnvioEmail clsEnvioEmail;
        public RegistroController()
        {
            ViewBag.Layout = LAYOUTLOGIN;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(string btnRegistro, string btnRegresar, Registro Registro)
        {
            ViewBag.Layout = LAYOUTLOGIN;
            if (!string.IsNullOrEmpty(btnRegistro))
            {
                try
                {
                    clsRegistro = new ClsRegistro();
                    var registrado = clsRegistro.RegistrarUsuario(Registro, "sys");
                    if (registrado)
                    {
                        //clsEnvioEmail = new ClsEnvioEmail();
                        //clsEnvioEmail.enviarCorreo(Registro.Email.Split(), "Bienvenido", crearHtmlRegistro(Registro.Nombres, Registro.Usuario));
                        return RedirectToAction("LoginWhenRegisterComplete", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrio un error, intente de nuevo");
                        return View("Registro");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("Registro");
                }
            }
            else if (!string.IsNullOrEmpty(btnRegresar))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View("Registro");
            }
        }

        public static ValidationResult IsValidPass(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return new ValidationResult("Las contraseñas no son identicas");
            }
            return ValidationResult.Success;
        }

        private string crearHtmlRegistro(string nombres, string usuario)
        {
            string html = "<center> <div style='background: #eeeeee; width:520px; border-radius: 0; padding: 20px;'> <table align='center'> <tbody> <tr> <td colspan='2'> <h3 style='text-align: center;'>Registro Correcto</h3> <p style='text-align: left;'>Hola {0}, Te damos la bienvenida al sistema de quejas del banco Mi Pretamito</p> <p style='text-align: left;'>Esperamos poder apoyarte en todo lo que necesites y te sientas cómodo de expresar tus incomodidades.</p>" +
                "</td> <</tr> <tr> <td colspan='0'> Usuario </td> <td colspan='1'> {1} </td> </tr> <tr> <td colspan='2'> <center> <a href='https://google.com' style='display:inline-block; text-decoration: none; color: black;'> <div style='width: 150px; height: 50px; background-color: rgb(0, 0, 0); border-radius: 10px;'> <h3 style=' text-align: center;  line-height: 50px; color:white;'> Visitar </h3> </div> </a> </center> </br> </td> </tr> </tbody> </table> </center>";
            string htmlFinal = string.Format(html, nombres, usuario);
            return htmlFinal;
        }
    }
}