using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;

namespace DistribucionRutas.Controllers
{
    public class UsuariosController : Controller
    {
        ClsUsuarios clsUsuarios;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        // GET: Usuarios
        public UsuariosController() 
        {
            ViewBag.Layout = LAYOUTMENU;
        }
         public async Task<ActionResult> Usuarios(bool limpiar = true)
        {
            if (limpiar)
            {
                HttpContext.Cache.Remove("listaUsuarios");
            }
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            Paginacion();
            //await llenarRoles(0);
            return PartialView("Usuarios");
        }

        public ActionResult Paginacion(int pagina = 1, int tamanoPagina = 5, string busqueda = "")
        {
            List<Usuarios> ListaUsuarios = new List<Usuarios>();
            List<Usuarios> ListaUsuariosFiltro = new List<Usuarios>();
            clsUsuarios = new ClsUsuarios();
            if (HttpContext.Cache["listaUsuarios"] != null)
                ListaUsuarios = HttpContext.Cache["listaUsuarios"] as List<Usuarios>;
            else
            {
                ListaUsuarios = Util.ConvertirDataTableALista<Usuarios>(clsUsuarios.ObtenerUsuarios(pagina, tamanoPagina));                    
                HttpContext.Cache.Insert("listaUsuarios", ListaUsuarios, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            int cantidadRegistros = clsUsuarios.CuentaUsuarios();
            ViewBag.PaginaActualTabla = pagina;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaTipos = ListaUsuarios;
            return PartialView("_Usuarios");
        }

    }
}