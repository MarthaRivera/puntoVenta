using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLProductos
    {
        private Modelos bd;
        public CLProductos()
        {
            bd = new Modelos();
        }

        public static DataTable ListarProductoVenta()
        {
            return new CDProductos().ListarProductoVenta();
        }

        public static DataTable Mostrar(int parRegistrosPorPagina, int parNumeroPagina)
        {
            return new CDProductos().Mostrar(parRegistrosPorPagina, parNumeroPagina);
        }
        public static int Tamaño(int parRegistrosPorPagina)
        {
            return new CDProductos().Tamaño(parRegistrosPorPagina);
        }
        public static DataTable Buscar(String parNombreBuscado)
        {
            CDProductos producto = new CDProductos();
            producto.Nombre_buscado = parNombreBuscado;
            return producto.Buscar(producto);
        }
        public static String Insertar(String parNombre_Producto, String parNombre_Categoria, String parUnidad, decimal parPrecioCosto, decimal parGanancia, decimal parPrecioVenta, decimal parCantidad)
        {
            CDProductos producto = new CDProductos();
            producto.Nombre_Producto = parNombre_Producto;
            producto.Nombre_Categoria = parNombre_Categoria;
            producto.Unidad = parUnidad;
            producto.Precio_Costo = parPrecioCosto;
            producto.Ganancia = parGanancia;
            producto.Precio_Venta = parPrecioVenta;
            producto.Cantidad = parCantidad;

            return producto.Insertar(producto);
            
        }

        public static String Eliminar(int parId_Producto)
        {
            CDProductos productos = new CDProductos();
            productos.Id_Producto = parId_Producto;

            return productos.Eliminar(productos);
        }

        public static String Editar(int parId_Producto, String parNombre_Producto, String parNombre_Categoria, String parUnidad, decimal parPrecioCosto, decimal parGanancia, decimal parPrecioVenta, decimal parCantidad)
        {
            CDProductos producto = new CDProductos();
            producto.Id_Producto = parId_Producto;
            producto.Nombre_Producto = parNombre_Producto;
            producto.Nombre_Categoria = parNombre_Categoria;
            producto.Unidad = parUnidad;
            producto.Precio_Costo = parPrecioCosto;
            producto.Ganancia = parGanancia;
            producto.Precio_Venta = parPrecioVenta;
            producto.Cantidad = parCantidad;

            return producto.Editar(producto);

        }

    }
}
