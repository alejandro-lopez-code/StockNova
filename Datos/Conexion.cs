using System;
using System.Data.SqlClient;

namespace StockNova
{
    public class Conexion
    {
        private static string cadena = "Server=localhost; Database=StockNovaDB; Integrated Security=true;";

        public static SqlConnection InstanciaConexion()
        {
            SqlConnection conexion = new SqlConnection(cadena);
            return conexion;
        }
    }
}