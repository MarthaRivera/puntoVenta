using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLInventario
    {
        public static DataTable ListarInventario()
        {
            return new CDInventario().ListarInventario();
        }

        public static String Editar(String parNombre_Producto, decimal parCantidad)
        {
            CDInventario Inventarios = new CDInventario();
            Inventarios.Nombre_Producto = parNombre_Producto;
            Inventarios.Cantidad = parCantidad;

            return Inventarios.Editar(Inventarios);
        }

        public static DataTable Buscar(String parNombre_Buscado)
        {
            CDInventario Inventarios = new CDInventario();
            Inventarios.Nombre_Buscado = parNombre_Buscado;

            return Inventarios.Buscar(Inventarios);
        }
    }
}
