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
        public ProvaTO GetProvaDB(int provaId, bool isLogin = false)
        {
            return ProvaDB.GetProvaDB(provaId);
        }

        [HttpPost("CriarProva")]
        public ProvaTO CriarProva(ProvaTO provaTO)
        {
            return ProvaDB.CriarProva(provaTO);
        }

        [HttpPost("DeletarQuestao")]
        public void DeletarQuestao(QuestaoTO questaoTO)
        {
            ProvaDB.DeletarQuestao(questaoTO.Id);
        }

        [HttpGet("EditarProva")]
        public ProvaTO GetProvaById(int provaId)
        {
            try
            {
                ProvaTO provaTO = ProvaDB.GetProvaById(provaId);
                return provaTO;
            }catch (Exception ex) {
                return new ProvaTO() {  };
            }
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
                ProvaDB.SaveChanges();  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void EditarQuestoesPreenchidasBanco(ProvaTO? provaTO)
        {
            try
            {

                List<QuestaoTO> questoesTO = provaTO.Questoes.ToList();

                foreach (QuestaoTO questaoTO in questoesTO) 
                {
                   ProvaDB.UpdateQuestaoFromTO(questaoTO, provaTO.Id);
                }
            }catch (Exception ex) { }
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
            try
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
            }catch (Exception ex) { }
        }

        private void SalvarNoBanco(ProvaTO provaTO, QuestaoTO questaoTO)
        {
            try
            {
                ProvaDB.SalvarQuestoes(provaTO, questaoTO);
            }catch (Exception ex) { }
        }

    }
}
