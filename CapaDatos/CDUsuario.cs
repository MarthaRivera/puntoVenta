using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CDUsuario
    {
        public int Id_Empleado{ get; set; }
        public String  Usuario { get; set; }
        public String Contraseña { get; set; }
        public String Nombre_Completo{ get; set; }

        public CDUsuario()
        {

        }

        public CDUsuario(int parId_Empleado, String parUsuario, String parContraseña, String parNombre_Completo)
        {
            this.Id_Empleado = parId_Empleado;
            this.Usuario = parUsuario;
            this.Contraseña = parContraseña;
            this.Nombre_Completo = parNombre_Completo;
        }

        public DataTable ListarUsuarioCB()
        {
            DataTable TablaDatos = new DataTable("RecursosHumanos.Empleados");
            SqlConnection SqlConexion = new SqlConnection();

            try
            {
                SqlConexion.ConnectionString = BDConexion.ConexionBdEmpresa;
                SqlConexion.Open();

                SqlCommand SqlComando = new SqlCommand();
                SqlComando.Connection = SqlConexion;
                SqlComando.CommandText = "RecursosHumanos.ListarUsuario";
                SqlComando.CommandType = CommandType.StoredProcedure;

                SqlComando.ExecuteNonQuery();

                SqlDataAdapter SqlAdaptadorDatos = new SqlDataAdapter(SqlComando);
                SqlAdaptadorDatos.Fill(TablaDatos);
            }
            catch
            {
                TablaDatos = null;
                //throw new Exception("Error al Cargar Datos");
            }
            finally
            {
                SqlConexion.Close();
            }
            return TablaDatos;
        }
    }
}
