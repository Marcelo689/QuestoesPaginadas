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
                Questoes = new QuestaoTO[] {
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
                }
            };
            return prova;
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
