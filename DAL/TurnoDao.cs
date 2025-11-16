using Entity;
using MAPPER;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TurnoDao
    {
        public List<TurnoBE> GetAll()
        {
			try
			{
				List<TurnoBE> turno = new List<TurnoBE>();
				var PacienteDao = new PacienteDao();
				var NutricionistaDao = new NutricionistaDao();
				using(SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
				{
					conexion.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT ID_TURNO, ID_PACIENTE, ID_NUTRICIONISTA, FECHA_HORA, ESTADO, NOTA FROM TURNO", conexion))
                    {
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idPaciente = Convert.ToInt32(reader["ID_PACIENTE"]);
                                int idNutricionista = Convert.ToInt32(reader["ID_NUTRICIONISTA"]);
                                PacienteBE paciente = PacienteDao.GetById(idPaciente);
                                NutricionistaBE nutricionista = NutricionistaDao.GetById(idNutricionista);
                                TurnoBE turnoBE = TurnoMapper.Map(reader, paciente, nutricionista);
                                turno.Add(turnoBE);
                            }
                        }
                    }
                }
               return turno;
            }
			catch (Exception)
			{
				throw;
			}
        }


    }
}
