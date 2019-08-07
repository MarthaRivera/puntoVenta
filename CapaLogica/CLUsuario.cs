using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class CLUsuario
    {
        public static Empleado CurrentUser { get; set; }
        public BDPuntoVentaEntities dbcontext;

        public CLUsuario()
        {
            dbcontext = new BDPuntoVentaEntities();
        }
        public static DataTable ListarUsuarioCB()
        {
            return new CDUsuario().ListarUsuarioCB();
        }

        public bool ValidarUsuario(string username, string password)
        {

            var anyUser = dbcontext.Empleados.Any(x => x.Usuario == username);
            if (anyUser)
            {
                var user = dbcontext.Empleados.First(x => x.Usuario == username);
                if (PasswordHash.ValidatePassword(password, user.Contraseña))
                {
                    CurrentUser = user;
                    return true;
                }
            }
            return false;
        }


        public bool CrearEmpleado(Empleado empleado)
        {
            if (dbcontext.Empleados.Any(x => x.Usuario == empleado.Usuario) 
                || dbcontext.Empleados.Any(x=> x.Nombre_Completo == empleado.Nombre_Completo))
            return false;
            string password = empleado.Contraseña+"";
            empleado.Contraseña = PasswordHash.CreateHash(password);
            dbcontext.Empleados.Add(empleado);
            if(dbcontext.SaveChanges()>0)return true;
            return false;
        }

        public bool CreateDefaultUser()
        {
            var user = new Empleado
            {
                Usuario = "admin",
                Nombre_Completo = "Administrador",
                Contraseña = "12345"
            };
            return CrearEmpleado(user);
        }
    }
}
