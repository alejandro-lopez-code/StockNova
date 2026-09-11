using StockNova.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StockNova.BLL
{
    public class ProductoBLL
    {
        public static List<StockNova.Entities.Producto> ObtenerProductos()
        {
            List<StockNova.Entities.Producto> lista = new List<StockNova.Entities.Producto>();

            using (SqlConnection conexion = Conexion.InstanciaConexion())
            {
                string query = "SELECT Id, Codigo, Nombre, Categoria, Precio, Stock FROM Productos";
                SqlCommand cmd = new SqlCommand(query, conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        StockNova.Entities.Producto prod = new StockNova.Entities.Producto()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Codigo = reader["Codigo"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Categoria = reader["Categoria"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Stock = Convert.ToInt32(reader["Stock"])
                        };
                        lista.Add(prod);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error al cargar productos: " + ex.Message);
                }
            }

            return lista;
        }

        public static void Agregar(StockNova.Entities.Producto p)
        {
            using (SqlConnection conexion = Conexion.InstanciaConexion())
            {
                string query = "INSERT INTO Productos (Codigo, Nombre, Categoria, Precio, Stock) VALUES (@codigo, @nombre, @categoria, @precio, @stock)";
                SqlCommand cmd = new SqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@codigo", p.Codigo);
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@categoria", p.Categoria);
                cmd.Parameters.AddWithValue("@precio", p.Precio);
                cmd.Parameters.AddWithValue("@stock", p.Stock);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Eliminar(int id)
        {
            using (SqlConnection conexion = Conexion.InstanciaConexion())
            {
                string query = "DELETE FROM Productos WHERE Id = @id";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}