using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLCategoria
    {
        public static DataTable ListarCategoria()
        {
            return new CDCategoria().ListarCategoria();
        }

        public static DataTable ListarCategoriaCB()
        {
            return new CDCategoria().ListarCategoriaCB();
        }

        public static DataTable Buscar(String parNombre_Buscado)
        {
            CDCategoria categorias = new CDCategoria();
            categorias.Nombre_Buscado = parNombre_Buscado;

            return categorias.Buscar(categorias);
        }



        public static String Insertar(String parNombre_Categoria)
        {
            CDCategoria categorias = new CDCategoria();
            categorias.Nombre_Categoria = parNombre_Categoria;

            return categorias.Insertar(categorias);
        }

        public static String Eliminar(String parNombre_Categoria)
        {
            CDCategoria categorias = new CDCategoria();
            categorias.Nombre_Categoria = parNombre_Categoria;

            return categorias.Eliminar(categorias);
        }

        public static String Editar(String parNombre_Categoria, String parNuevoNombre_Categoria)
        {
            CDCategoria categorias = new CDCategoria();
            categorias.Nombre_Categoria = parNombre_Categoria;
            categorias.NuevoNombre_Categoria = parNuevoNombre_Categoria;

            return categorias.Editar(categorias);
        }
    }
}
