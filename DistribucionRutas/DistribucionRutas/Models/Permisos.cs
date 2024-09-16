namespace DistribucionRutas.Models
{
    public class Permisos
    {
        public bool GestionConductores { get; set; } = false;
        public bool GestionUsuarios { get; set; } = false;
        public bool GestionTipoVehiculo { get;set; } = false;
        public bool GestionVehiculos { get; set; } = false;
        public bool GestionProveedores { get; set; } = false;
        public bool GestionProductos { get; set; } = false; 
    }
}