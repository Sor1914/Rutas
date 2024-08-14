using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class LoginController : Controller
    {
        string LAYOUTLOGIN = "~/Views/Shared/_LayoutLogin.cshtml";
        ClsLogin clsLogin;
        public LoginController()
        {
            ViewBag.Layout = LAYOUTLOGIN;
        }

        public ActionResult Login()
        {
            //ClsEnvioEmail clsEnvioEmail = new ClsEnvioEmail();
            //clsEnvioEmail.enviarCorreo("jonathansor2000sm@gmail.com".Split(), "Prueba", "Prueba");
            ViewBag.Layout = LAYOUTLOGIN;
            return View();
        }

        public ActionResult LoginWhenRegisterComplete()
        {
            Util.MostrarMensaje(ViewBag, "Te has registrado correctamente, inicia sesión", 1);
            return View("Login");
        }

        public ActionResult Logout()
        {
            HttpCookieCollection cookies = Request.Cookies;
            foreach (string key in cookies.AllKeys)
            {
                HttpCookie cookie = cookies[key];
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            Session.Clear();
            Session.Abandon();
            return View("Login");
        }

        [HttpPost]
        public ActionResult IniciarSesion(string BtnLogin, string BtnRegistro, Usuarios login)
        {
            if (!string.IsNullOrEmpty(BtnLogin))
            {
                try
                {
                    clsLogin = new ClsLogin();
                    Usuarios loginResponse = new Usuarios();
                    loginResponse = clsLogin.Autenticar(login);
                    if (loginResponse.Existe)
                    {
                        Permisos permisos = clsLogin.AsignarPermisos(loginResponse.IdRol);
                        Session["usuario"] = loginResponse.Usuario;
                        Session["nombres"] = loginResponse.Nombres;
                        Session["apellidos"] = loginResponse.Apellidos;
                        Session["Permisos"] = permisos;
                        return RedirectToAction("Bienvenida", "MenuPrincipal");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Usuario y/o contraseña inválidos");
                        return View("Login");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Usuario y/o contraseña inválidos");
                    return View("Login");
                }
            }
            else if (!string.IsNullOrEmpty(BtnRegistro))
            {
                return RedirectToAction("Registro", "Registro");
            }
            else
            {
                return View("Index");
            }
        }


    }
}