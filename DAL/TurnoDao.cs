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

        public void InsertTurno(TurnoBE turno)
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

        public void DeleteTurno(int idTurno)
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

        public void UpdateHorarioTurno(int idTurno, DateTime nuevoHorario)
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

        public void UpdateRealizarTurno(int idTurno, string nota)
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

        public void UpdateCancelarTurno(int idTurno)
        {
            try
            {
                using (SqlConnection cone = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    cone.Open();
                    using (SqlCommand comando = new SqlCommand("UPDATE TURNO SET ESTADO = 'CANCELADO' WHERE ID_TURNO = @IdTurno", cone))
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

        public void CambiarNutricionistaTurno(int idTurno, int idNuevoNutricionista)
        {
            try
            {
                using (SqlConnection cone = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    cone.Open();
                    using (SqlCommand comando = new SqlCommand("UPDATE TURNO SET ID_NUTRICIONISTA = @IdNutricionista WHERE ID_TURNO = @IdTurno", cone))
                    {
                        comando.Parameters.AddWithValue("@IdNutricionista", idNuevoNutricionista);
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


    }
}
