using DTO;
using Microsoft.AspNetCore.Mvc;

namespace ProvaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvaController : ControllerBase
    {
        [HttpGet]
        public ProvaTO Get()
        {
            var prova = new ProvaTO
            {
                Questoes = GetQuestoes()
            };
            return prova;

        }
        private static QuestaoTO[] GetQuestoes()
        {
            return new QuestaoTO[] {
                    new QuestaoTO( new
                    List<string>{
                        "Preto",
                        "Azul",
                        "Purpuro",
                        "Amarelo",
                        "Vermelho"
                    })
                    {
                        Id = 1,
                        Name = "Qual minha cor Favorita?",
                    },
                    new QuestaoTO( new
                    List<string>{
                        "10 reais",
                        "10 dolares",
                        "100 reais",
                        "30 dolares",
                        "400 euros"
                    })
                    {
                        Id = 2,
                        Name = "Quanto dinheiro tenho no meu bolso?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Cachorro akkakaskdks dfkdsfkkdfk dfjhghsdghjashdgf dfjfjkdjfjk",
                        "Gato",
                        "Peixe",
                        "Pássaro",
                        "Coelho"
                    })
                    {
                        Id = 3,
                        Name = "Qual é o melhor animal de estimação?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Ficção Científica",
                        "Fantasia",
                        "Suspense",
                        "Comédia",
                        "Drama"
                    })
                    {
                        Id = 4,
                        Name = "Qual é o seu gênero literário preferido?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Inverno",
                        "Verão",
                        "Outono",
                        "Primavera",
                        "Não tenho preferência"
                    })
                    {
                        Id = 5,
                        Name = "Qual é a sua estação do ano favorita?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Café",
                        "Chá",
                        "Suco",
                        "Refrigerante",
                        "Água"
                    })
                    {
                        Id = 6,
                        Name = "Qual é a sua bebida preferida?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Violão",
                        "Piano",
                        "Guitarra",
                        "Bateria",
                        "Saxofone"
                    })
                    {
                        Id = 7,
                        Name = "Se pudesse aprender a tocar um instrumento, qual seria?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Marvel",
                        "DC",
                        "Manga",
                        "Nenhuma das opções",
                        "Todas as opções"
                    })
                    {
                        Id = 8,
                        Name = "Você prefere quadrinhos americanos, mangás ou nenhum dos dois?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Montanhismo",
                        "Surfe",
                        "Ciclismo",
                        "Corrida",
                        "Yoga"
                    })
                    {
                        Id = 9,
                        Name = "Qual atividade física você gostaria de praticar?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Astronomia",
                        "Biologia",
                        "História",
                        "Matemática",
                        "Psicologia"
                    })
                    {
                        Id = 10,
                        Name = "Qual disciplina acadêmica é mais interessante para você?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Filme",
                        "Série",
                        "Livro",
                        "Podcast",
                        "Nenhum dos anteriores"
                    })
                    {
                        Id = 11,
                        Name = "Como você prefere consumir conteúdo?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Viajar para o passado",
                        "Viajar para o futuro",
                        "Ficar no presente",
                        "Não tenho preferência",
                        "Não gostaria de viajar no tempo"
                    })
                    {
                        Id = 12,
                        Name = "Se você pudesse viajar no tempo, para onde iria?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Comédia",
                        "Drama",
                        "Suspense",
                        "Ficção Científica",
                        "Ação"
                    })
                    {
                        Id = 13,
                        Name = "Qual é o seu gênero cinematográfico favorito?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Pizza",
                        "Hambúrguer",
                        "Sushi",
                        "Massa",
                        "Salada"
                    })
                    {
                        Id = 14,
                        Name = "Qual é o seu prato favorito?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Astronauta",
                        "Médico",
                        "Artista",
                        "Cientista",
                        "Atleta"
                    })
                    {
                        Id = 15,
                        Name = "Qual profissão você acha mais fascinante?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Sol",
                        "Chuva",
                        "Nevando",
                        "Vento",
                        "Nublado"
                    })
                    {
                        Id = 16,
                        Name = "Qual é o seu clima preferido?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Praia",
                        "Montanha",
                        "Cidade",
                        "Campo",
                        "Deserto"
                    })
                    {
                        Id = 17,
                        Name = "Qual tipo de ambiente você prefere?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Biblioteca",
                        "Parque",
                        "Cinema",
                        "Shopping",
                        "Cafeteria"
                    })
                    {
                        Id = 18,
                        Name = "Qual é o seu lugar favorito para relaxar?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Computador",
                        "Smartphone",
                        "Tablet",
                        "Nenhum dos anteriores",
                        "Todos os anteriores"
                    })
                    {
                        Id = 19,
                        Name = "Qual é o seu dispositivo eletrônico favorito?",
                    },
                    new QuestaoTO(new List<string>
                    {
                        "Verde",
                        "Laranja",
                        "Roxo",
                        "Marrom",
                        "Rosa"
                    })
                    {
                        Id = 20,
                        Name = "Qual é a sua cor favorita?",
                    },
                };
        }

        [HttpPost]
        public ProvaTO SaveAnswers(ProvaTO provaTO)
        {
            SalvaQuestoesPreenchidas(provaTO);
            return provaTO;
        }

        private void SalvaQuestoesPreenchidas(ProvaTO provaTO)
        {
            foreach (var questao in provaTO.Questoes)
            {
                bool opcaoPreenchida = questao.SelectedOption != 0;
                if (opcaoPreenchida)
                {
                    SalvarNoBanco(provaTO);
                }
            }
        }

        private string SalvarNoBanco(ProvaTO provaTO)
        {
            return "Nao faz nada, totalmente ficticio";
        }

        [HttpGet("Example")]
        public ExampleTO Example()
        {
            return new ExampleTO
            {
                Id = 1,
                Nome = "Marcelo",
            };
        }
    }
}
