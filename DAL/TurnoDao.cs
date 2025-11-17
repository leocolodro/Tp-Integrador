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

        public void AgregarTurno(TurnoBE turno)
        {
            try
            {
                using(SqlConnection cone = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    cone.Open();
                    using (SqlCommand comando = new SqlCommand("INSERT INTO TURNO (ID_PACIENTE, ID_NUTRICIONISTA, FECHA_HORA, ESTADO, NOTA) VALUES (@Paciente, @Nutricionista, @FechaHora, @Estado, @Nota)", cone))
                    {
                        comando.Parameters.AddWithValue("@Paciente", turno.Paciente.IdPaciente);
                        comando.Parameters.AddWithValue("@Nutricionista", turno.Nutricionista.IdNutricionista);
                        comando.Parameters.AddWithValue("@FechaHora", turno.FechaHora);
                        comando.Parameters.AddWithValue("@Estado", turno.Estado);
                        comando.Parameters.AddWithValue("@Nota", turno.Nota ?? (object)DBNull.Value);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EliminarTurno(int idTurno)
        {
            try
            {
                using (SqlConnection cone = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    cone.Open();
                    using (SqlCommand comando = new SqlCommand("DELETE FROM TURNO WHERE ID_TURNO = @IdTurno", cone))
                    {
                        comando.Parameters.AddWithValue("@IdTurno", idTurno);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModificarHorarioBLL(int idTurno, DateTime nuevoHorario)
        {
            try
            {
                using (SqlConnection cone = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    cone.Open();
                    using (SqlCommand comando = new SqlCommand("UPDATE TURNO SET FECHA_HORA = @NuevoHorario WHERE ID_TURNO = @IdTurno", cone))
                    {
                        comando.Parameters.AddWithValue("@NuevoHorario", nuevoHorario);
                        comando.Parameters.AddWithValue("@IdTurno", idTurno);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RealizarTurno(int idTurno, string nota)
        {
            try
            {
                using (SqlConnection cone = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    cone.Open();
                    using (SqlCommand comando = new SqlCommand("UPDATE TURNO SET ESTADO = 'REALIZADO', NOTA = @Nota WHERE ID_TURNO = @IdTurno", cone))
                    {
                        comando.Parameters.AddWithValue("@Nota", nota);
                        comando.Parameters.AddWithValue("@IdTurno", idTurno);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetById(int id)
        {
            try
            {
                using(SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT ID_TURNO, ID_PACIENTE, ID_NUTRICIONISTA, FECHA_HORA, ESTADO, NOTA FROM PACIENTE WHERE ID_PACIENTE = @IdPaciente", conexion))
                    {
                        comando.Parameters.AddWithValue("@ID_PACIENTE", id);
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TurnoBE pacienteBE = new TurnoBE
                                {
                                    IdTurno = Convert.ToInt32(reader["ID_TURNO"]),
                                    Paciente = reader["ID_PACIENTE"] as PacienteBE,
                                    Nutricionista = reader["ID_NUTRICIONISTA"] as NutricionistaBE,
                                    FechaHora = Convert.ToDateTime(reader["FECHA_HORA"]),
                                    Estado = reader["ESTADO"].ToString(),
                                    Nota = reader[reader.GetOrdinal("NOTA")] == DBNull.Value ? null : reader["NOTA"].ToString()


                                };
                                return pacienteBE;
                            }
                            else
                            {
                                return null; // o lanzar una excepción si no se encuentra
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
