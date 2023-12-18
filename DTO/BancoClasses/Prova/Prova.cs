using DTO.BancoClasses.Login.Entidades.ProfessorFolder;

namespace DTO.BancoClasses.Prova
{
    public class Prova
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public Professor Professor { get; set; }
    }
}
