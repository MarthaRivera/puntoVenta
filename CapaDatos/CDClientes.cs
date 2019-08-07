using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDClientes
    {
        public String Nombre_Cliente { get; set; }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
        public decimal Credito { get; set; }
        public String Nombre_Buscado { get; set; }
        public string Nombre_ClienteA { get; set; }

        public CDClientes()
        {

        }

        public CDClientes(String parNombre_Cliente, String parDireccion, String parTelefono,  decimal parCredito, String parNombre_Buscado, String parNombre_ClienteA)
        {
            this.Nombre_Cliente = parNombre_Cliente;
            this.Direccion = parDireccion;
            this.Telefono = parTelefono;
            this.Credito = parCredito;
            this.Nombre_Buscado = parNombre_Buscado;
            this.Nombre_ClienteA = parNombre_ClienteA;
        }

        public DataTable ListarClientes()
        {
            DataTable TablaDatos = new DataTable("Ventas.Clientes");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Ventas.ListarClientes";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }
            catch
            {
                TablaDatos = null;
                throw new Exception("Error al Cargar Datos");
            }
            finally
            {
                SqlConexion.Close();
            }
            return TablaDatos;
        }

        public string Insertar(CDClientes parCliente)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Ventas.InsertarClientes";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Cliente = new SqlParameter();
                ParNombre_Cliente.ParameterName = "@NombreClientes";
                ParNombre_Cliente.SqlDbType = SqlDbType.VarChar;
                ParNombre_Cliente.Size = parCliente.Nombre_Cliente.Length;
                ParNombre_Cliente.Value = parCliente.Nombre_Cliente;
                SqlComando.Parameters.Add(ParNombre_Cliente);

                SqlParameter ParDireccion = new SqlParameter();
                ParDireccion.ParameterName = "@Direccion";
                ParDireccion.SqlDbType = SqlDbType.VarChar;
                ParDireccion.Size = parCliente.Direccion.Length;
                ParDireccion.Value = parCliente.Direccion;
                SqlComando.Parameters.Add(ParDireccion);

                SqlParameter ParTelefono = new SqlParameter();
                ParTelefono.ParameterName = "@Telefono";
                ParTelefono.SqlDbType = SqlDbType.VarChar;
                ParTelefono.Size = parCliente.Direccion.Length;
                ParTelefono.Value = parCliente.Direccion;
                SqlComando.Parameters.Add(ParTelefono);

                SqlParameter ParCredito = new SqlParameter();
                ParCredito.ParameterName = "@Credito";
                ParCredito.SqlDbType = SqlDbType.Money;
                ParCredito.Value=parCliente.Credito;
                SqlComando.Parameters.Add(ParCredito);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 8152) //Este error se produce cuando el numero de caracteres es mayor que el admitido en la variable
                {
                    Respuesta = "Error numero de caracteres permitido excedido";
                }
                else if (ex.Number == 2627)
                {
                    Respuesta = "Ya existe un cliente con ese Nombre.";
                }
                else if (ex.Number == 515)
                {
                    Respuesta = "No puedes dejar el campo Nombre vacío.";
                }
                else
                {
                    Respuesta = "Error al intentar insertar cliente" + ex.Message;
                }
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }
            return Respuesta;
        }

        
        public string Editar(CDClientes parCliente)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Ventas.EditarClientes";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_ClienteA = new SqlParameter();
                ParNombre_ClienteA.ParameterName = "@NombreClienteAntes";
                ParNombre_ClienteA.SqlDbType = SqlDbType.VarChar;
                ParNombre_ClienteA.Size = parCliente.Nombre_ClienteA.Length;
                ParNombre_ClienteA.Value = parCliente.Nombre_ClienteA;
                SqlComando.Parameters.Add(ParNombre_ClienteA);

                SqlParameter ParNombre_Cliente = new SqlParameter();
                ParNombre_Cliente.ParameterName = "@NombreClientes";
                ParNombre_Cliente.SqlDbType = SqlDbType.VarChar;
                ParNombre_Cliente.Size = parCliente.Nombre_Cliente.Length;
                ParNombre_Cliente.Value = parCliente.Nombre_Cliente;
                SqlComando.Parameters.Add(ParNombre_Cliente);

                SqlParameter ParDireccion = new SqlParameter();
                ParDireccion.ParameterName = "@Direccion";
                ParDireccion.SqlDbType = SqlDbType.VarChar;
                ParDireccion.Size = parCliente.Direccion.Length;
                ParDireccion.Value = parCliente.Direccion;
                SqlComando.Parameters.Add(ParDireccion);

                SqlParameter ParTelefono = new SqlParameter();
                ParTelefono.ParameterName = "@Telefono";
                ParTelefono.SqlDbType = SqlDbType.VarChar;
                ParTelefono.Size = parCliente.Direccion.Length;
                ParTelefono.Value = parCliente.Direccion;
                SqlComando.Parameters.Add(ParTelefono);

                SqlParameter ParCredito = new SqlParameter();
                ParCredito.ParameterName = "@Credito";
                ParCredito.SqlDbType = SqlDbType.Money;
                ParCredito.Value=parCliente.Credito;
                SqlComando.Parameters.Add(ParCredito);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 8152) //Este error se produce cuando el numero de caracteres es mayor que el admitido en la variable
                {
                    Respuesta = "Error numero de caracteres permitido excedido";
                }
                else if (ex.Number == 2627)
                {
                    Respuesta = "Ya existe un cliente con ese Nombre.";
                }
                else if (ex.Number == 515)
                {
                    Respuesta = "No puedes dejar el campo Nombre vacío.";
                }
                else
                {
                    Respuesta = "Error al intentar insertar cliente" + ex.Message;
                }
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }
            return Respuesta;
        }
        public DataTable Buscar(CDClientes parCliente)
        {
            DataTable TablaDatos = new DataTable("Ventas.Clientes");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Ventas.BuscarCliente";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Buscado = new SqlParameter();
                ParNombre_Buscado.ParameterName = "@NombreBuscado";
                ParNombre_Buscado.SqlDbType = SqlDbType.VarChar;
                ParNombre_Buscado.Size = parCliente.Nombre_Buscado.Length;
                ParNombre_Buscado.Value = parCliente.Nombre_Buscado;
                SqlComando.Parameters.Add(ParNombre_Buscado);

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Ventas.BuscarCliente. " + ex.Message, ex);
            }

            finally
            {
                if (SqlConexion.State == ConnectionState.Open)
                {
                    SqlConexion.Close();
                }
            }

            return TablaDatos;
        }
    }
}
