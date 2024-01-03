using BancoProject.Login;
using DTO;
using DTO.BancoClasses.Login.Entidades.EstudanteFolder;
using DTO.BancoClasses.Login.Entidades.ProfessorFolder;
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

        public static ProvaTO GetProvaById(int provaId)
        {
            Prova prova = DBInstance.DB.Prova.FirstOrDefault(e => e.Id == provaId);

            return ProvaToTO(provaId, prova);
        }

        private static ProvaTO ProvaToTO(int provaId, Prova prova)
        {
            IQueryable<Questao> questoesDB = DBInstance.DB.Questao.Include(e => e.Prova).Where(questao => questao.Prova.Id == provaId);
            IQueryable<QuestaoTO> questoes = questoesDB.Select(questao => (QuestaoTO) questao);
            ProvaTO provaTO = (ProvaTO)prova;

            Estudante? estudante = DBInstance.DB.Estudante.ToList().FirstOrDefault(e => e.Id == prova.Estudante.Id);

            if(estudante is not null) {
                List<int> questoesIds = questoes.Select(e => e.Id).ToList();
                IQueryable<ProvaQuestaoResposta> questaoRespondidas = DBInstance.DB.ProvaQuestaoResposta.Where(e => e.QuestaoOpcao.Opcao != 0 && e.Prova.Id == provaId && e.Estudante.Id == estudante.Id);
                provaTO.Questoes = PreencherOpcoesSelecionadas(questoesDB.ToList(), questoesIds, questaoRespondidas).ToArray();
            }
            else
            {
                provaTO.Questoes = questoes.ToArray();
            }
            
            return provaTO;
        }

        public static ProvaTO CriarProva(ProvaTO provaTO)
        {
            Estudante? estudante = DBInstance.DB.Estudante.FirstOrDefault(e => e.Usuario.Id == provaTO.Usuario.Id);
            Professor? professor = DBInstance.DB.Professor.FirstOrDefault(e => e.Usuario.Id == provaTO.Usuario.Id);
            Prova prova = new Prova
            {
                Estudante = estudante,
                Professor = professor,
                Name = provaTO.Name,
            };

            DBInstance.DB.Prova.Add(prova);

            SaveChanges();
            ProvaTO provaCriada = (ProvaTO)prova;
            return provaCriada;
        }

        public static void UpdateQuestaoWithTO(QuestaoTO questaoTO)
        {
            Questao questao = DBInstance.DB.Questao.FirstOrDefault(quest => quest.Id == questaoTO.Id);

            PreencherDescricoesQuestaoDB(questaoTO, questao);
            questao.Descricao = questaoTO.Name;

            DBInstance.DB.Questao.Update(questao);
            DBInstance.DB.SaveChanges();
        }

        private static void PreencherOpcaoSelecionada(QuestaoTO questaoTO, Questao questao)
        {
            int opcaoSelecionada = ((int)questaoTO.SelectedOption);

            QuestaoOpcao questaoOpcao = DBInstance.DB.QuestaoOpcao.FirstOrDefault(e => ((int) e.Opcao) == opcaoSelecionada);

            if (questaoOpcao is not null)
            {
                questao.OpcaoSelecionada = questaoOpcao;
            }
        }

        private static void PreencherDescricoesQuestaoDB(QuestaoTO questaoTO, Questao questao)
        {
            questao.DescricaoOpcao1 = questaoTO.OptionDescriptions[Options.A];
            questao.DescricaoOpcao2 = questaoTO.OptionDescriptions[Options.B];
            questao.DescricaoOpcao3 = questaoTO.OptionDescriptions[Options.C];
            questao.DescricaoOpcao4 = questaoTO.OptionDescriptions[Options.D];
            questao.DescricaoOpcao5 = questaoTO.OptionDescriptions[Options.E];
        }
    }
}
