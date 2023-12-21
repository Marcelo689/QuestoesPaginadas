using DTO.BancoClasses.Login;
using DTO.BancoClasses.Login.Entidades.EstudanteFolder;
using DTO.BancoClasses.Login.Entidades.ProfessorFolder;
using DTO.BancoClasses.ProvaFolder;
using Microsoft.EntityFrameworkCore;

namespace Banco
{
    public class DBClass : DbContext
    {

        public DbSet<Professor>  Professor{ get; set; }
        public DbSet<Estudante> Estudante{ get; set; }
        public DbSet<Usuario> Usuario{ get; set; }
        public DbSet<Prova> Prova { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<QuestaoOpcao> QuestaoOpcao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=Universidade;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
