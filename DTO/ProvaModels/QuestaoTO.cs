using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DTO
{
    public enum Options
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
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
    }
}