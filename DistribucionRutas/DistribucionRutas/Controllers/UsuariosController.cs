using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class UsuariosController : Controller
    {
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        // GET: Usuarios
        public UsuariosController() 
        {
            ViewBag.Layout = LAYOUTMENU;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}