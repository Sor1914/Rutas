using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class ProductosController : Controller
    {
        ClsProductos clsConsultas;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        public ProductosController()
        {
            ViewBag.Layout = LAYOUTMENU;
        }

        public ActionResult Productos(bool limpiar = true)
        {
            if (limpiar)
            {
                HttpContext.Cache.Remove("listaProductos");
            }
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            Paginacion();
            return PartialView("Productos");
        }

        public ActionResult Paginacion(int inicioRegistros = 0, int tamanoPagina = 5, string busqueda = "")
        {
            List<Productos> ListaRegistros = new List<Productos>();
            List<Productos> ListaConductoresFiltro = new List<Productos>();
            clsConsultas = new ClsProductos();
            ListaRegistros = clsConsultas.ObtenerRegistros(inicioRegistros, tamanoPagina, busqueda);
            HttpContext.Cache.Insert("listaProductos", ListaRegistros, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            int cantidadRegistros = clsConsultas.ContarRegistros(busqueda);
            ViewBag.PaginaActualTabla = (inicioRegistros / tamanoPagina) + 1;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaRegistros = ListaRegistros;
            ViewBag.ValorBusqueda = busqueda;
            return PartialView("_Productos");
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
                clsConsultas = new ClsProductos();
                bool seCambio = clsConsultas.ActualizarEstado(estado, id, Session["usuario"].ToString());
                Util.MostrarMensaje(ViewBag, mensaje, 1);
                return Productos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al cambiar el estado", 3);
                return Productos(false);
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
                return Productos(false);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al eliminar la información", 3);
                return Productos(false);

            }
        }

        public ActionResult mostrarModalEditar(string nombreProducto,
                                               string tipoProducto,
                                               string pesoProducto,
                                               int id)
        {
            ViewBag.nombreProducto = nombreProducto;
            ViewBag.tipoProducto = tipoProducto;
            ViewBag.pesoProducto = pesoProducto;
            ViewBag.id = id;
            ViewBag.MostrarModalNuevo = true;
            ViewBag.Editar = "True";
            ViewBag.TituloModal = "Editar";
            return Productos(false);
        }

        public ActionResult mostrarModalProductoProveedor(int idProducto)
        {
            ViewBag.modalProductoProveedor = true;
            ViewBag.IdProducto = idProducto;
            LlenarProveedores();
            return Productos(false);
        }

        public ActionResult mostrarModalDetalleProductoProveedor(int idProducto)
        {
            ViewBag.modalDetalleProductoProveedor = true;
            ObtenerProductoProveedor(idProducto);
            return Productos(false);
        }

        public ActionResult mostrarModalNuevo()
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.ModalAccion = "nuevo";
            ViewBag.TituloModal = "Nuevo Registro";
            ViewBag.Id = 0;
            LlenarTipoProducto("");
            return Productos(false);
        }

        public ActionResult ActualizarRegistro(Productos registro, string id)
        {
            try
            {
                clsConsultas = new ClsProductos();
                bool respuesta = clsConsultas.ActualizarRegistro(registro, Session["usuario"].ToString(), id);
                if (respuesta)
                {
                    string mensajeNotificacion = "El registro se modificó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Productos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Productos();
            }
        }

        public ActionResult GuardarModal(Productos registro, string id = "0")
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

        public ActionResult InsertarRegistro(Productos conductor)
        {
            try
            {
                clsConsultas = new ClsProductos();
                bool respuesta = clsConsultas.InsertarRegistro(conductor, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "La información se guardó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Productos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Productos();
            }
        }

        private void LlenarTipoProducto(string tipoProducto)
        {
            List<SelectListItem> items = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Alimentos", Value = "Ali" },
                    new SelectListItem { Text = "Mueble", Value = "Mue" },
                    new SelectListItem { Text = "Papeleria", Value = "Pap" },
                    new SelectListItem { Text = "Medicamentos", Value = "Med" },
                    new SelectListItem { Text = "Electronico", Value = "Ele"},
                    new SelectListItem { Text = "Ropa", Value = "Rop"},
                    new SelectListItem { Text = "Otro pequeno", Value = "Ope"},
                    new SelectListItem { Text = "Otro grande", Value = "Ogr"}
                };
            var selectedItem = items.Find(i => i.Value == tipoProducto);
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }
            ViewBag.tipoProductoSelectedList = items;
        }

        private void LlenarProveedores()
        {
            List<Proveedores> lista = new List<Proveedores>();
            if (HttpContext.Cache["listaProveedores"] as List<Conductores> != null)
                lista = HttpContext.Cache["listaProveedores"] as List<Proveedores>;
            else
            {
                clsConsultas = new ClsProductos();
                lista = clsConsultas.obtenerProveedores();
                HttpContext.Cache.Insert("listaProveedores", lista, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            var listaSelect = new List<SelectListItem>();
            foreach (var dato in lista)
            {
                listaSelect.Add(new SelectListItem { Value = dato.IdProveedor.ToString(), Text = dato.NombreProveedor});
            }

            ViewBag.listaProveedores = listaSelect;
        }

        public ActionResult GuardarProductoProveedor(ProveedorProducto dato, int idProducto)
        {
            try
            {
                clsConsultas = new ClsProductos();
                dato.IdProducto = idProducto;
                bool respuesta = clsConsultas.InsertarProveedorProducto(dato, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "La información se guardó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Productos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return Productos();
            }
        }

        public ActionResult ObtenerProductoProveedor(int idProducto)
            {
            List<ProveedorProducto> ListaProductoProveedor = new List<ProveedorProducto>();
            clsConsultas = new ClsProductos();
            ListaProductoProveedor = clsConsultas.ObtenerProveedorProducto(idProducto);
            HttpContext.Cache.Insert("listaProveedorProducto", ListaProductoProveedor, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            ViewBag.ListaProductoProveedor = ListaProductoProveedor;
            return Productos();
        }

        public ActionResult notificarEliminacionProductoProveedor(int id, int id2, string tipo)
        {
            try
            {
                int estado;
                string mensaje;
                if (tipo.Equals("Activar"))
                {
                    estado = 1;
                    mensaje = "El registro se activó correctamente";
                }
                else
                {
                    estado = 0;
                    mensaje = "El registro se desactivó correctamente";
                }
                clsConsultas = new ClsProductos();
                bool seCambio = clsConsultas.ActualizarEstadoProveedorProducto(id, id2);
                Util.MostrarMensaje(ViewBag, mensaje, 1);
                return Productos(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al cambiar el estado", 3);
                return Productos(false);
            }
        }

    }
}