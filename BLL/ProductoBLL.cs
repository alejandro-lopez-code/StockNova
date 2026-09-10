using System.Collections.Generic;
using System.Linq;
using StockNova.Entities;

namespace StockNova.BLL
{
    public class ProductoBLL
    {
        private static List<Producto> listaProductos = new List<Producto>()
        {
            new Producto { Id = 1, Codigo = "P001", Nombre = "Laptop HP", Categoria = "Tecnología", Precio = 750.00m, Stock = 10 },
            new Producto { Id = 2, Codigo = "P002", Nombre = "Teclado Mecánico", Categoria = "Accesorios", Precio = 45.50m, Stock = 25 }
        };

        public static List<Producto> ObtenerProductos()
        {
            return listaProductos;
        }

        public static void Agregar(Producto producto)
        {
            producto.Id = listaProductos.Count > 0 ? listaProductos.Max(p => p.Id) + 1 : 1;
            listaProductos.Add(producto);
        }

        public static void Eliminar(int id)
        {
            var producto = listaProductos.FirstOrDefault(p => p.Id == id);
            if (producto != null)
            {
                listaProductos.Remove(producto);
            }
        }
    }
}