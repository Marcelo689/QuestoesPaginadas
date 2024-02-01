using DTO.Login.EstudanteFolder;
using System;

namespace DTO.BancoClasses.Login.Entidades.EstudanteFolder
{
    public class Estudante
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public Usuario Usuario { get; set;}

        public static explicit operator Estudante(EstudanteTO v)
        {
            throw new NotImplementedException();
        }
    }
}
