using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDProductos
    {
        public int Id_Producto { get; set; }
        public String Nombre_Producto { get; set; }
        public String Nombre_Categoria { get; set; }
        public String Unidad { get; set; }
        public decimal Precio_Costo { get; set; }
        public decimal Ganancia { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio_Venta { get; set; }
        public String Nombre_buscado { get; set; }
        public object MessageBox { get; private set; }

        public CDProductos()
        {
        }

        public CDProductos (int parId_Producto, String parNombre_Producto, String parNombre_Catalogo, String parUnidad, decimal parPrecio_Costo, decimal parGanancia, decimal parCantidad, decimal parPrecio_Venta)
        {
            this.Id_Producto = parId_Producto;
            this.Nombre_Categoria = parNombre_Producto;
            this.Nombre_Categoria = parNombre_Catalogo;
            this.Unidad = parUnidad;
            this.Precio_Costo = parPrecio_Costo;
            this.Ganancia = parGanancia;
            this.Cantidad = parCantidad;
            this.Precio_Venta = parPrecio_Venta;
            
        }


        public DataTable ListarProductoVenta()
        {
            DataTable TablaDatos = new DataTable("Productos.V_Productos");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.ListarProducto";
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

        public DataTable Mostrar(int parRegistrosPorPagina, int parNumeroPagina)
        {
            DataTable TablaDatos = new DataTable("Productos.V_Productos");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.MostrarProductos";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParRegistrosPorPagina = new SqlParameter();
                ParRegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                ParRegistrosPorPagina.SqlDbType = SqlDbType.Int;
                ParRegistrosPorPagina.Value = parRegistrosPorPagina;
                SqlComando.Parameters.Add(ParRegistrosPorPagina);

                SqlParameter ParNumeroPagina = new SqlParameter();
                ParNumeroPagina.ParameterName = "@NumeroPagina";
                ParNumeroPagina.SqlDbType = SqlDbType.Int;
                ParNumeroPagina.Value = parNumeroPagina;
                SqlComando.Parameters.Add(ParNumeroPagina);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter AdaptadorDatos = new SqlDataAdapter(SqlComando);
                AdaptadorDatos.Fill(TablaDatos);

            }
            catch(Exception ex)
            {
                TablaDatos = null;
                //throw new  Exception("Error al intentar ejecutar el procedimiento almacendao Productos.MostrarProductos" + ex.Message);
            }
            finally

            {
                SqlConexion.Close();
            }

            return TablaDatos;
        }

        public int Tamaño(int parRegistrosPorPagina)
        {
            int totalPaginas = 1;
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.ProductosPaginas";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParRegistrosPorPagina = new SqlParameter();
                ParRegistrosPorPagina.ParameterName = "@RegistrosPorPagina";
                ParRegistrosPorPagina.SqlDbType = SqlDbType.Int;
                ParRegistrosPorPagina.Value = parRegistrosPorPagina;
                SqlComando.Parameters.Add(ParRegistrosPorPagina);

                SqlParameter ParTotalPaginas= new SqlParameter();
                ParTotalPaginas.ParameterName = "@TotalPaginas";
                ParTotalPaginas.Direction = ParameterDirection.Output;
                ParTotalPaginas.SqlDbType = SqlDbType.Int;
                SqlComando.Parameters.Add(ParTotalPaginas);

                SqlComando.ExecuteNonQuery();

                totalPaginas= (int)SqlComando.Parameters["@TotalPaginas"].Value;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar ejecutar el procedimiento almacendao Productos.ProductosPaginas" + ex.Message);
            }
            finally

            {
                SqlConexion.Close();
            }

            return totalPaginas;
        }

        public DataTable Buscar(CDProductos parProductos)
        {
            DataTable TablaDatos = new DataTable("Productos.V_Productos");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CatalogoBuscar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParNombreBuscar = new SqlParameter();
                ParNombreBuscar.ParameterName = "@NombreBuscado";
                ParNombreBuscar.SqlDbType = SqlDbType.VarChar;
                ParNombreBuscar.Size = parProductos.Nombre_buscado.Length;
                ParNombreBuscar.Value = parProductos.Nombre_buscado;
                SqlComando.Parameters.Add(ParNombreBuscar);

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter AdaptadorDatos = new SqlDataAdapter(SqlComando);
                AdaptadorDatos.Fill(TablaDatos);

            }
            catch (Exception ex)
            {
                TablaDatos = null;
                throw new Exception("Error al intentar ejecutar el procedimiento almacendao Productos.CatalogoBuscar" + ex.Message);
            }
            finally

            {
                SqlConexion.Close();
            }

            return TablaDatos;
        }

        //Procedimiento Almacenado Insertar Productos
        public string Insertar(CDProductos parProductos)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                //Procedimiento Almacenado CatalogoInsertar
                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CatalogoInsertar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                //Parametro Nombre Producto 
                SqlParameter ParNombreProducto = new SqlParameter();
                ParNombreProducto.ParameterName = "@NombreProducto";
                ParNombreProducto.SqlDbType = SqlDbType.VarChar;
                ParNombreProducto.Size = parProductos.Nombre_Producto.Length;
                ParNombreProducto.Value = parProductos.Nombre_Producto;
                SqlComando.Parameters.Add(ParNombreProducto);

                //Parametro Nombre Categoria
                SqlParameter ParNombreCategoria = new SqlParameter();
                ParNombreCategoria.ParameterName = "@NombreCategoria";
                ParNombreCategoria.SqlDbType = SqlDbType.VarChar;
                ParNombreCategoria.Size = parProductos.Nombre_Categoria.Length;
                ParNombreCategoria.Value = parProductos.Nombre_Categoria;
                SqlComando.Parameters.Add(ParNombreCategoria);

                //Parametro Unidad
                SqlParameter ParUnidad = new SqlParameter();
                ParUnidad.ParameterName = "@Unidad";
                ParUnidad.SqlDbType = SqlDbType.VarChar;
                ParUnidad.Size = parProductos.Unidad.Length;
                ParUnidad.Value = parProductos.Unidad;
                SqlComando.Parameters.Add(ParUnidad);

               //Parametro Precio Costo
               SqlParameter ParPrecioCosto = new SqlParameter();
                ParPrecioCosto.ParameterName = "@PrecioCosto";
                ParPrecioCosto.SqlDbType = SqlDbType.Money;
                ParPrecioCosto.Value = parProductos.Precio_Costo;
                SqlComando.Parameters.Add(ParPrecioCosto);

                //Parametro Ganancia
                SqlParameter ParGanancia = new SqlParameter();
                ParGanancia.ParameterName = "@Ganancia";
                ParGanancia.SqlDbType = SqlDbType.Decimal;
                ParGanancia.Value = parProductos.Ganancia;
                SqlComando.Parameters.Add(ParGanancia);

                //Paramentro Precio Venta
                SqlParameter ParPrecioVenta = new SqlParameter();
                ParPrecioVenta.ParameterName = "@PrecioVenta";
                ParPrecioVenta.SqlDbType = SqlDbType.Money;
                ParPrecioVenta.Value = parProductos.Precio_Venta;
                SqlComando.Parameters.Add(ParPrecioVenta);

                //Parametro Cantidad
                SqlParameter ParCantidad = new SqlParameter();
                ParCantidad.ParameterName = "@Cantidad";
                ParCantidad.SqlDbType = SqlDbType.Decimal;
                ParCantidad.Value = parProductos.Cantidad;
                SqlComando.Parameters.Add(ParCantidad);
      
                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";

            }
            catch (SqlException ex)
            {
                if(ex.Number==8152)//Este error se produce cuando el numero de caracteres es mayor que el admitido en la variable
                {
                    Respuesta = "";
                }
                else if(ex.Number==2627)//Restrincion nombre unico
                {
                    Respuesta = "Ya existe un registro con ese nombre";
                }
                else if(ex.Number==5015)//Restriccion campos nulos
                {
                    Respuesta = "Campos vacios";
                }
                else if(ex.Number==50000)
                {
                    Respuesta = "No existe Categoria";
                }
                else
                {
                    Respuesta = "A ocurrido un error "+ ex.Message;
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

        public string Eliminar(CDProductos parProductos)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CatalogoEliminar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlParameter ParId_Producto = new SqlParameter();
                ParId_Producto.ParameterName = "@Id_Producto";
                ParId_Producto.SqlDbType = SqlDbType.Int;
                ParId_Producto.Value = parProductos.Id_Producto;
                SqlComando.Parameters.Add(ParId_Producto);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";
            }

            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    Respuesta = "No puedes eliminar un producto presente en una venta";
                }

                else
                {
                    Respuesta = "Error al intentar ejecutar el procedimiento almacenado Produccion.EliminarProducto. " + ex.Message;
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

        public string Editar(CDProductos parProductos)
        {
            string Respuesta = "";
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                //Procedimiento Almacenado CatalogoInsertar
                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "Productos.CatalogoEditar";
                SqlComando.CommandType = CommandType.StoredProcedure;

                //Parametro Id producto
                SqlParameter ParId_Producto = new SqlParameter();
                ParId_Producto.ParameterName = "@Id_Producto";
                ParId_Producto.SqlDbType = SqlDbType.Int;
                ParId_Producto.Value = parProductos.Id_Producto;
                SqlComando.Parameters.Add(ParId_Producto);

                //Parametro Nombre Producto 
                SqlParameter ParNombreProducto = new SqlParameter();
                ParNombreProducto.ParameterName = "@NombreProducto";
                ParNombreProducto.SqlDbType = SqlDbType.VarChar;
                ParNombreProducto.Size = parProductos.Nombre_Producto.Length;
                ParNombreProducto.Value = parProductos.Nombre_Producto;
                SqlComando.Parameters.Add(ParNombreProducto);

                //Parametro Nombre Categoria
                SqlParameter ParNombreCategoria = new SqlParameter();
                ParNombreCategoria.ParameterName = "@NombreCategoria";
                ParNombreCategoria.SqlDbType = SqlDbType.VarChar;
                ParNombreCategoria.Size = parProductos.Nombre_Categoria.Length;
                ParNombreCategoria.Value = parProductos.Nombre_Categoria;
                SqlComando.Parameters.Add(ParNombreCategoria);

                //Parametro Unidad
                SqlParameter ParUnidad = new SqlParameter();
                ParUnidad.ParameterName = "@Unidad";
                ParUnidad.SqlDbType = SqlDbType.VarChar;
                ParUnidad.Size = parProductos.Unidad.Length;
                ParUnidad.Value = parProductos.Unidad;
                SqlComando.Parameters.Add(ParUnidad);

                //Parametro Precio Costo
                SqlParameter ParPrecioCosto = new SqlParameter();
                ParPrecioCosto.ParameterName = "@PrecioCosto";
                ParPrecioCosto.SqlDbType = SqlDbType.Money;
                ParPrecioCosto.Value = parProductos.Precio_Costo;
                SqlComando.Parameters.Add(ParPrecioCosto);

                //Parametro Ganancia
                SqlParameter ParGanancia = new SqlParameter();
                ParGanancia.ParameterName = "@Ganancia";
                ParGanancia.SqlDbType = SqlDbType.Decimal;
                ParGanancia.Value = parProductos.Ganancia;
                SqlComando.Parameters.Add(ParGanancia);

                //Paramentro Precio Venta
                SqlParameter ParPrecioVenta = new SqlParameter();
                ParPrecioVenta.ParameterName = "@PrecioVenta";
                ParPrecioVenta.SqlDbType = SqlDbType.Money;
                ParPrecioVenta.Value = parProductos.Precio_Venta;
                SqlComando.Parameters.Add(ParPrecioVenta);

                //Parametro Cantidad
                SqlParameter ParCantidad = new SqlParameter();
                ParCantidad.ParameterName = "@Cantidad";
                ParCantidad.SqlDbType = SqlDbType.Decimal;
                ParCantidad.Value = parProductos.Cantidad;
                SqlComando.Parameters.Add(ParCantidad);

                SqlComando.ExecuteNonQuery();
                Respuesta = "Y";

            }
            catch (SqlException ex)
            {
                if (ex.Number == 8152)//Este error se produce cuando el numero de caracteres es mayor que el admitido en la variable
                {
                    Respuesta = "";
                }
                else if (ex.Number == 2627)//Restrincion nombre unico
                {
                    Respuesta = "Ya existe un registro con ese nombre";
                }
                else if (ex.Number == 5015)//Restriccion campos nulos
                {
                    Respuesta = "Campos vacios";
                }
                else if (ex.Number == 50000)
                {
                    Respuesta = "No existe Categoria";
                }
                else
                {
                    Respuesta = "A ocurrido un error " + ex.Message;
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
