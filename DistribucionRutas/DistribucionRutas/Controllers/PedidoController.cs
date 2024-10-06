using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{

    public class PedidoController : Controller
    {
        ClsProductos clsConsultasProductos;
        ClsPedido clsConsultasPedidos;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";
        // GET: Pedido
        public PedidoController()
        {
            ViewBag.Layout = LAYOUTMENU;
        }

        public ActionResult Pedido(bool limpiar = true)
        {
            if (limpiar)
            {
                HttpContext.Cache.Remove("listaProductos");
            }
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            Paginacion();
            return PartialView("Pedido");
        }

        public ActionResult Paginacion(int inicioRegistros = 0, int tamanoPagina = 5, string busqueda = "")
        {
            List<Productos> ListaRegistros = new List<Productos>();
            List<Productos> ListaConductoresFiltro = new List<Productos>();
            clsConsultasProductos = new ClsProductos();
            ListaRegistros = clsConsultasProductos.ObtenerRegistros(inicioRegistros, tamanoPagina, busqueda);
            HttpContext.Cache.Insert("listaProductos", ListaRegistros, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            int cantidadRegistros = clsConsultasProductos.ContarRegistros(busqueda);
            ViewBag.PaginaActualTabla = (inicioRegistros / tamanoPagina) + 1;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaRegistros = ListaRegistros;
            ViewBag.ValorBusqueda = busqueda;
            return PartialView("_Pedido");
        }

        

        public ActionResult mostrarModalcomprar(int id)
        {            
            ViewBag.id = id;
            ViewBag.MostrarModalNuevo = true;
            ViewBag.Editar = "True";
            ViewBag.TituloModal = "Editar";
            return Pedido(false);
        }

        public ActionResult insertarPedido(UnionDetallePedido pedido) 
        {
            try 
            {                
                clsConsultasPedidos = new ClsPedido();
                bool correcto = clsConsultasPedidos.InsertarPedido(pedido, Session["usuario"].ToString());
                if (correcto)
                {
                    Util.MostrarMensaje(ViewBag, "El Registro se almacenó correctamente", 1);
                }
                else 
                {
                    Util.MostrarMensaje(ViewBag, "Hubo un error al almacenar el registro", 3);
                }
                return Pedido(false);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al almacenar el registro", 3);
                return Pedido(false);
            }

        }
       
    }
}