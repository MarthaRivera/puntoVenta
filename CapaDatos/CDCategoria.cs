using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDCategoria
    {
        public int Id_Categoria { get; set; }
        public String Nombre_Categoria { get; set; }
        public String NuevoNombre_Categoria { get; set; }
        public String Nombre_Buscado { get; set; }
        

        public CDCategoria()
        {

        }

        public CDCategoria(int parId_Categoria, String parNombre_Categoria, String parNuevoNombre_Categoria, String parNombre_Buscado)
        {
            this.Id_Categoria = parId_Categoria;
            this.Nombre_Categoria = parNombre_Categoria;
            this.NuevoNombre_Categoria = parNuevoNombre_Categoria;
            this.Nombre_Buscado = parNombre_Buscado;
        }

        public DataTable ListarCategoria()
        {
            DataTable TablaDatos = new DataTable("Productos.Categoria");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CategoriaListar";
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

        public DataTable ListarCategoriaCB()
        {
            DataTable TablaDatos = new DataTable("Productos.Categoria");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CategoriaListarCB";
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

        public DataTable Buscar(CDCategoria parCategorias)
        {
            DataTable TablaDatos = new DataTable("Productos.Categoria");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CategoriaBuscar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Buscado = new SqlParameter();
                ParNombre_Buscado.ParameterName = "@NombreBuscado";
                ParNombre_Buscado.SqlDbType = SqlDbType.VarChar;
                ParNombre_Buscado.Size = parCategorias.Nombre_Buscado.Length;
                ParNombre_Buscado.Value = parCategorias.Nombre_Buscado;
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

        public string Insertar(CDCategoria parCategorias)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CategoriaInsertar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Categoria = new SqlParameter();
                ParNombre_Categoria.ParameterName = "@NombreCategoria";
                ParNombre_Categoria.SqlDbType = SqlDbType.VarChar;
                ParNombre_Categoria.Size = parCategorias.Nombre_Categoria.Length;
                ParNombre_Categoria.Value = parCategorias.Nombre_Categoria;
                SqlComando.Parameters.Add(ParNombre_Categoria);

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
                    Respuesta = "Ya existe una categoría con ese Nombre.";
                }
                else if (ex.Number == 515)
                {
                    Respuesta = "No puedes dejar el campo Nombre vacío.";
                }
                else
                {
                    Respuesta = "Error al intentar insertar categoria" + ex.Message;
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

        public string Eliminar(CDCategoria parCategoria)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CategoriaEliminar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Categoria = new SqlParameter();
                ParNombre_Categoria.ParameterName = "@NombreCategoria";
                ParNombre_Categoria.SqlDbType = SqlDbType.VarChar;
                ParNombre_Categoria.Size = parCategoria.Nombre_Categoria.Length;
                ParNombre_Categoria.Value = parCategoria.Nombre_Categoria;
                SqlComando.Parameters.Add(ParNombre_Categoria);

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
                    Respuesta = "Error al intentar eliminar" + ex.Message;
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

        public string Editar(CDCategoria parCategoria)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CategoriaEditar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombre_Categoria = new SqlParameter();
                ParNombre_Categoria.ParameterName = "@NombreCategoria";
                ParNombre_Categoria.SqlDbType = SqlDbType.VarChar;
                ParNombre_Categoria.Size = parCategoria.Nombre_Categoria.Length;
                ParNombre_Categoria.Value = parCategoria.Nombre_Categoria;
                SqlComando.Parameters.Add(ParNombre_Categoria);

                SqlParameter ParNuevoNombre_Categoria = new SqlParameter();
                ParNuevoNombre_Categoria.ParameterName = "@NuevoNombreCategoria";
                ParNuevoNombre_Categoria.SqlDbType = SqlDbType.VarChar;
                ParNuevoNombre_Categoria.Size = parCategoria.NuevoNombre_Categoria.Length;
                ParNuevoNombre_Categoria.Value = parCategoria.NuevoNombre_Categoria;
                SqlComando.Parameters.Add(ParNuevoNombre_Categoria);

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
                    Respuesta = "Error al intentar eliminar" + ex.Message;
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
    }

}
