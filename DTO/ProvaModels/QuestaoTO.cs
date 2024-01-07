using DTO.BancoClasses.ProvaFolder;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO
{
    public enum Options
    {
        Vazio = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
    }

    public class QuestaoTO
    {
        public QuestaoTO()
        {

        }
        public QuestaoTO(List<string> descricoes)
        {
            InsertDescriptions(descricoes);
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        public bool IsTeacher { get; set; } 

        [JsonPropertyName("selectedOption")]
        public Options? SelectedOption { get; set; } = null;

        [JsonPropertyName("optionDescriptions")]
        public Dictionary<Options, string> OptionDescriptions { get; set; } = new Dictionary<Options, string>();

        private void InsertDescriptions(List<string> optionsDescriptions)
        {
            bool optionsAmountError = optionsDescriptions.Count != 5;

            if (optionsAmountError)
                ErrorNecessary5Descriptions();
            else
                AddDescriptions(optionsDescriptions);
        }
        private void AddDescriptions(List<string> optionsDescriptions)
        {
            OptionDescriptions.Clear();
            OptionDescriptions.Add(Options.A, optionsDescriptions[0]);
            OptionDescriptions.Add(Options.B, optionsDescriptions[1]);
            OptionDescriptions.Add(Options.C, optionsDescriptions[2]);
            OptionDescriptions.Add(Options.D, optionsDescriptions[3]);
            OptionDescriptions.Add(Options.E, optionsDescriptions[4]);
        }
        private static void ErrorNecessary5Descriptions()
        {
            throw new ArgumentException("Necessary exacly 5 descriptions");
        }
        public bool QuestaoPreenchida()
        {
            return SelectedOption != null;
        }

        public static explicit operator QuestaoTO(Questao questao)
        {
            return new QuestaoTO
            {
                Id = questao.Id,
                Name = questao.Descricao,
                OptionDescriptions = GetQuestaoOptionsDescriptions(questao),
                SelectedOption = (Options) questao.OpcaoSelecionada.Opcao
            };
        }

        private static Dictionary<Options, string> GetQuestaoOptionsDescriptions(Questao questao)
        {
            var opcoes = new Dictionary<Options, string>();
            
            opcoes.Add(Options.A, questao.DescricaoOpcao1);
            opcoes.Add(Options.B, questao.DescricaoOpcao2);
            opcoes.Add(Options.C, questao.DescricaoOpcao3);
            opcoes.Add(Options.D, questao.DescricaoOpcao4);
            opcoes.Add(Options.E, questao.DescricaoOpcao5);

            return opcoes;
        }
    }
}