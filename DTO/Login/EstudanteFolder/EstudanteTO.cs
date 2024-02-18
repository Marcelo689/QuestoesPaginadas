using DTO.BancoClasses.Login.Entidades.EstudanteFolder;

namespace DTO.Login.EstudanteFolder
{
    public class EstudanteTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static explicit operator EstudanteTO(Estudante v)
        {
            if(v == null) return null;  
            return new EstudanteTO { Id = v.Id, Name = v.Name };
        }
    }
}
