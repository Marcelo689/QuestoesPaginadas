using DTO.BancoClasses.Login.Entidades.EstudanteFolder;
using DTO.BancoClasses.Login.Entidades.ProfessorFolder;

namespace DTO.BancoClasses.ProvaFolder
{
    public class Prova
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public Professor Professor { get; set; }
        public Estudante Estudante { get; set; }

        public static explicit operator Prova(ProvaTO v)
        {
            return new Prova()
            {
                Name = v.Name,
                Estudante = (Estudante)v.Estudante,
            };
        }
    }
}
