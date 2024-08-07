using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DistribucionRutas.Clases
{
    public class ClsConexionSql
    {
        //private SqlConnection Conexion = new SqlConnection("Server=10.238.76.168;DataBase=master;User Id=sa;Password=8ac3etuq");
        private SqlConnection Conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conexionsql"].ToString());

        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }        

        public DataTable CrearConsulta(string consulta)
        {
            DataTable dtResultado = new DataTable();
            SqlCommand cmd = new SqlCommand(consulta, Conexion);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            AbrirConexion();
            reader = cmd.ExecuteReader();
            dtResultado.Load(reader);
            CerrarConexion();
            return dtResultado;
        }

        public bool CrearDML(string consulta)
        {
            SqlCommand cmd = new SqlCommand(consulta, Conexion);
            cmd.CommandType = CommandType.Text;
            AbrirConexion();
            int i = cmd.ExecuteNonQuery();
            CerrarConexion();
            return i > 0;      
        }
    }
}
