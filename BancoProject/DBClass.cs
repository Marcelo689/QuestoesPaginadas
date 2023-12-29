using DTO.BancoClasses.Login;
using DTO.BancoClasses.Login.Entidades.EstudanteFolder;
using DTO.BancoClasses.Login.Entidades.ProfessorFolder;
using DTO.BancoClasses.ProvaFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace Banco
{
    public class DBClass : DbContext
    {
        public DBClass()
        {
                
        }
        public DBClass(DbContextOptions<DBClass> options) : base(options)
        {
            
        }
        public DbSet<Professor>  Professor { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Estudante> Estudante { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Prova> Prova { get; set; }
        public DbSet<Questao> Questao { get; set; }
        public DbSet<QuestaoOpcao> QuestaoOpcao { get; set; }
        public DbSet<ProvaQuestaoResposta> ProvaQuestaoResposta { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Universidade;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
