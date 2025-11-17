using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConexionUtils
    {
        public static string ObtenerCadenaConexion()
        {
            
            return ConfigurationManager.ConnectionStrings["NutriUai"].ConnectionString;
        }
    }
}
