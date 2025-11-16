using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPPER
{
    public class NutricionistaMapper
    {
        public static NutricionistaBE Map(SqlDataReader reader)
        {
            NutricionistaBE nutricionistaBE = new NutricionistaBE
            {
                IdNutricionista = Convert.ToInt32(reader["ID_NUTRICIONISTA"]),
                Matricula = reader["MATRICULA"].ToString(),
                Nombre = reader["NOMBRE"].ToString(),
                Apellido = reader["APELLIDO"].ToString(),
            };
            return nutricionistaBE;
        }
    }
}
