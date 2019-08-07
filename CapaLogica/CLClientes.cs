using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLClientes
    {
        public static DataTable ListarClientes()
        {
            return new CDClientes().ListarClientes();
        }

        public static String Insertar(String parNombre_Cliente, String parDireccion, String parTelefono, decimal parCredito)
        {
            CDClientes clientes = new CDClientes();

            clientes.Nombre_Cliente = parNombre_Cliente;
            clientes.Direccion = parDireccion;
            clientes.Telefono = parTelefono;
            clientes.Credito = parCredito;

            return clientes.Insertar(clientes);
        }

        public static String Editar(string parNombre_ClienteA, String parNombre_Cliente, String parDireccion, String parTelefono, decimal parCredito)
        {
            CDClientes clientes = new CDClientes();

            clientes.Nombre_Cliente = parNombre_Cliente;
            clientes.Direccion = parDireccion;
            clientes.Telefono = parTelefono;
            clientes.Credito = parCredito;
            clientes.Nombre_ClienteA = parNombre_ClienteA;

            return clientes.Editar(clientes);
        }

        public static DataTable Buscar(String parNombre_Buscado)
        {
            CDClientes BuscarCliente = new CDClientes();
            BuscarCliente.Nombre_Buscado = parNombre_Buscado;

            return BuscarCliente.Buscar(BuscarCliente);
        }

    }
}
