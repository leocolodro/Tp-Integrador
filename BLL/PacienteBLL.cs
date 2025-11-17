using DAL;
using Entity;
using System.Transactions;

namespace BLL
{
    public class PacienteBLL
    {
        private PacienteDao PacienteDao = new PacienteDao();
        public void AgregarPacienteBLL(PacienteBE paciente)
        {
            if (paciente == null)
                throw new ArgumentNullException("El Paciente no puede ser nulo.");

            try
            {
                using (var trx = new TransactionScope())
                {
                    ValNombre(paciente.Nombre);
                    ValApellido(paciente.Apellido);
                    ValFechadeNac(paciente.FechaNacimiento);
                    ValMail(paciente.Email);
                    ValTelefono(paciente.Telefono);
                    ValDNI(paciente.Dni);
                    PacienteDao.AgregarPaciente(paciente);
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
        public void eliminarPacienteBLL(int Idpaciente)
        {
            try
            {
                using (var trx = new TransactionScope())
                {
                    throw new ArgumentNullException("El ID del Paciente a Eliminar no puede ser nulo");
                    if (Idpaciente < 0)
                        throw new ArgumentException("El ID del paciente no puede ser cero");
                    var paciente = PacienteDao.GetById(Idpaciente);
                    if (paciente == null)
                        throw new ArgumentNullException("El Paciente solicitado no Existe.");
                    PacienteDao.EliminarPaciente(Idpaciente);
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
                throw new ArgumentException("El nombre debe tener más de 2 caracteres");
        }
        public void ValApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío");

            if (apellido.Trim().Length <= 2)
                throw new ArgumentException("El apellido debe tener más de 2 caracteres");

            if (apellido.Trim().Length > 25)
                throw new ArgumentException("El apellido debe tener menos de 25 caracteres");
        }
        public void ValFechadeNac(DateTime naci)
        {
            if (naci == null)
                throw new ArgumentNullException("La edad no puede ser nula");
            if (naci.Year < 1915)
                throw new ArgumentException("La Fecha de Nacimiento no puede superar los 110 años.");

        }
        public void ValMail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                throw new ArgumentException("el mail no puede ser nulo");
            if (mail.Length > 25)
                throw new ArgumentException("el mail no puede tener más de 25 caracteres.");
        }
        public void ValTelefono(string telefono)
        {
            if (telefono.Length<=0)
                throw new ArgumentException("El telefono no puede ser cero.");
            if (telefono.Length >= 15)
                throw new ArgumentException("El telefono no puede tener más de 15 caracteres.");
        }
        public void ValDNI(string docu) { 
            if(docu.Length==0)
                throw new ArgumentException("El DNI no puede ser cero.");
            if (docu.Length > 9)
                throw new ArgumentException("El DNI no puede tener más de 9 Caracteres.");
        }
            
          
    }
}
