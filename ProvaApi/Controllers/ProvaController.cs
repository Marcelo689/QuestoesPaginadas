using BancoProject.ProvaFolder;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace ProvaApi.Controllers
{
    [Route("[controller]Api")]
    [ApiController]
    public class ProvaController : ControllerBase
    {
        [HttpGet("GetProva")]
        public ProvaTO GetProvaDB()
        {
            return ProvaDB.GetProvaDB(1);
        }

        [HttpGet("ListProvas")]
        public List<ProvaTO> GetListProva()
        {
            return ProvaDB.GetAllProvas();
        }

        [HttpPost("SaveAnswers")]
        public ProvaTO SaveAnswers(ProvaTO? provaTO)
        {
            SalvaQuestoesPreenchidas(provaTO);
            return provaTO;
        }

        private void SalvaQuestoesPreenchidas(ProvaTO provaTO)
        {
            try
            {
                SalvarQuestoesPreenchidasBanco(provaTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SalvarQuestoesPreenchidasBanco(ProvaTO provaTO)
        {
            foreach (var questaoTO in provaTO.Questoes)
            {
                bool opcaoPreenchida = questaoTO.SelectedOption != 0;
                if (opcaoPreenchida)
                {
                    SalvarNoBanco(provaTO, questaoTO);
                }
            }
            ProvaDB.SaveChanges();
        }

        private void SalvarNoBanco(ProvaTO provaTO, QuestaoTO questaoTO)
        {
            ProvaDB.SalvarQuestoes(provaTO, questaoTO);
        }

    }
}
