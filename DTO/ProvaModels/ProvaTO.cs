using DTO.BancoClasses.ProvaFolder;
using DTO.Login;
using DTO.Login.EstudanteFolder;
using DTO.ProvaModels;
using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace DTO
{
    public class ProvaTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("description")]
        public string Description { get; set; } = "";
        
        [JsonPropertyName("questoes")]
        public QuestaoTO[]  Questoes { get; set; }
        public EstudanteTO Estudante { get; set; }

        [JsonPropertyName("professorNome")]
        public string ProfessorNome { get; set; }  
        public UsuarioTO Usuario { get; set; }
        public ProvaResultado GetResultados()
        {
            return new ProvaResultado
            {
                Questoes = Questoes
            };
        }
        public void PreencherRespostas(ProvaOpcoesMarcadasViewModel provaViewModel, bool isEditar = false)
        {
            var properties = provaViewModel.GetType().GetProperties();

            int quantidadeQuestoes = GetQuantidadeQuestoes();
            PreencherRespostas(provaViewModel, properties, quantidadeQuestoes, isEditar);
        }
        private void PreencherRespostas(ProvaOpcoesMarcadasViewModel provaViewModel, PropertyInfo[] property, int quantidadeQuestoes, bool isEditar = false)
        {
            int numeroOpcoesPorQuestao = 5;
            for (int i = 0; i < quantidadeQuestoes * numeroOpcoesPorQuestao; i+= 7)
            {
                var campo = property[i];
                string nome = campo.Name;

                var questaoId = GetGetIdFromQuestionName(nome);

                int indiceOpcaoPreenchida = (int) campo.GetValue(provaViewModel);

                QuestaoTO questaoSelecinada = this.Questoes.FirstOrDefault(questao => questao.Id == questaoId);

                if (isEditar)
                {
                    AtualizaDescricoesOpcoes(provaViewModel, property, i , questaoSelecinada);
                }
                if (questaoFoiPreenchida(questaoSelecinada, indiceOpcaoPreenchida))
                {
                    PreencherOpcaoDaQuestao(indiceOpcaoPreenchida, questaoSelecinada);
                }
            }
        }

        private void AtualizaDescricoesOpcoes(ProvaOpcoesMarcadasViewModel provaViewModel, PropertyInfo[] propertyArray, int indiceQuestao, QuestaoTO questaoSelecionada)
        {
            string descricaoPrincipal  = propertyArray[indiceQuestao + 1].GetValue(provaViewModel).ToString();

            string campoDescricaoNome1 = propertyArray[indiceQuestao + 2].GetValue(provaViewModel).ToString();
            string campoDescricaoNome2 = propertyArray[indiceQuestao + 3].GetValue(provaViewModel).ToString();
            string campoDescricaoNome3 = propertyArray[indiceQuestao + 4].GetValue(provaViewModel).ToString();
            string campoDescricaoNome4 = propertyArray[indiceQuestao + 5].GetValue(provaViewModel).ToString();
            string campoDescricaoNome5 = propertyArray[indiceQuestao + 6].GetValue(provaViewModel).ToString();

            questaoSelecionada.Name = descricaoPrincipal;

            questaoSelecionada.OptionDescriptions[Options.A] = campoDescricaoNome1;
            questaoSelecionada.OptionDescriptions[Options.B] = campoDescricaoNome2;
            questaoSelecionada.OptionDescriptions[Options.C] = campoDescricaoNome3;
            questaoSelecionada.OptionDescriptions[Options.D] = campoDescricaoNome4;
            questaoSelecionada.OptionDescriptions[Options.E] = campoDescricaoNome5;
        }

        private static bool questaoFoiPreenchida(QuestaoTO questaoSelecinada, int indiceOpcaoPreenchida)
        {
            bool questaoFoiEncontrada = questaoSelecinada != null;
            return questaoFoiEncontrada && indiceOpcaoPreenchida != 0;
        }

        private static void PreencherOpcaoDaQuestao(int indiceQuestao, QuestaoTO questaoSelecinada)
        {
            questaoSelecinada.SelectedOption = (Options) Convert.ToInt32(indiceQuestao);

        }
        private static int GetGetIdFromQuestionName(string nome)
        {
            if (nome.Contains("descricao"))
            {
                return Convert.ToInt32(nome.Split('_')[2]);
            }
            else
            {
                return Convert.ToInt32(nome.Split('_')[1]);
            }

        }
        private int GetQuantidadeQuestoes()
        {
            if (Questoes is null)
                return 0;
            else
                return Questoes.Length;
        }

        public static explicit operator ProvaTO(Prova to)
        {
            return new ProvaTO
            {
                Id = to.Id,
                Name = to.Name,
                Description = to.Name,
                ProfessorNome = to.Professor != null ? to.Professor.Usuario.Username : null,
            };
        }
    }
}
