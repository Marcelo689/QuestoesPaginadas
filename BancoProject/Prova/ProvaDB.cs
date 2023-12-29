using BancoProject.Login;
using DTO;
using DTO.BancoClasses.Login.Entidades.EstudanteFolder;
using DTO.BancoClasses.ProvaFolder;
using DTO.Login.EstudanteFolder;
using Microsoft.EntityFrameworkCore;

namespace BancoProject.ProvaFolder
{
    public static class ProvaDB
    {
        public static ProvaTO GetProvaDB(int id) { 
            ProvaTO provaTO = PreencheProvaTO(id);
            return provaTO;
        }

        private static ProvaTO PreencheProvaTO(int provaId)
        {
            Prova provaDB = DBInstance.DB.Prova.Include(e => e.Professor.Usuario).FirstOrDefault(e => e.Id == provaId);
            Estudante estudante = DBInstance.DB.Estudante.ToList().FirstOrDefault(e => e.Id == provaDB.Estudante.Id);
            IQueryable<Questao> questoes = DBInstance.DB.Questao.Where(e => e.Prova.Id == provaId);
            List<int> questoesIds = questoes.Select(e => e.Id).ToList();
            IQueryable<ProvaQuestaoResposta> questaoRespondidas = DBInstance.DB.ProvaQuestaoResposta.Where(e => e.QuestaoOpcao.Opcao != 0 && e.Prova.Id == provaId && e.Estudante.Id == estudante.Id);
            var listaTO = PreencherOpcoesSelecionadas(questoes.ToList(), questoesIds, questaoRespondidas);

            var provaTO = (ProvaTO) provaDB;
            provaTO.Questoes = listaTO.ToArray();
            provaTO.Estudante = (EstudanteTO) estudante;
            return provaTO;
        }

        private static List<QuestaoTO> PreencherOpcoesSelecionadas(List<Questao> questoes, List<int> questoesIds, IQueryable<ProvaQuestaoResposta> questaoRespondidas)
        {
            foreach (var questao in questoes)
            {
                if (questoesIds.Contains(questao.Id))
                {
                    var algo = questaoRespondidas.Include(e => e.QuestaoOpcao).Where(e => e.Questao.Id == questao.Id).ToList();
                    var opcao = algo.Select(e => e.QuestaoOpcao).FirstOrDefault();
                    bool opcaoExiste = opcao != null;
                    if (opcaoExiste)
                    {
                        questao.OpcaoSelecionada = DBInstance.DB.QuestaoOpcao.FirstOrDefault( e => e.Opcao == opcao.Opcao);// error 
                    }
                    else
                    {
                        questao.OpcaoSelecionada = DBInstance.DB.QuestaoOpcao.FirstOrDefault(e => e.Id == 1);
                    }
                }
            }

            return questoes.Select(q => (QuestaoTO) q).ToList();
        }

        public static void SaveChanges()
        {
            DBInstance.DB.SaveChanges();
        }

        public static void SalvarQuestoes(ProvaTO provaTO, QuestaoTO questaoTO)
        {
            Questao questao = DBInstance.DB.Questao.FirstOrDefault(q => q.Id == questaoTO.Id);
            var num1 = GetSelectedOption(questaoTO);
            QuestaoOpcao opcaoSelecionada = DBInstance.DB.QuestaoOpcao.FirstOrDefault(qo => ((int)qo.Opcao) == num1);

            Prova prova = DBInstance.DB.Prova.FirstOrDefault(e => e.Id == provaTO.Id);
            Estudante estudante = DBInstance.DB.Estudante.FirstOrDefault(e => e.Usuario.Id == provaTO.Usuario.Id);

            ProvaQuestaoResposta? provaQuestaoResposta = DBInstance.DB.ProvaQuestaoResposta.FirstOrDefault(e => e.Questao.Id == questaoTO.Id && e.Prova.Id == provaTO.Id);

            if(provaQuestaoResposta == null)
            {
                provaQuestaoResposta = new ProvaQuestaoResposta
                {
                    DataRespondida = DateTime.Now,
                    Questao = questao,
                    Estudante = estudante,
                    QuestaoOpcao = opcaoSelecionada,
                    Prova = prova
                };
                DBInstance.DB.ProvaQuestaoResposta.Add(provaQuestaoResposta);
            }
            else
            {
                provaQuestaoResposta.DataRespondida = DateTime.Now;
                provaQuestaoResposta.Prova = prova;
                provaQuestaoResposta.Questao = questao;
                provaQuestaoResposta.Estudante = estudante;
                provaQuestaoResposta.QuestaoOpcao = opcaoSelecionada;
            }

            DBInstance.DB.SaveChanges();
        }

        private static int GetSelectedOption(QuestaoTO questaoTO)
        {
            if(questaoTO.SelectedOption == null)
            {
                return 0;
            }

            return (int)questaoTO.SelectedOption;
        }

        public static List<ProvaTO> GetAllProvas()
        {
            IQueryable<Prova> provas = DBInstance.DB.Prova.AsQueryable();
            List<ProvaTO> provasTO = provas.Include(e => e.Professor.Usuario).Select(prova => (ProvaTO) prova).ToList();

            return provasTO;
        }
    }
}
