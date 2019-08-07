using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDInventario
    {
        public String  Nombre_Producto { get; set; }
        public decimal Cantidad { get; set; }
        public String Nombre_Buscado { get; set; }

        public CDInventario()
        {

        }

        public CDInventario(String parNombre_Producto, decimal parCantidad, String parNombre_Buscado)
        {
            this.Nombre_Producto = parNombre_Producto;
            this.Cantidad = parCantidad;
            this.Nombre_Buscado = parNombre_Buscado;
        }

        public DataTable ListarInventario()
        {
            DataTable TablaDatos = new DataTable("Productos.V_Inventario");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.InventarioListar";
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

        public string Editar(CDInventario parInventario)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.InventarioModificar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Producto = new SqlParameter();
                ParNombre_Producto.ParameterName = "@NombreProducto";
                ParNombre_Producto.SqlDbType = SqlDbType.VarChar;
                ParNombre_Producto.Size = parInventario.Nombre_Producto.Length;
                ParNombre_Producto.Value = parInventario.Nombre_Producto;
                SqlComando.Parameters.Add(ParNombre_Producto);

                SqlParameter ParCantidad = new SqlParameter();
                ParCantidad.ParameterName = "@Cantidad";
                ParCantidad.SqlDbType = SqlDbType.Decimal;
                ParCantidad.Value = parInventario.Cantidad;
                SqlComando.Parameters.Add(ParCantidad);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    Respuesta = "No puedes eliminar una categoría que cuenta con Productos. Debes eliminar o actualizar sus Productos antes de eliminar la categoría.";
                }

                else
                {
                    Respuesta = "Error al intentar editar" + ex.Message;
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

        public DataTable Buscar(CDInventario parInventario)
        {
            DataTable TablaDatos = new DataTable("Productos.V_Inventario");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.InventarioBuscar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Buscado = new SqlParameter();
                ParNombre_Buscado.ParameterName = "@NombreBuscado";
                ParNombre_Buscado.SqlDbType = SqlDbType.VarChar;
                ParNombre_Buscado.Size = parInventario.Nombre_Buscado.Length;
                ParNombre_Buscado.Value = parInventario.Nombre_Buscado;
                SqlComando.Parameters.Add(ParNombre_Buscado);

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }

            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacenado Produccion.BuscarCategoria. " + ex.Message, ex);
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
