using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace DistribucionRutas.Controllers
{
    public class ConductoresController : Controller
    {
        ClsConductores clsConductores;
        string LAYOUTMENU = "~/Views/Shared/_LayoutMenu.cshtml";

        public ConductoresController()
        {
            ViewBag.Layout = LAYOUTMENU;
        }
        // GET: Conductores    
        public async Task<ActionResult> Conductores(bool limpiar = true)
        {
            if (limpiar)
            {
                HttpContext.Cache.Remove("listaConductores");
            }
            ViewBag.PaginaActual = ControllerContext.RouteData.Values["action"].ToString();
            Paginacion();
            LlenarHorarios(0);
            LlenarUsuarios("");
            LlenarTiposLicencia("");
            return PartialView("Conductores");
        }

        public ActionResult Paginacion(int inicioRegistros = 0, int tamanoPagina = 5, string busqueda = "")
        {
            List<Conductores> ListaConductores = new List<Conductores>();
            List<Conductores> ListaConductoresFiltro = new List<Conductores>();
            clsConductores = new ClsConductores();
            ListaConductores = clsConductores.ObtenerConductores(inicioRegistros, tamanoPagina, busqueda);
            HttpContext.Cache.Insert("listaUsuarios", ListaConductores, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            int cantidadRegistros = clsConductores.ContarConductores(busqueda);
            ViewBag.PaginaActualTabla = (inicioRegistros / tamanoPagina) + 1;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaTipos = ListaConductores;
            ViewBag.ValorBusqueda = busqueda;
            LlenarHorarios(0);
            LlenarUsuarios("");
            LlenarTiposLicencia("");
            return PartialView("_Conductores");
        }

        public async Task<ActionResult> CambiarEstado(string licencia, string tipoGestion)
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
                clsConductores = new ClsConductores();
                bool seCambio = clsConductores.ActualizarEstado(estado,licencia, Session["usuario"].ToString());
                Util.MostrarMensaje(ViewBag, mensaje, 1);
                return await Conductores(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al cambiar el estado", 3);
                return await Conductores(false);
            }
        }

        public async Task<ActionResult> notificarEliminacion(string licencia, string tipo)
        {
            try
            {
                ViewBag.TipoGestion = tipo;
                ViewBag.idEliminar = licencia;
                ViewBag.Eliminar = "True";
                string mensaje = $"¿Está seguro de {tipo} este conductor?";
                Util.MostrarMensaje(ViewBag, mensaje, 4);
                return await Conductores(false);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al eliminar la información", 3);
                return await Conductores(false);

            }
        }

        public async Task<ActionResult> mostrarModalEditar(string tipoLicencia,
                                                           int idHorario,
                                                           int limiteParadas,
                                                           string licencia,
                                                           string usuario)
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.Editar = "True";
            ViewBag.tipoLicencia = tipoLicencia;
            ViewBag.idHorario = idHorario;
            ViewBag.limiteParadas = limiteParadas;
            ViewBag.licencia = licencia;
            ViewBag.TituloModal = "Editar";
            ViewBag.Usuario = usuario;
            LlenarHorarios(idHorario);
            LlenarUsuarios(usuario);
            LlenarTiposLicencia(tipoLicencia);
            return await Conductores(false);
        }

        private void LlenarHorarios(int idHorario)
        {
            DataTable dtHorarios;
            if (HttpContext.Cache["dtHorarios"] as List<Roles> != null)
                dtHorarios = HttpContext.Cache["dtHorarios"] as DataTable;
            else
            {
                clsConductores = new ClsConductores();
                dtHorarios = clsConductores.ObtenerHorarios();
                HttpContext.Cache.Insert("dtHorarios", dtHorarios, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            var horariosSelectedList = new List<SelectListItem>(); // Crear una lista vacía de objetos SelectListItem                                

            //obtener la lista de regiones
            foreach (DataRow horario in dtHorarios.Rows)
            {
                // Agregar cada objeto Region a la lista de objetos SelectListItem
                if (idHorario.ToString() == horario[0].ToString())
                    horariosSelectedList.Add(new SelectListItem { Value = horario[0].ToString(), Text = horario[0].ToString(), Selected = true });
                else
                    horariosSelectedList.Add(new SelectListItem { Value = horario[0].ToString(), Text = horario[0].ToString() });
            }
            ViewBag.horarioSelectList = horariosSelectedList;
        }

        private void LlenarUsuarios(string usuarioActual)
        {
            DataTable dtUsuarios;
            if (HttpContext.Cache["dtUsuarios"] as List<Roles> != null)
                dtUsuarios = HttpContext.Cache["dtUsuarios"] as DataTable;
            else
            {
                clsConductores = new ClsConductores();
                dtUsuarios = clsConductores.ObtenerUsuariosConductores();
                HttpContext.Cache.Insert("dtUsuarios", dtUsuarios, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            var usuariosSelectedList = new List<SelectListItem>(); // Crear una lista vacía de objetos SelectListItem                                

            //obtener la lista de regiones
            foreach (DataRow usuario in dtUsuarios.Rows)
            {
                // Agregar cada objeto Region a la lista de objetos SelectListItem
                if (usuarioActual == usuario[0].ToString())
                    usuariosSelectedList.Add(new SelectListItem { Value = usuario[0].ToString(), Text = usuario[0].ToString(), Selected = true });
                else
                    usuariosSelectedList.Add(new SelectListItem { Value = usuario[0].ToString(), Text = usuario[0].ToString() });
            }
            ViewBag.usuarioSelectedList = usuariosSelectedList;
        }

        private void LlenarTiposLicencia(string tipoLicencia)
        {
            List<SelectListItem> items = new List<SelectListItem>
{
    new SelectListItem { Text = "M", Value = "M" },
    new SelectListItem { Text = "A", Value = "A" },
    new SelectListItem { Text = "B", Value = "B" },
    new SelectListItem { Text = "C", Value = "C" }
};             
            var selectedItem = items.Find(i => i.Value == tipoLicencia);
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }
            ViewBag.tipoLicenciaSelectList = items;
        }

        public async Task<ActionResult> mostrarModalNuevo()
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.ModalAccion = "nuevo";
            ViewBag.TituloModal = "Nuevo Registro";
            ViewBag.Id = 0;
            return await Conductores(false);
        }

        public async Task<ActionResult> ActualizarConductor(Conductores conductor)
        {
            try
            {
                clsConductores = new ClsConductores();
                bool respuesta = clsConductores.ActualizarConductor(conductor, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "El registro se modificó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return await Conductores(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return await Conductores();
            }
        }

        public async Task<ActionResult> GuardarModal(Conductores conductor, string licencia)
        {
            if (string.IsNullOrEmpty(licencia))
            {
                return await InsertarConductor(conductor);
            }
            else
            {
                return await ActualizarConductor(conductor);
            }
        }

        public async Task<ActionResult> InsertarConductor(Conductores conductor)
        {
            try
            {
                clsConductores = new ClsConductores();
                bool respuesta = clsConductores.InsertarConductor(conductor, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "La información se guardó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return await Conductores(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return await Conductores();
            }
        }


    }
}