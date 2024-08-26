using DistribucionRutas.Clases;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using Roles = DistribucionRutas.Models.Roles;

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
            LlenarRoles(0);
            return PartialView("Usuarios");
        }

        public ActionResult Paginacion(int inicioRegistros = 0, int tamanoPagina = 5, string busqueda = "")
        {
            List<Usuarios> ListaUsuarios = new List<Usuarios>();
            List<Usuarios> ListaUsuariosFiltro = new List<Usuarios>();
            clsUsuarios = new ClsUsuarios();
            ListaUsuarios = Util.ConvertirDataTableALista<Usuarios>(clsUsuarios.ObtenerUsuarios(inicioRegistros, tamanoPagina, busqueda));
            HttpContext.Cache.Insert("listaUsuarios", ListaUsuarios, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            int cantidadRegistros = clsUsuarios.CuentaUsuarios(busqueda);
            ViewBag.PaginaActualTabla = (inicioRegistros / tamanoPagina) + 1;
            ViewBag.TamanoPagina = tamanoPagina;
            ViewBag.TotalElementos = cantidadRegistros;
            ViewBag.TotalPaginas = (int)Math.Ceiling((decimal)cantidadRegistros / tamanoPagina);
            ViewBag.ListaTipos = ListaUsuarios;
            ViewBag.ValorBusqueda = busqueda;
            LlenarRoles(0);
            return PartialView("_Usuarios");
        }

        public async Task<ActionResult> notificarEliminacion(string usuario, string tipo)
        {
            try
            {
                ViewBag.TipoGestion = tipo;
                ViewBag.IdEliminar = usuario;
                ViewBag.Eliminar = "True";
                string mensaje = $"¿Está seguro de {tipo} este usuario?";
                Util.MostrarMensaje(ViewBag, mensaje, 4);
                return await Usuarios(false);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al eliminar la información", 3);
                return await Usuarios(false);

            }
        }

        public async Task<ActionResult> mostrarModalEditar(string Usuario, string Nombre, int idRol)
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.Usuario = Usuario;
            ViewBag.Nombre = Nombre;
            ViewBag.IdRol = idRol;
            ViewBag.TituloModal = "Editar Rol";
            LlenarRoles(idRol);
            return await Usuarios(false);
        }

        private void LlenarRoles(int idRol)
        {
            List<Roles> listaRoles = new List<Roles>();
            if (HttpContext.Cache["listaRoles"] as List<Roles> != null)
                listaRoles = HttpContext.Cache["listaRoles"] as List<Roles>;
            else
            {
                listaRoles = Util.ConvertirDataTableALista<Roles>(clsUsuarios.ObtenerRoles());
                HttpContext.Cache.Insert("listaRoles", listaRoles, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            //obtener la lista de regiones
            var rolesSelectList = new List<SelectListItem>(); // Crear una lista vacía de objetos SelectListItem                                
            foreach (var rol in listaRoles)
            {
                // Agregar cada objeto Region a la lista de objetos SelectListItem
                if (idRol == rol.IdRol)
                    rolesSelectList.Add(new SelectListItem { Value = rol.IdRol.ToString(), Text = rol.Descripcion, Selected = true });
                else
                    rolesSelectList.Add(new SelectListItem { Value = rol.IdRol.ToString(), Text = rol.Descripcion });
            }
            ViewBag.RolSelectList = rolesSelectList;
        }

        public async Task<ActionResult> CambiarEstadoUsuario(string usuario, string tipo)
        {
            try
            {
                int estado;
                string mensaje;
                if (tipo.Equals("Activar"))
                {
                    estado = 1;
                    mensaje = "El usuario se activó correctamente";
                }
                else
                {
                    estado = 0;
                    mensaje = "El usuario se desactivó correctamente";
                }
                clsUsuarios = new ClsUsuarios();
                bool seCambio = clsUsuarios.CambiarEstadoUsuario(usuario, Session["usuario"].ToString(), estado);
                Util.MostrarMensaje(ViewBag, mensaje, 1);
                return await Usuarios(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al cambiar el estado", 3);
                return await Usuarios(false);
            }
        }

        public async Task<ActionResult> mostrarModalNuevo()
        {
            ViewBag.MostrarModalNuevo = true;
            ViewBag.ModalAccion = "nuevo";
            ViewBag.TituloModal = "Creación de Puntos de Atención";
            ViewBag.Id = 0;
            return await Usuarios(false);
        }

        public async Task<ActionResult> actualizarRol(Usuarios usuario, string usuarioModificado)
        {
            try
            {
                clsUsuarios = new ClsUsuarios();
                bool respuesta = clsUsuarios.AsignarRol(usuario.IdRol, usuarioModificado, Session["usuario"].ToString());
                if (respuesta)
                {
                    string mensajeNotificacion = "El rol se modificó correctamente";
                    Util.MostrarMensaje(ViewBag, mensajeNotificacion, 1);
                }
                else
                    Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return await Usuarios(true);
            }
            catch (Exception ex)
            {
                Util.MostrarMensaje(ViewBag, "Hubo un error al guardar la información", 3);
                return await Usuarios();
            }
        }
    }
}