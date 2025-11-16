using Entity;
using MAPPER;
using System.Data.SqlClient;

namespace DAL
{
    public class PacienteDao
    {
        internal PacienteBE GetById(int v)
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
        
    }
}
