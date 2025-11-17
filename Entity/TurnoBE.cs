using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class TurnoBE
    {
        public int IDTurno {get;set;}
        public DateTime FechaHora{get;set;}
        public string Detalle {get;set;}
        public string Estado {get;set;}
        public PacienteBE Paciente {get;set;}
        public NutricionistaBE Nutricionista {get;set;}
    }
}
