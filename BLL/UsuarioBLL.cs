using System;
using System.Data.SqlClient;

namespace StockNova
{
    public class UsuarioBLL
    {
        public bool ValidarLogin(string usuario, string password)
        {
            bool resultado = false;

            using (SqlConnection conexion = Conexion.InstanciaConexion())
            {
                string query = "SELECT COUNT(1) FROM Usuarios WHERE RTRIM(Username) = @user AND RTRIM(Password) = @pass";
                SqlCommand cmd = new SqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@user", usuario);
                cmd.Parameters.AddWithValue("@pass", password);

                try
                {
                    conexion.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        resultado = true; 
                    }
                }
                catch (Exception)
                {
                    resultado = false;
                }
            }

            return resultado;
        }
    }
}