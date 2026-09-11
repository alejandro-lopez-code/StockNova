using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StockNova.Entities;

namespace StockNova.BLL
{
    public class CategoriaBLL
    {
        public static List<Categoria> ObtenerCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection conexion = Conexion.InstanciaConexion())
            {
                string query = "SELECT Id, Nombre, Descripcion FROM Categorias";
                SqlCommand cmd = new SqlCommand(query, conexion);
                conexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Categoria
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public static void Agregar(Categoria c)
        {
            using (SqlConnection conexion = Conexion.InstanciaConexion())
            {
                string query = "INSERT INTO Categorias (Nombre, Descripcion) VALUES (@nombre, @descripcion)";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", c.Descripcion ?? (object)DBNull.Value);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Eliminar(int id)
        {
            using (SqlConnection conexion = Conexion.InstanciaConexion())
            {
                string query = "DELETE FROM Categorias WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}