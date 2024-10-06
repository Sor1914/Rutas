using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class RutaController : Controller
    {
        // GET: Ruta
        ClsConductores clsConductores;
        ClsRuta clsConsultas;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        public RutaController()
        {
            ViewBag.Layout = LAYOUTMENU;
        }

        public ActionResult Ruta(bool limpiar = true)
        {
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            LlenarRutas(0);
            return View("Ruta");
        }
        private void LlenarRutas(int id, List<Ruta> lista = null)
        {            
            if (lista == null) 
            {
                clsConsultas = new ClsRuta();
                lista = clsConsultas.ObtenerRegistros();
            }            
            var listaRuta = new List<SelectListItem>();
            foreach (var dato in lista)
            {
                if (id == dato.IdPedido)
                    listaRuta.Add(new SelectListItem { Value = dato.IdPedido.ToString(), Text = $"{dato.IdPedido} - {dato.UsuarioCreo}", Selected = true });
                else
                    listaRuta.Add(new SelectListItem { Value = dato.IdPedido.ToString(), Text = $"{dato.IdPedido} - {dato.UsuarioCreo}" });
            }          
            ViewBag.listaRutas = listaRuta;
        }

        public ActionResult ObtenerRutas(int id)
        {
            ClsRuta clsConsultas = new ClsRuta();
            var lista = clsConsultas.ObtenerRegistros();
            var rutaActiva = lista.Where(p => p.IdPedido == id).FirstOrDefault(); // Usa FirstOrDefault para manejar posibles nulls

            if (rutaActiva != null)
            {
                ViewBag.LatitudInicial = rutaActiva.LatitudInicial;
                ViewBag.LongitudInicial = rutaActiva.LongitudInicial;
                ViewBag.LatitudFinal = rutaActiva.LatitudFinal;
                ViewBag.LongitudFinal = rutaActiva.Longitudfinal;
                return Json(new
                {
                    latitudInicial = rutaActiva.LatitudInicial,
                    longitudInicial = rutaActiva.LongitudInicial,
                    latitudFinal = rutaActiva.LatitudFinal,
                    longitudFinal = rutaActiva.Longitudfinal
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.LatitudInicial = "null";
                ViewBag.LongitudInicial = "null";
                ViewBag.LatitudFinal = "null";
                ViewBag.LongitudFinal = "null";
            }
            return Json(null, JsonRequestBehavior.AllowGet);


        }
    }
}