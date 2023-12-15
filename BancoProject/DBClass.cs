using BancoProject;
using Microsoft.EntityFrameworkCore;

namespace Banco
{
    public class DBClass : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamAnswerSheet> ExamAnswerSheet { get; set; }
        public DbSet<Question> Question { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=MyDataBase;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
