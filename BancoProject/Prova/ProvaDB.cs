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
        public static readonly DbSet<Prova> ProvaRepository = DBInstance.DB.Set<Prova>();
        public static readonly DbSet<Estudante> EstudanteRepository = DBInstance.DB.Set<Estudante>();
        public static readonly DbSet<Professor> ProfessorRepository = DBInstance.DB.Set<Professor>();
        public static readonly DbSet<Questao> QuestaoRepository = DBInstance.DB.Set<Questao>();
        public static readonly DbSet<QuestaoOpcao> QuestaoOpcaoRepository = DBInstance.DB.Set<QuestaoOpcao>();
        public static readonly DbSet<ProvaQuestaoResposta> ProvaQuestaoRespostaRepository = DBInstance.DB.Set<ProvaQuestaoResposta>();
        public static ProvaTO GetProvaDB(int id)
        { 
            ProvaTO provaTO = PreencheProvaTO(id).Result;
            return provaTO;
        }

        public async static Task<ProvaTO> PreencheProvaTO(int provaId)
        {
            Prova? provaDB = null;
            var provaTO = new ProvaTO();

            bool provaNaoExiste = provaId == 0;
            
            if(provaNaoExiste)
            {
                return new ProvaTO();
            }

            bool provaExiste = !provaNaoExiste;

            if (provaExiste)
            {
                provaDB = await ProvaRepository.Include(e => e.Professor.Usuario).Include(e => e.Estudante).FirstOrDefaultAsync(e => e.Id == provaId);
            }

            if (provaNaoExiste)
            {
                int provaUltimoId = ProvaRepository.OrderBy(e => e.Id).AsEnumerable().LastOrDefault().Id;
                provaTO.Id = provaUltimoId + 1;
                List<QuestaoTO> questoesTO = new List<QuestaoTO>();

                provaTO.Name = "ProvaDefault";
                provaTO.Questoes = questoesTO.ToArray();
                provaTO.Estudante = (EstudanteTO) EstudanteRepository.FirstOrDefault();
                provaTO.ProfessorNome = ProfessorRepository.Include( e => e.Usuario).FirstOrDefault().Usuario.Username;

                var prova = (Prova) provaTO;
            }
            else
            {
                provaTO = (ProvaTO) provaDB;
                
                Estudante? estudante = EstudanteRepository.FirstOrDefault(e => e.Id == provaDB.Estudante.Id);

                IQueryable<Questao> questoes = DBInstance.DB.Questao.Where(e => e.Prova.Id == provaId);
                IQueryable<ProvaQuestaoResposta> questaoRespondidas = DBInstance.DB.ProvaQuestaoResposta.Where(e => e.QuestaoOpcao.Opcao != 0 && e.Prova.Id == provaId && e.Estudante.Id == estudante.Id);
                var listaTO = PreencherOpcoesSelecionadas(questoes.ToList(), questaoRespondidas);
                provaTO.Questoes = listaTO.ToArray();
                provaTO.Estudante = (EstudanteTO) estudante;
            }

            return provaTO;
        }

        private static List<QuestaoTO> PreencherOpcoesSelecionadas(List<Questao> questoes, IQueryable<ProvaQuestaoResposta> questaoRespondidas)
        {
            List<int> questoesIds = questoes.Select(e => e.Id).ToList();
            foreach (var questao in questoes)
            {
                if (questoesIds.Contains(questao.Id))
                {
                    var algo = questaoRespondidas.Include(e => e.QuestaoOpcao).Where(e => e.Questao.Id == questao.Id).ToList();
                    var opcao = algo.Select(e => e.QuestaoOpcao).FirstOrDefault();
                    bool opcaoExiste = opcao != null;
                    if (opcaoExiste)
                    {
                        questao.OpcaoSelecionada = QuestaoOpcaoRepository.FirstOrDefault( e => e.Opcao == opcao.Opcao);// error 
                    }
                    else
                    {
                        questao.OpcaoSelecionada = QuestaoOpcaoRepository.FirstOrDefault(e => e.Id == 1);
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
            Questao questao = QuestaoRepository.FirstOrDefault(q => q.Id == questaoTO.Id);
            var num1 = GetSelectedOption(questaoTO);
            QuestaoOpcao opcaoSelecionada = QuestaoOpcaoRepository.FirstOrDefault(qo => ((int)qo.Opcao) == num1);

            Prova prova = ProvaRepository.FirstOrDefault(e => e.Id == provaTO.Id);
            Estudante estudante = EstudanteRepository.FirstOrDefault(e => e.Usuario.Id == provaTO.Usuario.Id);

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
            IQueryable<Prova> provas = ProvaRepository.AsQueryable();
            List<ProvaTO> provasTO = provas.Include(e => e.Professor.Usuario).Select(prova => (ProvaTO) prova).ToList();

            return provasTO;
        }

        public static ProvaTO GetProvaById(int provaId)
        {
            Prova prova = ProvaRepository.FirstOrDefault(e => e.Id == provaId);
            ProvaTO provaTO = ProvaToTO(provaId, prova);
            prova = null;
            return provaTO;
        }

        private static ProvaTO ProvaToTO(int provaId, Prova prova)
        {
            IQueryable<Questao> questoesDB = QuestaoRepository.Include(e => e.Prova).Where(questao => questao.Prova.Id == provaId);
            IQueryable<QuestaoTO> questoes = questoesDB.Select(questao => (QuestaoTO) questao);
            ProvaTO provaTO = (ProvaTO)prova;

            Estudante? estudante = EstudanteRepository.ToList().FirstOrDefault(e => e.Id == prova.Estudante.Id);

            if(estudante is not null) {
                IQueryable<ProvaQuestaoResposta> questaoRespondidas = ProvaQuestaoRespostaRepository.Where(e => e.QuestaoOpcao.Opcao != 0 && e.Prova.Id == provaId && e.Estudante.Id == estudante.Id);
                provaTO.Questoes = PreencherOpcoesSelecionadas(questoesDB.ToList(), questaoRespondidas).ToArray();
            }
            else
            {
                provaTO.Questoes = questoes.ToArray();
            }
            
            return provaTO;
        }

        public static ProvaTO CriarProva(ProvaTO provaTO)
        {
            Estudante? estudante = EstudanteRepository.FirstOrDefault(e => e.Usuario.Id == provaTO.Usuario.Id);
            Professor? professor = ProfessorRepository.FirstOrDefault(e => e.Usuario.Id == provaTO.Usuario.Id);
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

        public static void UpdateQuestaoFromTO(QuestaoTO questaoTO, int provaId)
        {
            Questao questao = QuestaoRepository.FirstOrDefault(quest => quest.Id == questaoTO.Id);

            if(questao is null){
                questao = (Questao) questaoTO;
                questao.Prova = ProvaRepository.FirstOrDefault(provaIdent => provaIdent.Id == provaId);
                QuestaoRepository.Add(questao);
            }

            if (provaId == 0)
                questao.Prova = ProvaRepository.FirstOrDefault();

            PreencherDescricoesQuestaoDB(questaoTO, questao);
            questao.Descricao = questaoTO.Name;
            QuestaoRepository.Update(questao);
        }

        private static void PreencherOpcaoSelecionada(QuestaoTO questaoTO, Questao questao)
        {
            int opcaoSelecionada = ((int)questaoTO.SelectedOption);
            QuestaoOpcao questaoOpcao = QuestaoOpcaoRepository.FirstOrDefault(e => ((int) e.Opcao) == opcaoSelecionada);

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

        public static void DeletarProva(ProvaTO provaTO)
        {
            Questao questaoParaDelete = QuestaoRepository.FirstOrDefault(e => provaTO.QuestaoDelete.Id == e.Id);
            QuestaoRepository.Remove(questaoParaDelete);
        }

        public static void DeletarQuestao(int id)
        {
            Questao questaoParaDelete = QuestaoRepository.FirstOrDefault(e => id == e.Id);
            QuestaoRepository.Remove(questaoParaDelete);
            SaveChanges();
        }
    }
}
