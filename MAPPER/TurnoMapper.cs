using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPPER
{
    public class TurnoMapper
    {
        public static TurnoBE Map(SqlDataReader reader, PacienteBE paciente, NutricionistaBE nutricionista)
        {
            TurnoBE turnoBE = new TurnoBE
            {
                IdTurno = Convert.ToInt32(reader["ID_TURNO"]),
                Paciente = paciente,
                Nutricionista = nutricionista,
                FechaHora = Convert.ToDateTime(reader["FECHA_HORA"]),
                Estado = reader["Estado"].ToString(),
                Nota = reader[reader.GetOrdinal("NOTA")] == DBNull.Value ? null : reader["NOTA"].ToString()
            };
            return turnoBE;
        }
    }
}
