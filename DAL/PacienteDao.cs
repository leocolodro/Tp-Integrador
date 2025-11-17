using Entity;
using MAPPER;
using System.Data.SqlClient;

namespace DAL
{
    public class PacienteDao
    {
       public PacienteBE GetById(int v)
        {
			try
			{
				using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT ID_PACIENTE, NOMBRE, APELLIDO, DNI, FECHA_NACIMIENTO, EMAIL, TELEFONO FROM PACIENTE WHERE ID_PACIENTE = @IdPaciente", conexion))
                    {
                        comando.Parameters.AddWithValue("@ID_PACIENTE", v);
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                PacienteBE pacienteBE = new PacienteBE
                                {
                                    IdPaciente = Convert.ToInt32(reader["ID_PACIENTE"]),
                                    Nombre = reader["NOMBRE"].ToString(),
                                    Apellido = reader["APELLIDO"].ToString(),
                                    Dni = reader["DNI"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(reader["FECHA_NACIMIENTO"]),
                                    Email = reader["EMAIL"].ToString(),
                                    Telefono = reader["TELEFONO"].ToString()

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

        public List<PacienteBE> GetAll()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT ID_PACIENTE, NOMBRE, APELLIDO, DNI, FECHA_NACIMIENTO, EMAIL, TELEFONO FROM PACIENTE", conexion))
                    {
                        List<PacienteBE> pacientes = new List<PacienteBE>();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               int idPaciente = Convert.ToInt32(reader["ID_PACIENTE"]);
                               PacienteBE paciente = PacienteMapper.Map(reader);
                               pacientes.Add(paciente);
                            }
                        }
                        return pacientes;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public void AgregarPaciente(PacienteBE paciente)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("INSERT INTO PACIENTE (NOMBRE, APELLIDO, DNI, FECHA_NACIMIENTO, EMAIL, TELEFONO) VALUES (@Nombre, @Apellido, @Dni, @FechaNacimiento, @Email, @Telefono)", conexion))
                    {
                        comando.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                        comando.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                        comando.Parameters.AddWithValue("@Dni", paciente.Dni);
                        comando.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
                        comando.Parameters.AddWithValue("@Email", paciente.Email);
                        comando.Parameters.AddWithValue("@Telefono", paciente.Telefono);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       public void EliminarPaciente(int idPaciente)
       {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("DELETE FROM PACIENTE WHERE ID_PACIENTE = @IdPaciente", conexion))
                    {
                        comando.Parameters.AddWithValue("@IdPaciente", idPaciente);
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
