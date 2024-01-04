using BancoProject.Login;
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

        [HttpPost("CriarProva")]
        public ProvaTO CriarProva(ProvaTO provaTO)
        {
            return ProvaDB.CriarProva(provaTO);
        }

        [HttpGet("EditarProva")]
        public ProvaTO GetProvaById(int provaId)
        {
            ProvaTO provaTO = ProvaDB.GetProvaById(provaId);

            return provaTO;
        }
        [HttpGet("ListProvas")]
        public List<ProvaTO> GetListProva()
        {
            return ProvaDB.GetAllProvas();
        }

        [HttpPost("SaveAnswers")]
        public ProvaTO SaveAnswers(ProvaTO? provaTO)
        {
            SalvarQuestoesPreenchidas(provaTO);
            return provaTO;
        }

        [HttpPost("EditAnswers")]
        public ProvaTO EditAnswers(ProvaTO? provaTO)
        {
            EditarQuestoesPreenchidas(provaTO);
            return provaTO;
        }

        private void EditarQuestoesPreenchidas(ProvaTO? provaTO)
        {
            try
            {
                EditarQuestoesPreenchidasBanco(provaTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditarQuestoesPreenchidasBanco(ProvaTO? provaTO)
        {
            List<QuestaoTO> questoesTO = provaTO.Questoes.ToList();

            foreach (QuestaoTO questaoTO in questoesTO) 
            {
               ProvaDB.UpdateQuestaoFromTO(questaoTO);
            }
        }

        private void SalvarQuestoesPreenchidas(ProvaTO provaTO)
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
