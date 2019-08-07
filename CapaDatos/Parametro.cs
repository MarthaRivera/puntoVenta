using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Parametro
    {
        private string _value;

        public Parametro()
        {
            Type = "".GetType();
        }
        
        public string Param { get; set; }

        public DbType TipoDb { get; set; }

        public Type Type { get; set; }

        public dynamic Value
        {
            get
            {
                return Convert.ChangeType(_value, Type);
            }
            set { _value = Convert.ToString(value); }
        }
    }
}
