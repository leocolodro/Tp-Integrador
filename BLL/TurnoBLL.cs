using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BLL
{
    public class TurnoBLL
    {
        private TurnoDao TurnoDAO = new TurnoDao();
        public void AgregarTurnoBLL(TurnoBE turno)
        {
            if (turno == null)
                throw new ArgumentNullException("El Turno no puede ser nulo.");

            try
            {
                using (var trx = new TransactionScope())
                {
                    
                    ValHorario(turno.FechaHora);
                    ValEstado(turno.Estado);
                    ValPaciente(turno.Paciente);
                    TurnoDAO.AgregarTurno(turno);
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
        public void CancelarTurnoBLL(int IdTurno)
        {
            try
            {
                using (var trx = new TransactionScope())
                {
                    
                    if (IdTurno < 0)
                        throw new ArgumentException("El ID del Turno no puede ser cero");
                    var turno = TurnoDAO.GetById(IdTurno);
                    if (turno == null)
                        throw new ArgumentNullException("El Turno solicitado no Existe.");
                    TurnoDAO.EliminarTurno(IdTurno);
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
        public void ModificarHorarioBLL(int idturno, DateTime nuevacita)
        {
            try
            {
                using (var trx = new TransactionScope())
                {
                    if (idturno <= 0)
                        throw new ArgumentException("ID de turno inválido");

                    ValHorario(nuevacita);

                    var turno = TurnoDAO.GetById(idturno);
                    if (turno == null)
                        throw new ArgumentException("El turno no existe");

                    TurnoDAO.ModificarHorarioBLL(idturno, nuevacita);
                    trx.Complete();
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar horario: " + ex.Message);
            }
        }
        
        public void RealizarTurnoBLL(int idturno,string detalle)
        {
            try
            {
                using (var trx = new TransactionScope())
                {
                    if (idturno <= 0)
                        throw new ArgumentException("ID de turno inválido");

                    ValDetalle(detalle);

                    var turno = TurnoDAO.GetById(idturno);
                    if (turno == null)
                        throw new ArgumentException("El turno no existe");

                    TurnoDAO.RealizarTurno(idturno, detalle);
                    trx.Complete();
                }
            }
            catch (ArgumentException)
            {
                throw;
            }
            
        }
       
        //VALIDACIONES
        public void ValEstado(string estado)
        {
            try
            {
                if (!estado.Equals("Completado") || !estado.Equals("Pendiente"))
                    throw new ArgumentException("El detalle solo puede ser PENDIENTE o COMPLETO");          
            }
            catch (Exception ex) { 
            }
        }
        public void ValDetalle(string detalle)
        {
            if (detalle.Length > 200)
                throw new ArgumentException("El detalle no puede superar los 200 caracteres");

        }
        public void ValHorario(DateTime horario)
        {
            try
            {
                DateTime diaActual = DateTime.Now;
                if (horario < diaActual)
                    throw new ArgumentException("No se puede cargar un Horario anterior a este momento");
            }
            catch (Exception ex) { }
        }
       
        public void ValPaciente(PacienteBE paciente)
        {
            if (paciente.IdPaciente== 0)
                throw new ArgumentException("El paciente ingresado no puede ser cero)");
            var paci = TurnoDAO.GetById(paciente.IdPaciente);
            if (paci == null)
                throw new ArgumentNullException("El paciente no existe.");
        }
    }
    }


