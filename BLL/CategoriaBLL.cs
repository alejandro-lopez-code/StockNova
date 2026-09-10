using System.Collections.Generic;
using StockNova.Entities;

namespace StockNova.BLL
{
    public static class CategoriaBLL
    {
        private static List<Categoria> listaCategorias = new List<Categoria>();
        private static int contadorId = 1;

        public static List<Categoria> ObtenerCategorias()
        {
            return listaCategorias;
        }

        public static void Agregar(Categoria categoria)
        {
            categoria.Id = contadorId++;
            listaCategorias.Add(categoria);
        }

        public static void Eliminar(int id)
        {
            listaCategorias.RemoveAll(c => c.Id == id);
        }
    }
}