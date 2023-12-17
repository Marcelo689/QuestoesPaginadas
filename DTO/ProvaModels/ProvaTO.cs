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

        public ProvaResultado GetResultados()
        {
            return new ProvaResultado
            {
                Questoes = Questoes
            };
        }
        public void PreencherRespostas(ProvaOpcoesMarcadasViewModel provaViewModel)
        {
            var properties = provaViewModel.GetType().GetProperties();

            int quantidadeQuestoes = GetQuantidadeQuestoes();
            PreencherRespostas(provaViewModel, properties, quantidadeQuestoes);
        }
        private void PreencherRespostas(ProvaOpcoesMarcadasViewModel provaViewModel, PropertyInfo[] property, int quantidadeQuestoes)
        {
            for (int i = 0; i < quantidadeQuestoes; i++)
            {
                var campo = property[i];
                string nome = campo.Name;
                var questaoId = GetGetIdFromQuestionName(nome);
                var opcaoPreenchida = campo.GetValue(provaViewModel);

                QuestaoTO questaoSelecinada = this.Questoes.FirstOrDefault(questao => questao.Id == questaoId);

                bool questaoFoiEncontrada = questaoSelecinada != null;
                if (questaoFoiEncontrada)
                {
                    PreencherOpcaoDaQuestao(opcaoPreenchida, questaoSelecinada);
                }
                else
                {

                }
            }
        }
        private static void PreencherOpcaoDaQuestao(object opcaoPreenchida, QuestaoTO questaoSelecinada)
        {
            questaoSelecinada.SelectedOption = (Options)opcaoPreenchida;
        }
        private static int GetGetIdFromQuestionName(string nome)
        {
            return Convert.ToInt32(nome.Split('_')[1]);
        }
        private int GetQuantidadeQuestoes()
        {
            if (Questoes is null)
                return 0;
            else
                return Questoes.Length;
        }
    }
}
