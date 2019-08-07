using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Configuration;
using Dapper;

namespace CapaDatos
{
    public class BDConexion
    {
        //public static String ConexionBDEmpresa = "Data Source = SUSANA; Initial Catalog = BDPuntoVenta; Integrated Security = SSPI;";
        //public static String ConexionBdEmpresa =
        //    "Data Source=ASUSU56E-SSD\\SQLEXPRESS2014;Initial Catalog = BDPuntoVenta; Persist Security Info=True;User ID = visual_studio; Password=mizuki1";
        public static String ConexionBdEmpresa = ConfigurationManager.ConnectionStrings["BDPuntoVenta"].ConnectionString;
        //public static String ConexionBDMaster = "Data Source = SUSANA; Initial Catalog = master; Integrated Security = SSPI;";
        //public static String ConexionBdMaster = "Data Source=ASUSU56E-SSD\\SQLEXPRESS2014;Initial Catalog = master; Persist Security Info=True;User ID = visual_studio; Password=mizuki1";
        //Crear metodo Comprobar conexion

        protected SqlConnection Conexion = new SqlConnection();

        public BDConexion()
        {
            Conexion.ConnectionString = BDConexion.ConexionBdEmpresa;
        }


        /// <summary>
        /// Magia
        /// </summary>
        /// <returns></returns>
        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConexionBdEmpresa);
            connection.Open();

            return connection;
        }

        protected int ExecuteStoredProcedure(string command, List<SqlParameter> parameters)
        {
            using (var connection = GetOpenConnection())
            {
                var res = -1;
                var p = new DynamicParameters();
                try
                {
                    Conexion.Open();
                    using (SqlCommand comando = new SqlCommand())
                    {
                        connection.Execute("");
                        comando.Connection = Conexion;
                        comando.CommandText = command;
                        comando.CommandType = System.Data.CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            comando.Parameters.AddRange(parameters.ToArray());
                        }
                        res = comando.ExecuteNonQuery();
                    }
                }
                finally
                {
                    Conexion.Close();
                }
                return res;
            }
        }

        protected static DynamicParameters ListofParametrosToDynamic(List<Parametro> parametros)
        {
            var p = new DynamicParameters();
            foreach (var parametro in parametros)
            {
                p.Add(parametro.Param, parametro.Value, parametro.TipoDb);
            }
            return p;
        }
        

        protected static SqlParameter GenerateParameter(string parameter, Object value)
        {
            return new SqlParameter(parameter, value);
        }

        protected static bool ExecuteStoredProcCommand(Procedimiento proc, DynamicParameters paramentros)
        {
            try
            {
                using (var connection = GetOpenConnection())
                {
                    var result = connection.Execute(proc.Commando, paramentros, commandType: CommandType.StoredProcedure);
                    if (result == 1) return true;
                }
                
            }
            catch (Exception e)
            {
                // ignored
            }
            return false;
            
        }



    }
}
