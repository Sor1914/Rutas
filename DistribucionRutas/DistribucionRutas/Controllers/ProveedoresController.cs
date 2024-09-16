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
    public class ProveedoresController : Controller
    {
        ClsProveedores clsConsultas;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        public ProveedoresController()
        {
            ViewBag.Layout = LAYOUTMENU;
        }

        public ActionResult Proveedores(bool limpiar = true)
        {
            if (limpiar)
            {
                HttpContext.Cache.Remove("listaProveedores");
            }
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            Paginacion();
            return PartialView("Proveedores");
        }

        public ActionResult Paginacion(int inicioRegistros = 0, int tamanoPagina = 5, string busqueda = "")
        {
            List<Proveedores> ListaRegistros = new List<Proveedores>();
            List<Proveedores> ListaConductoresFiltro = new List<Proveedores>();
            clsConsultas = new ClsProveedores();
            ListaRegistros = clsConsultas.ObtenerRegistros(inicioRegistros, tamanoPagina, busqueda);
            HttpContext.Cache.Insert("listaProveedores", ListaRegistros, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            int cantidadRegistros = clsConsultas.ContarRegistros(busqueda);
            ViewBag.PaginaActualTabla = (inicioRegistros / tamanoPagina) + 1;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaRegistros = ListaRegistros;
            ViewBag.ValorBusqueda = busqueda;
            return PartialView("_Proveedores");
        }

        public ActionResult CambiarEstado(string id, string tipoGestion)
        {
            try
            {
                int estado;
                string mensaje;
                if (tipoGestion.Equals("Activar"))
                {
                    estado = 1;
                    mensaje = "El registro se activó correctamente";
                }
                else
                {
                    estado = 0;
                    mensaje = "El registro se desactivó correctamente";
                }
                clsConsultas = new ClsProveedores();
                bool seCambio = clsConsultas.ActualizarEstado(estado, id, Session["usuario"].ToString());
                Util.MostrarMensaje(ViewBag, mensaje, 1);
                return Proveedores(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al cambiar el estado", 3);
                return Proveedores(false);
            }
        }

        public ActionResult notificarEliminacion(string id, string tipo)
        {
            try
            {
                ViewBag.TipoGestion = tipo;
                ViewBag.idEliminar = id;
                ViewBag.Eliminar = "True";
                string mensaje = $"¿Está seguro de {tipo} este conductor?";
                Util.MostrarMensaje(ViewBag, mensaje, 4);
                return Proveedores(false);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al eliminar la información", 3);
                return Proveedores(false);

            }
        }

        public ActionResult mostrarModalEditar(string direccionProveedor,
                                               string longitud,
                                               string latitud,
                                               string nombreProveedor,
                                               int idProveedor)
        {
            ViewBag.direccionProveedor = direccionProveedor;
            ViewBag.longitud = longitud;
            ViewBag.latitud = latitud;
            ViewBag.id = idProveedor;
            ViewBag.nombreProveedor = nombreProveedor;
            ViewBag.MostrarModalNuevo = true;
            ViewBag.Editar = "True";
            ViewBag.TituloModal = "Editar";
            return Proveedores(false);
        }

        public ActionResult mostrarModalNuevo()
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.ModalAccion = "nuevo";
            ViewBag.TituloModal = "Nuevo Registro";
            ViewBag.Id = 0;
            return Proveedores(false);
        }

        public ActionResult ActualizarRegistro(Proveedores registro, string id)
        {
            try
            {
                clsConsultas = new ClsProveedores();
                bool respuesta = clsConsultas.ActualizarRegistro(registro, Session["usuario"].ToString(), id);
                if (respuesta)
                {
                    string mensajeNotificacion = "El registro se modificó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Proveedores(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Proveedores();
            }
        }

        public ActionResult GuardarModal(Proveedores registro, string id = "0")
        {
            if (string.IsNullOrEmpty(id) || id.Equals("0"))
            {
                return InsertarRegistro(registro);
            }
            else
            {
                return ActualizarRegistro(registro, id);
            }
        }

        public ActionResult InsertarRegistro(Proveedores conductor)
        {
            try
            {
                clsConsultas = new ClsProveedores();
                bool respuesta = clsConsultas.InsertarRegistro(conductor, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "La información se guardó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Proveedores(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Proveedores();
            }
        }
    }
}