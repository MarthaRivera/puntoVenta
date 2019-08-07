using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Procedimiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Commando { get; set; }
        public List<Parametro> Parametros { get; set; }

    }
}
