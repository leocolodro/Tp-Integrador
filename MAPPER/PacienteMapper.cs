using Entity;
using System.Data.SqlClient;

namespace MAPPER
{
    public class PacienteMapper
    {
        public static PacienteBE Map(SqlDataReader reader)
        {
            PacienteBE pacienteBE = new PacienteBE
            {
                IdPaciente = Convert.ToInt32(reader["ID_PACIENTE"]),
                Nombre = reader["NOMBRE"].ToString(),
                Apellido = reader["APELLIDO"].ToString(),
                Dni = reader["DNI"].ToString(),
                FechaNacimiento = Convert.ToDateTime(reader["FECHA_NACIMIENTO"]),
                Email = reader["EMAIL"].ToString(),
                Telefono = reader["TELEFONO"].ToString(),
                
            };
            return pacienteBE;
        }
    }
}
