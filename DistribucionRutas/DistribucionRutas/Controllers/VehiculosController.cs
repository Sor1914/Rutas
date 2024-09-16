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
    public class VehiculosController : Controller
    {
        ClsVehiculos clsConsultas;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        public VehiculosController()
        {
            ViewBag.Layout = LAYOUTMENU;
        }

        public ActionResult Vehiculos(bool limpiar = true)
        {
            if (limpiar)
            {
                HttpContext.Cache.Remove("listaVehiculos");
            }
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            Paginacion();
            return PartialView("Vehiculos");
        }

        public ActionResult Paginacion(int inicioRegistros = 0, int tamanoPagina = 5, string busqueda = "")
        {
            List<Vehiculos> ListaRegistros = new List<Vehiculos>();
            List<Vehiculos> ListaConductoresFiltro = new List<Vehiculos>();
            clsConsultas = new ClsVehiculos();
            ListaRegistros = clsConsultas.ObtenerRegistros(inicioRegistros, tamanoPagina, busqueda);
            HttpContext.Cache.Insert("listaVehiculos", ListaRegistros, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            int cantidadRegistros = clsConsultas.ContarRegistros(busqueda);
            ViewBag.PaginaActualTabla = (inicioRegistros / tamanoPagina) + 1;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaRegistros = ListaRegistros;
            ViewBag.ValorBusqueda = busqueda;
            return PartialView("_Vehiculos");
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
                clsConsultas = new ClsVehiculos();
                bool seCambio = clsConsultas.ActualizarEstado(estado, id, Session["usuario"].ToString());
                Util.MostrarMensaje(ViewBag, mensaje, 1);
                return Vehiculos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al cambiar el estado", 3);
                return Vehiculos(false);
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
                return Vehiculos(false);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al eliminar la información", 3);
                return Vehiculos(false);

            }
        }

        public ActionResult mostrarModalEditar(string placa,
                                               string licenciaConductor,
                                               int idTipoVehiculo,
                                               string kmRecorridos)
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.Editar = "True";
            ViewBag.placa = placa;
            ViewBag.licenciaConductor = licenciaConductor;
            ViewBag.idTIpoVehiculo = idTipoVehiculo;
            ViewBag.TituloModal = "Editar";
            ViewBag.kmRecorridos = kmRecorridos;
            LlenarConductores(licenciaConductor);
            LlenarTipoVehiculo(idTipoVehiculo);
            return Vehiculos(false);
        }

        private void LlenarConductores(string licencia)
        {
            ViewBag.licenciaConductor = licencia;
            List<Conductores> lista = new List<Conductores>();
            if (HttpContext.Cache["listaConductores"] as List<Conductores> != null)
                lista = HttpContext.Cache["listaConductores"] as List<Conductores>;
            else
            {
                clsConsultas = new ClsVehiculos();
                lista = clsConsultas.ObtenerConductores();
                HttpContext.Cache.Insert("listaConductores", lista, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            var listaConductores = new List<SelectListItem>();                
            foreach (var dato in lista)
            {
                if (licencia == dato.Licencia)
                    listaConductores.Add(new SelectListItem { Value = dato.Licencia.ToString(), Text = dato.Licencia, Selected = true });
                else
                    listaConductores.Add(new SelectListItem { Value = dato.Licencia.ToString(), Text = dato.Licencia });
            }
            if (!string.IsNullOrEmpty(licencia)) 
            {
                listaConductores.Add(new SelectListItem { Value = licencia, Text = licencia, Selected = true });
            }
            ViewBag.listaConductoresSelectList = listaConductores;
        }

        private void LlenarTipoVehiculo(int tipoVehiculo)
        {
            List<TipoVehiculo> lista = new List<TipoVehiculo>();
            if (HttpContext.Cache["listaTipoVehiculos"] as List<TipoVehiculo> != null)
                lista = HttpContext.Cache["listaTipoVehiculos"] as List<TipoVehiculo>;
            else
            {
                lista = clsConsultas.ObtenerTipoVehiculos();
                HttpContext.Cache.Insert("listaTipoVehiculos", lista, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            var listaTipoVEhiculo = new List<SelectListItem>();
            foreach (var dato in lista)
            {
                if (tipoVehiculo == dato.IdTipoVehiculo)
                    listaTipoVEhiculo.Add(new SelectListItem { Value = dato.IdTipoVehiculo.ToString(), Text = dato.Descripcion + "(" + dato.CapacidadPeso + ")", Selected = true });
                else
                    listaTipoVEhiculo.Add(new SelectListItem { Value = dato.IdTipoVehiculo.ToString(), Text = dato.Descripcion + "(" + dato.CapacidadPeso + ")" });
            }
            ViewBag.listaTipoVehiculoSelected = listaTipoVEhiculo;
        }

        public ActionResult mostrarModalNuevo()
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.ModalAccion = "nuevo";
            ViewBag.TituloModal = "Nuevo Registro";
            ViewBag.Id = 0;
            LlenarConductores("");
            LlenarTipoVehiculo(0);
            return Vehiculos(false);
        }

        public ActionResult ActualizarRegistro(Vehiculos registro, string id)
        {
            try
            {
                clsConsultas = new ClsVehiculos();
                bool respuesta = clsConsultas.ActualizarRegistro(registro, Session["usuario"].ToString(), id);
                if (respuesta)
                {
                    string mensajeNotificacion = "El registro se modificó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Vehiculos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Vehiculos();
            }
        }

        public ActionResult GuardarModal(Vehiculos registro, string id = "0")
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

        public ActionResult InsertarRegistro(Vehiculos conductor)
        {
            try
            {
                clsConsultas = new ClsVehiculos();
                bool respuesta = clsConsultas.InsertarRegistro(conductor, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "La información se guardó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Vehiculos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Vehiculos();
            }
        }
    }
}