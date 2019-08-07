//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data.SqlClient;
//using System.Data;
//using System.Data.Common;
//using Dapper;

//namespace CapaDatos
//{
//    public class CDVenta : BDConexion
//    {
//        protected BDConexion bd = new BDConexion();

//        protected List<Procedimiento> procedimientos = new List<Procedimiento>
//        {
//            new Procedimiento
//            {
//                Nombre = "Insertar",

//            },
//            new Procedimiento
//            {
                
//            }
//        };

//        public CDVenta()
//        {
            
//        }

//        public bool InsertarVenta(Venta venta)
//        {
//            //var proc = procedimientos.First(x => x.Nombre == "Insertar");
//            var proc = new Procedimiento
//            {
//                Nombre = "Insertar",
//                Commando = "",
//                Parametros = new List<Parametro>
//                {
//                    new Parametro
//                    {
//                        Param = "@Total",
//                        Type = typeof(float),
//                        TipoDb = DbType.Decimal,
//                        Value = venta.Total
//                    },
//                    new Parametro
//                    {
//                        Param = "@FechaVenta",
//                        Type = typeof(float),
//                        TipoDb = DbType.DateTime,
//                        Value = venta.FechaVenta
//                    },
//                    new Parametro
//                    {
//                        Param = "@Iva",
//                        Type = typeof(float),
//                        TipoDb = DbType.Decimal,
//                        Value = venta.Iva
//                    },
//                    new Parametro
//                    {
//                        Param = "@Client_Id",
//                        Type = typeof(float),
//                        TipoDb = DbType.UInt32,
//                        Value = venta.Client_Id
//                    },
//                    new Parametro
//                    {
//                        Param = "@Vendedor_Id",
//                        Type = typeof(float),
//                        TipoDb = DbType.UInt32,
//                        Value = venta.Vendedor_Id
//                    }
//                }
//            };

//            using (var connection = GetOpenConnection())
//            {
//                try
//                {
//                    var parametros = ListofParametrosToDynamic(proc.Parametros);
//                    parametros.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
//                    var result = ExecuteStoredProcCommand(proc, parametros);
//                    int id = parametros.Get<int>("@Id");
//                    if (id>0)
//                    {
//                        foreach (Venta_Producto producto in venta.Productos)
//                        {
//                            proc = new Procedimiento
//                            {
//                                Nombre = "Insertar_Venta_Prod",
//                                Commando = "",
//                                Parametros = new List<Parametro>
//                                {
//                                    new Parametro
//                                    {
//                                        Param = "@VentaId",
//                                        TipoDb = DbType.UInt32,
//                                        Type = typeof(int),
//                                        Value = id
//                                    },
//                                    new Parametro
//                                    {
//                                        Param = "@ProductoId",
//                                        TipoDb = DbType.UInt32,
//                                        Type = typeof(int),
//                                        Value = producto.ProductoId
//                                    },
//                                    new Parametro
//                                    {
//                                        Param = "@Cantidad",
//                                        TipoDb = DbType.Int32,
//                                        Type = typeof(int),
//                                        Value = producto.Cantidad
//                                    },
//                                    new Parametro
//                                    {
//                                        Param = "@Total_Vendido",
//                                        TipoDb = DbType.Decimal,
//                                        Type = typeof(float),
//                                        Value = producto.TotalVendido
//                                    }
//                                }
//                            };
//                            //TODO: DELETE SALE IF THERE ISN'T ANY
//                            if(!ExecuteStoredProcCommand(proc, parametros)) return false;

//                        }
//                        return true;
//                    }
//                }
//                catch (Exception)
//                {
//                    // ignored
//                }
//                return false;

//            }
//        }

        

//        public Venta GetVenta(int id)
//        {
//            var venta = new Venta();
//            var proc = new Procedimiento
//            {
//                Nombre="Obtener",
//                Commando="",
//                Parametros = new List<Parametro>
//                {
//                    new Parametro
//                    {
//                        Param="@Id",
//                        TipoDb = DbType.Int32,
//                        Type = typeof(Int32),
//                        Value = id
//                    }
//                }
//            };

//            using (var connection = GetOpenConnection())
//            {
//                try
//                {
//                    venta = connection.Query<Venta>(proc.Commando, proc, commandType: CommandType.StoredProcedure).FirstOrDefault();
//                }
//                catch (Exception)
//                {
                    
//                }
//                return venta;
//            }
//        }
        
//    }
//}
