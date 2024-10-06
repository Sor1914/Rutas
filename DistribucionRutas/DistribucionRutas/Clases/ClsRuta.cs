using DistribucionRutas.Consultas;
using DistribucionRutas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DistribucionRutas.Clases
{
    public class ClsRuta
    {
        ClsConexionSql conexionSql;
        public List<Ruta> ObtenerRegistros()
        {
            var consulta = string.Format(SqlRuta.ObtenerPedidos); 
            conexionSql = new ClsConexionSql();
            var dtConductores = conexionSql.CrearConsulta(consulta);
            List<Ruta> lista = Util.ConvertirDataTableALista<Ruta>(dtConductores);
            return lista;
        }
    }
}