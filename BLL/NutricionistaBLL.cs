using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BLL
{
    public class NutricionistaBLL
    {
        public void AgregarNutricionistaBLL(NutricionistaBE nutricionista)
        {
            if (nutricionista == null)
                throw new ArgumentNullException("El Nutricionista no puede ser nulo.");

            try
            {
                using (var trx = new TransactionScope())
                {
                    ValNombre(nutricionista.Nombre);
                    ValApellido(nutricionista.Apellido);
                    ValMatricula(nutricionista.Matricula);
                    NutricionistaDAO.AgregarNutricionista(nutricionista);
                    trx.Complete();
                }


            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void eliminaNutricionistaBLL(int Idnutri)
        {
            try
            {
                using (var trx = new TransactionScope())
                {
                    throw new ArgumentNullException("El ID del Nutricionista a Eliminar no puede ser nulo");
                    if (Idnutri < 0)
                        throw new ArgumentException("El ID del paciente no puede ser cero");
                    var nutricionista = NutricionistaDAO.GetById(Idnutri);
                    if (nutricionista == null)
                        throw new ArgumentNullException("El Nutricionista solicitado no Existe.");
                    NutricionistaDao.EliminarNutricionista(Idnutri);
                    trx.Complete();
                }

            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        
        //VALIDACIONES
        public void ValNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío");

            if (nombre.Trim().Length <= 2)
                throw new ArgumentException("El título debe tener más de 2 caracteres");
        }
        public void ValApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío");

            if (apellido.Trim().Length <= 2)
                throw new ArgumentException("El título debe tener más de 2 caracteres");

            if (apellido.Trim().Length > 25)
                throw new ArgumentException("El título debe tener menos de 25 caracteres");
        }
        public void ValMatricula(string matricula)
        {
            if (matricula.Length > 12 || matricula.Length < 12)
                throw new ArgumentException("La Matricula debe tener 12 digitos");
        }
    }
}
