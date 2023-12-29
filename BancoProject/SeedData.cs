using Banco;
using DTO.BancoClasses.Login.Entidades.ProfessorFolder;

namespace BancoProject
{
    public static class SeedData
    {
        public static void Initialize()
        {
            using (var context = new DBClass())
            {
                if (context.Usuario.Any())
                {
                    return;   // DB has been seeded
                }
                context.Usuario.AddRange(
                    new DTO.BancoClasses.Login.Usuario
                    {
                        CreatedAccountDate = DateTime.Now,
                        Password = "123",
                        Username = "professor",
                    },
                    new DTO.BancoClasses.Login.Usuario
                    {
                        CreatedAccountDate = DateTime.Now,
                        Password = "123",
                        Username = "estudante",
                    }
                );
                context.SaveChanges();

                context.Estudante.Add(
                    new DTO.BancoClasses.Login.Entidades.EstudanteFolder.Estudante
                    {
                        Name = "Marcelo Estudante",
                        Usuario = context.Usuario.First(e => e.Id == 2),
                    }
                );

                context.Area.Add(new Area
                {
                    Name = "Matemática"
                });

                context.SaveChanges(true);

                context.Professor.Add(
                    new Professor
                    {
                        Usuario = context.Usuario.First(e => e.Id == 1),
                        Area = context.Area.First(e => e.Id == 1)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
