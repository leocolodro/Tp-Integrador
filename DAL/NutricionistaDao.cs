using Entity;
using MAPPER;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NutricionistaDao
    {
        public NutricionistaBE GetById(int v)
        {
			try
			{
				using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT ID_NUTRICIONISTA, MATRICULA, NOMBRE, APELLIDO FROM NUTRICIONISTA WHERE ID_NUTRICIONISTA = @IdNutricionista", conexion))
                    {
                        comando.Parameters.AddWithValue("@ID_NUTRICIONISTA", v);
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NutricionistaBE nutricionistaBE = new NutricionistaBE
                                {
                                    IdNutricionista = Convert.ToInt32(reader["ID_NUTRICIONISTA"]),
                                    Matricula = reader["MATRICULA"].ToString(),
                                    Nombre = reader["NOMBRE"].ToString(),
                                    Apellido = reader["APELLIDO"].ToString()
                                };
                                return nutricionistaBE;
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

        public List<NutricionistaBE> GetAll()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("SELECT ID_NUTRICIONISTA, MATRICULA, NOMBRE, APELLIDO FROM NUTRICIONISTA", conexion))
                    {
                        List<NutricionistaBE> nutricionistas = new List<NutricionistaBE>();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idNutricionista = Convert.ToInt32(reader["ID_NUTRICIONISTA"]);
                                NutricionistaBE nutri = NutricionistaMapper.Map(reader);
                                nutricionistas.Add(nutri);
                            }
                        }
                        return nutricionistas;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertNutri(NutricionistaBE nutricionista)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("INSERT INTO NUTRICIONISTA (MATRICULA, NOMBRE, APELLIDO) VALUES (@Matricula, @Nombre, @Apellido)", conexion))
                    {
                        comando.Parameters.AddWithValue("@Matricula", nutricionista.Matricula);
                        comando.Parameters.AddWithValue("@Nombre", nutricionista.Nombre);
                        comando.Parameters.AddWithValue("@Apellido", nutricionista.Apellido);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteNutri(int idNutricionista)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("DELETE FROM NUTRICIONISTA WHERE ID_NUTRICIONISTA = @IdNutricionista", conexion))
                    {
                        comando.Parameters.AddWithValue("@IdNutricionista", idNutricionista);
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateNutri(NutricionistaBE nutricionista)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConexionUtils.ObtenerCadenaConexion()))
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand("UPDATE NUTRICIONISTA SET MATRICULA = @Matricula, NOMBRE = @Nombre, APELLIDO = @Apellido WHERE ID_NUTRICIONISTA = @IdNutricionista", conexion))
                    {
                        comando.Parameters.AddWithValue("@Matricula", nutricionista.Matricula);
                        comando.Parameters.AddWithValue("@Nombre", nutricionista.Nombre);
                        comando.Parameters.AddWithValue("@Apellido", nutricionista.Apellido);
                        comando.Parameters.AddWithValue("@IdNutricionista", nutricionista.IdNutricionista);
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
