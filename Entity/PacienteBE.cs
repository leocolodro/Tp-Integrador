namespace Entity
{
    public class PacienteBE
    {
        public int IdPaciente { get; set; }

        public string Dni { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
