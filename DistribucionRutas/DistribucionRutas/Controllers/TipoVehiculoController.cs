using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class TipoVehiculoController : Controller
    {
        ClsTipoVehiculo clsConsultas;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        public TipoVehiculoController()
        {
            ViewBag.Layout = LAYOUTMENU;
        }

        public ActionResult TipoVehiculo(bool limpiar = true)
        {
            if (limpiar)
            {
                HttpContext.Cache.Remove("listaConductores");
            }
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            Paginacion();
            LlenarTipoVehiculo("");
            LlenarTipoGas("");
            return PartialView("TipoVehiculo");
        }

        public ActionResult Paginacion(int inicioRegistros = 0, int tamanoPagina = 5, string busqueda = "")
        {
            List<TipoVehiculo> ListaRegistros = new List<TipoVehiculo>();
            List<TipoVehiculo> ListaConductoresFiltro = new List<TipoVehiculo>();
            clsConsultas = new ClsTipoVehiculo();
            ListaRegistros = clsConsultas.ObtenerRegistros(inicioRegistros, tamanoPagina, busqueda);
            HttpContext.Cache.Insert("listaTipoVehiculo", ListaRegistros, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            int cantidadRegistros = clsConsultas.ContarRegistros(busqueda);
            ViewBag.PaginaActualTabla = (inicioRegistros / tamanoPagina) + 1;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaRegistros = ListaRegistros;
            ViewBag.ValorBusqueda = busqueda;
            LlenarTipoVehiculo("");
            LlenarTipoGas("");
            return PartialView("_TipoVehiculo");
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
                clsConsultas = new ClsTipoVehiculo();
                bool seCambio = clsConsultas.ActualizarEstado(estado, id, Session["usuario"].ToString());
                Util.MostrarMensaje(ViewBag, mensaje, 1);
                return TipoVehiculo(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al cambiar el estado", 3);
                return TipoVehiculo(false);
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
                return TipoVehiculo(false);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al eliminar la información", 3);
                return TipoVehiculo(false);

            }
        }

        public ActionResult mostrarModalEditar(string capacidadPeso,
                                                           string KMXGalon,
                                                           string Galones,
                                                           string Descripcion,
                                                           string IdTipoVehiculo,
                                                           string tipoGas)
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.Editar = "True";
            ViewBag.CapacidadPeso = capacidadPeso;
            ViewBag.KMXgalon = KMXGalon;
            ViewBag.Galones = Galones;
            ViewBag.Descripcion = Descripcion;
            ViewBag.Id = IdTipoVehiculo;
            ViewBag.TituloModal = "Editar";
            LlenarTipoVehiculo(IdTipoVehiculo);
            LlenarTipoGas(tipoGas);
            return TipoVehiculo(false);
        }

        private void LlenarTipoVehiculo(string tipoVehiculo)
        {
            List<SelectListItem> items = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Moto", Value = "Moto" },
                    new SelectListItem { Text = "Furgoneta", Value = "Furgoneta" },
                    new SelectListItem { Text = "PickUp", Value = "PickUp" },
                    new SelectListItem { Text = "Camión Ligero", Value = "CLigero" },
                    new SelectListItem { Text = "Camión Mediano", Value = "CMediano"},
                    new SelectListItem { Text = "Camión Pesado", Value = "CPesado"}
                };
            var selectedItem = items.Find(i => i.Value == tipoVehiculo);
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }
            ViewBag.tipoVehiculoSelectedList = items;
        }

        private void LlenarTipoGas(string tipogas)
        {
            List<SelectListItem> items = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Regular", Value = "Reg" },
                    new SelectListItem { Text = "Premium", Value = "Pre" },
                    new SelectListItem { Text = "Diesel", Value = "Die" },
                };
            var selectedItem = items.Find(i => i.Value == tipogas);
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }
            ViewBag.tipoGasSelectedList = items;
        }

        public ActionResult mostrarModalNuevo()
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.ModalAccion = "nuevo";
            ViewBag.TituloModal = "Nuevo Registro";
            ViewBag.Id = 0;
            return TipoVehiculo(false);
        }

        public ActionResult ActualizarRegistro(TipoVehiculo registro, string id)
        {
            try
            {
                clsConsultas = new ClsTipoVehiculo();
                bool respuesta = clsConsultas.ActualizarRegistro(registro, Session["usuario"].ToString(), id);
                if (respuesta)
                {
                    string mensajeNotificacion = "El registro se modificó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return TipoVehiculo(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return TipoVehiculo();
            }
        }

        public ActionResult GuardarModal(TipoVehiculo registro, string id = "0")
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

        public ActionResult InsertarRegistro(TipoVehiculo conductor)
        {
            try
            {
                clsConsultas = new ClsTipoVehiculo();
                bool respuesta = clsConsultas.InsertarRegistro(conductor, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "La información se guardó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return TipoVehiculo(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return TipoVehiculo();
            }
        }
    }
}