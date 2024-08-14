using DistribucionRutas.Models;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class MenuPrincipalController : Controller
    {
        string LayoutMenu = "~/Views/Shared/_LayoutMenu.cshtml";

        public MenuPrincipalController()
        {
            int rol = 2;
            ViewBag.Layout = LayoutMenu;
            ViewBag.Rol = rol;
        }

        // GET: MenuInicial
        public ActionResult Bienvenida()
        {
            ViewBag.Permisos = (Permisos)Session["Permisos"];
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.Usuario = Session["usuario"].ToString();
            ViewBag.Nombres = Session["nombres"].ToString();
            ViewBag.Apellidos = Session["apellidos"].ToString();
            return View();
        }

        private void validarPermisos()
        {
            //Si es admin
            if (true)
            {
                ViewBag.Permiso = true;
            }
        }
    }
}